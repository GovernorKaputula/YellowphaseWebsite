using Microsoft.AspNetCore.Mvc;
using YellowphaseWebsite.Data;

public class AdminKpiViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public AdminKpiViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var model = new AdminKpiViewModel
        {
            TotalLeads = _context.ContactLeads.Count(),
            ActiveProducts = _context.Products.Count(p => p.IsApproved),
            VerifiedSellers = _context.Users.Count(u => u.EmailConfirmed),
            NetworkThroughput = "99.98%"
        };

        return View(model);
    }
}