using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YellowphaseWebsite.Data;

namespace YellowphaseWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var leads = _db.ContactLeads
                .OrderByDescending(x => x.Id)
                .ToList();

            ViewBag.ActiveProductsCount = _db.Products.Count(p => p.IsApproved);

            ViewBag.VerifiedSellersCount = _db.Products
                .Select(p => p.SellerId)
                .Distinct()
                .Count();

            ViewBag.NetworkThroughput = "99.98%";

            return View(leads);
        }
    }
}