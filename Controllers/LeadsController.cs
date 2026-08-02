using Microsoft.AspNetCore.Mvc;
using YellowphaseWebsite.Data;
using YellowphaseWebsite.Models;
using YellowphaseWebsite.Services;

namespace YellowphaseWebsite.Controllers
{
    public class LeadsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly TikTokEventService _tiktokEventService;

        public LeadsController(ApplicationDbContext db, TikTokEventService tiktokEventService)
        {
            _db = db;
            _tiktokEventService = tiktokEventService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Contact", "Home");

            // Save to database
            var lead = new ContactLead
            {
                Name = model.Name,
                Phone = model.Phone,
                Email = model.Email,
                Service = model.Service,
                Message = model.Message
            };

            _db.ContactLeads.Add(lead);
            await _db.SaveChangesAsync();

            // TikTok tracking
            await _tiktokEventService.SendEventAsync(
                "SubmitForm",
                model.Email,
                model.Phone,
                HttpContext.Connection.RemoteIpAddress?.ToString(),
                Request.Headers["User-Agent"].ToString()
            );

            return RedirectToAction("ThankYou", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ProductInquiry(ContactModel model, int ProductId, string ProductName)
        {
            var lead = new ContactLead
            {
                Name = model.Name,
                Phone = model.Phone,
                Email = model.Email,
                Message = $"Product Inquiry: {ProductName} - {model.Message}",
                Service = "Product Inquiry"
            };

            _db.ContactLeads.Add(lead);
            await _db.SaveChangesAsync();

            // TikTok tracking
            await _tiktokEventService.SendEventAsync(
                "SubmitForm",
                model.Email,
                model.Phone,
                HttpContext.Connection.RemoteIpAddress?.ToString(),
                Request.Headers["User-Agent"].ToString()
            );

            return RedirectToAction("ThankYou", "Home");
        }
    }
}