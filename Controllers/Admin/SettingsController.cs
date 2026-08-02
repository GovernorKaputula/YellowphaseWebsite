using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowphaseWebsite.Data;
using YellowphaseWebsite.Models;

namespace YellowphaseWebsite.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SettingsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _db.AppSettings.ToListAsync();
            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string key, string value)
        {
            var setting = await _db.AppSettings.FirstOrDefaultAsync(s => s.Key == key);

            if (setting == null)
                return NotFound();

            setting.Value = value;

            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}