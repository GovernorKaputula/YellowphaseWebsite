using Microsoft.AspNetCore.Identity;

namespace YellowphaseWebsite.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? BusinessName { get; set; }   // ✅ ADD THIS

        public string? ProfileImageUrl { get; set; }

    }
}