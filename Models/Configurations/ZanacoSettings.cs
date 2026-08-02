namespace YellowphaseWebsite.Models.Configurations
{
    public class ZanacoSettings
    {
        public string BaseUrl { get; set; } = "";
        public string ApiKey { get; set; } = "";
        public string AccessKey { get; set; } = "";
        public string ApiSecret { get; set; } = "";

        public string TerminalId { get; set; } = "";
        public string MerchantCategoryCode { get; set; } = "";

        public string CallbackUrl { get; set; } = "";

        public string PrivateKeyPath { get; set; } = "";
        public string ZanacoPublicKeyPath { get; set; } = "";
    }
}