namespace YellowphaseWebsite.Models
{
    public class ProductInquiry
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
