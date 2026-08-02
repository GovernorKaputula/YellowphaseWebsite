using System.ComponentModel.DataAnnotations;

namespace YellowphaseWebsite.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public int ProductId { get; set; }
        public string ItemName { get; set; } = "";
        public string ItemType { get; set; } = "";
        public decimal Amount { get; set; }
        public string Name { get; set; }

        public string CustomerName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string? CompanyName { get; set; }
        public string? Notes { get; set; }
        public string Currency { get; set; } = "ZMW";
        public string DisplayPrice { get; set; } = "";
        public int Quantity { get; set; } = 1;
    }
}