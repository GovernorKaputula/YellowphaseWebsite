using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using YellowphaseWebsite.Models;

namespace YellowphaseWebsite.ViewComponents
{
    public class AdminUsersViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminUsersViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
    }
}