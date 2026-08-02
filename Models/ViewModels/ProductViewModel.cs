namespace YellowphaseWebsite.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }

        public decimal Price { get; set; }
        public string Currency { get; set; } = "ZMW";
        // Culture-aware pre-formatted display price, e.g. "$123.45" or "K384.00"
        public string DisplayPrice { get; set; } = "";

        public string ProductType { get; set; } = "";
        public int Quantity { get; set; }

        public string SellerId { get; set; } = "";
        public string SellerName { get; set; } = "";
        public string SellerPhone { get; set; } = "";
        public string SellerEmail { get; set; } = "";
    }
}