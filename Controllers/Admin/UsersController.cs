using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YellowphaseWebsite.Models;

namespace YellowphaseWebsite.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // =========================
        // LIST USERS
        // =========================
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // =========================
        // GET USER DETAILS (roles included)
        // =========================
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            ViewBag.Roles = roles;
            ViewBag.AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            return View(user);
        }

        // =========================
        // ASSIGN ROLE
        // =========================
        [HttpPost]
        public async Task<IActionResult> AddRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (!await _roleManager.RoleExistsAsync(role))
                return BadRequest("Role does not exist");

            await _userManager.AddToRoleAsync(user, role);

            return RedirectToAction(nameof(Details), new { id = userId });
        }

        // =========================
        // REMOVE ROLE
        // =========================
        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            await _userManager.RemoveFromRoleAsync(user, role);

            return RedirectToAction(nameof(Details), new { id = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(
        string Id,
        string FullName,
        string Email,
        string PhoneNumber)
        {
            var user = await _userManager.FindByIdAsync(Id);


       if (user == null)
                return NotFound();

            user.FullName = FullName;
            user.Email = Email;
            user.UserName = Email;
            user.PhoneNumber = PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                var roles = await _userManager.GetRolesAsync(user);

                ViewBag.Roles = roles;
                ViewBag.AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();

                return View("Details", user);
            }

            return RedirectToAction(nameof(Details), new { id = user.Id });

        }


        // =========================
        // DELETE USER
        // =========================
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}