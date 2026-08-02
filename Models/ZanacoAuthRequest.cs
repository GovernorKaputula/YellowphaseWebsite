namespace YellowphaseWebsite.Models
{
    public class ZanacoAuthRequest
    {
        public string ApiKey { get; set; } = "";
        public string GrantType { get; set; } = "login";
        public string AccessKey { get; set; } = "";
        public string ApiSecret { get; set; } = "";
    }
}
