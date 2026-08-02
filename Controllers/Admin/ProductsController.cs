using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowphaseWebsite.Data;
using YellowphaseWebsite.Models;

namespace YellowphaseWebsite.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // =========================
        // DASHBOARD (GRID VIEW)
        // =========================
        public async Task<IActionResult> Index()
        {
            var products = await _db.Products
                .Include(p => p.Seller)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(products);
        }

        // =========================
        // CREATE (AJAX)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([FromForm] Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Invalid data" });

            model.CreatedAt = DateTime.UtcNow;
            model.IsApproved = true;
            model.IsUserSubmitted = false;

            // 🔥 NEW: SYSTEM PRODUCT (no email fields anymore)
            model.SellerId = null; // or assign a system seller if you create one

            _db.Products.Add(model);
            await _db.SaveChangesAsync();

            return Json(new
            {
                success = true,
                id = model.Id,
                name = model.Name,
                category = model.Category,
                productType = model.ProductType,
                productCode = model.ProductCode ?? "UNASSIGNED",
                price = model.Price.ToString("0.00"),
                quantity = model.Quantity,
                imageUrl = model.ImageUrl ?? ""
            });
        }

        // =========================
        // DELETE (AJAX)
        // =========================
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
                return NotFound(new { success = false, message = "Product not found" });

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return Ok(new { success = true });
        }

        // =========================
        // EDIT (GET)
        // =========================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _db.Products
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // =========================
        // EDIT (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product model, IFormFile? ImageFile)
        {
            var product = await _db.Products.FindAsync(model.Id);

            if (product == null)
                return NotFound(new { success = false });

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Category = model.Category;
            product.ProductType = model.ProductType;
            product.ProductCode = model.ProductCode;
            product.IsApproved = model.IsApproved;

            // ✅ NEW FIELD
            product.Quantity = model.Quantity;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var path = Path.Combine("wwwroot/images", fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await ImageFile.CopyToAsync(stream);

                product.ImageUrl = "/images/" + fileName;
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}