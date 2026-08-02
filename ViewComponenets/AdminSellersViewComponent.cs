using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YellowphaseWebsite.Models;

public class AdminSellersViewComponent : ViewComponent
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminSellersViewComponent(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var users = _userManager.Users.ToList();

        var sellers = new List<ApplicationUser>();

        foreach (var user in users)
        {
            if (await _userManager.IsInRoleAsync(user, "Seller"))
            {
                sellers.Add(user);
            }
        }

        return View(sellers);
    }
}