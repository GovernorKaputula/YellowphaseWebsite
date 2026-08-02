namespace YellowphaseWebsite.Models
{
    public class PayPalPaymentDto
    {
        public string OrderId { get; set; } = "";
        public string PayerName { get; set; } = "";
        public string PayerEmail { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }   // ✅ ADD THIS
    }
}
