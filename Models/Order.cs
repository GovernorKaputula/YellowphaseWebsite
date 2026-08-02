using System;
using System.ComponentModel.DataAnnotations;
using YellowphaseWebsite.Models;

namespace YellowphaseWebsite.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Reference to Product
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        // Snapshot values
        [Required]
        public string ItemName { get; set; } = string.Empty;

        public string ItemType { get; set; } = string.Empty;

        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        // Customer Information
        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? CompanyName { get; set; }

        public string? Notes { get; set; }

        // Workflow
        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}