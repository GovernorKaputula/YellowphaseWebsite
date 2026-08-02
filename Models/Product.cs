using System.ComponentModel.DataAnnotations.Schema;

namespace YellowphaseWebsite.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public string? Category { get; set; }
        public string? ProductType { get; set; }
        public string? ProductCode { get; set; }

        public decimal Price { get; set; }

        public bool IsApproved { get; set; }
        public bool IsUserSubmitted { get; set; }
        public DateTime CreatedAt { get; set; }

        // ✅ NEW: Quantity
        public int Quantity { get; set; }

        // ✅ SELLER LINK (Identity User)
        public string? SellerId { get; set; }

        [ForeignKey("SellerId")]
        public ApplicationUser Seller { get; set; }
    }
}