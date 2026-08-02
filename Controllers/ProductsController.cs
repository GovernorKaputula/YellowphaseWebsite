using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowphaseWebsite.Data;
using YellowphaseWebsite.Models;
using YellowphaseWebsite.Services;
using YellowphaseWebsite.ViewModels;

namespace YellowphaseWebsite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly PricingService _pricingService;
        private readonly GeoService _geoService;

        public ProductsController(
            ApplicationDbContext db,
            PricingService pricingService,
            GeoService geoService)
        {
            _db = db;
            _pricingService = pricingService;
            _geoService = geoService;
        }

        // =========================
        // MARKETPLACE
        // =========================
        public async Task<IActionResult> Index(
            string? search,
            string? category,
            string? type,
            decimal? minPrice,
            decimal? maxPrice)
        {
           

            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                ip = Request.Headers["X-Forwarded-For"].ToString();

         

            var currency = await GetUserCurrency();
        

            // =========================
            // QUERY (FIXED)
            // =========================
            var query = from p in _db.Products
                        where p.IsApproved
                        join u in _db.Users on p.SellerId equals u.Id into sellerJoin
                        from u in sellerJoin.DefaultIfEmpty()
                        select new ProductViewModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            ImageUrl = p.ImageUrl,   // ✅ FIXED HERE
                            Category = p.Category,
                            ProductType = p.ProductType,
                            Price = p.Price,

                            SellerName = u != null ? u.FullName : "System",
                            SellerEmail = u != null ? u.Email : ""
                        };

          

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    p.Name.Contains(search) ||
                    p.Description.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.Category == category);

            if (!string.IsNullOrWhiteSpace(type))
                query = query.Where(p => p.ProductType == type);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            var vmList = await query.ToListAsync();

         

            // =========================
            // PRICE CONVERSION (SAFE)
            // =========================
            foreach (var item in vmList)
            {
                var converted = await _pricingService.ConvertAsync(item.Price, currency);

                item.Price = converted.Price;
                item.Currency = converted.Currency;
            }


            foreach (var v in vmList)
            {
                
            }

            // =========================
            // FILTER DATA
            // =========================
            ViewBag.Categories = await _db.Products
                .Where(p => p.IsApproved)
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            ViewBag.Types = await _db.Products
                .Where(p => p.IsApproved)
                .Select(p => p.ProductType)
                .Distinct()
                .ToListAsync();

         

            return View(vmList);
        }

        // =========================
        // EDIT
        // =========================
        [HttpPost]
        public async Task<IActionResult> Edit(Product model, IFormFile ImageFile)
        {
            var existing = await _db.Products.FindAsync(model.Id);

            if (existing == null)
                return NotFound();

            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.Price = model.Price;
            existing.Category = model.Category;
            existing.ProductType = model.ProductType;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                existing.ImageUrl = "/images/" + fileName;
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // =========================
        // DETAILS
        // =========================
        public async Task<IActionResult> Details(int id)
        {
            // 1. Fetch your database entity
            YellowphaseWebsite.Models.Product product = await _db.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // 2. Map the data over to the ViewModel expected by our premium view
            var viewModel = new YellowphaseWebsite.ViewModels.ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                ImageUrl = product.ImageUrl,
               
            };

            // 3. Hand the ViewModel off to the view engine
            return View(viewModel);
        }

        // =========================
        // GEO
        // =========================
        private async Task<string> GetUserCurrency()
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

       

            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                var forwarded = Request.Headers["X-Forwarded-For"].ToString();

             
                ip = forwarded;
            }

            var currency = await _geoService.GetCurrencyAsync(ip ?? "");

        

            return currency;
        }

    }
}