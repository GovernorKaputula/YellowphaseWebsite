using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowphaseWebsite.Data;
using YellowphaseWebsite.Models;

namespace YellowphaseWebsite.Controllers
{
    [Authorize(Roles = "Seller,Admin")]
    public class SellerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public SellerController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // =========================
        // DASHBOARD
        // =========================
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Admin"))
            {
                var allProducts = await _db.Products
                    .Include(p => p.Seller)
                    .ToListAsync();

                return View(allProducts);
            }

            var products = await _db.Products
                .Where(p => p.SellerId == user.Id)
                .Include(p => p.Seller)
                .ToListAsync();

            return View(products);
        }

        // =========================
        // CREATE PRODUCT (GET)
        // =========================
        public IActionResult CreateProduct()
        {
            return View();
        }

        // =========================
        // CREATE PRODUCT (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            model.SellerId = user.Id;

            model.IsUserSubmitted = true;
            model.IsApproved = false;
            model.CreatedAt = DateTime.Now;

            _db.Products.Add(model);
            await _db.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }
    }
}