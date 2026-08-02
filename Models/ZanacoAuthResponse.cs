namespace YellowphaseWebsite.Models
{
    public class ZanacoAuthResponse
    {
        public AuthResponse Response { get; set; } = new();
        public string Signature { get; set; } = "";
    }

    public class AuthResponse
    {
        public string Message { get; set; } = "";
        public string AccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
    }
}
