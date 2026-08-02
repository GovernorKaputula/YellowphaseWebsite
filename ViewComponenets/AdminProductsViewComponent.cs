using Microsoft.AspNetCore.Mvc;
using YellowphaseWebsite.Data;
using Microsoft.EntityFrameworkCore;

public class AdminProductsViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public AdminProductsViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var products = await _context.Products
            .OrderByDescending(p => p.Id)
            .ToListAsync();

        return View(products);
    }
}