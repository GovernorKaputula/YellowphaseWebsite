using System;
namespace YellowPhase.Models.ViewModels
{
    public class ProductDisplayViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? DisplayCurrency { get; set; }
        public string? Image { get; set; }
        public string? ProductType { get; set; }
        // optional seller contact info if available
        public string? SellerPhone { get; set; }
    }
}
