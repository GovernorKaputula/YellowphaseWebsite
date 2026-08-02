using System.Security.Cryptography;
using System.Text;
using System.Net.Http.Json;

namespace YellowphaseWebsite.Services
{
    public class MetaCapiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MetaCapiService(
            HttpClient httpClient,
            IConfiguration config,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendEvent(
            string eventName,
            decimal value,
            string currency,
            string? email,
            string eventId,
            string? testEventCode = null)
        {
            var pixelId = _config["Meta:PixelId"];
            var token = _config["Meta:AccessToken"];

            var url =
                $"https://graph.facebook.com/v20.0/{pixelId}/events?access_token={token}";

            var httpContext = _httpContextAccessor.HttpContext;

            var userData = new
            {
                em = string.IsNullOrWhiteSpace(email)
                    ? null
                    : new[] { Hash(email) },

                client_ip_address = httpContext?.Connection?.RemoteIpAddress?.ToString(),
                client_user_agent = httpContext?.Request?.Headers["User-Agent"].ToString()
            };

            var payload = new
            {
                data = new[]
                {
                    new
                    {
                        event_name = eventName,
                        event_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        event_id = eventId,
                        action_source = "website",

                        test_event_code = testEventCode, // ✅ only used in test mode

                        user_data = userData,

                        custom_data = new
                        {
                            currency = currency,
                            value = value
                        }
                    }
                }
            };

            var response = await _httpClient.PostAsJsonAsync(url, payload);
            var responseBody = await response.Content.ReadAsStringAsync();

            // ❗ IMPORTANT: fail fast if Meta rejects request
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Meta CAPI Error: {responseBody}");
            }
        }

        // ===============================
        // SHA256 hashing for Meta user_data
        // ===============================
        private string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(
                Encoding.UTF8.GetBytes(input.Trim().ToLower())
            );

            return Convert.ToHexString(bytes).ToLower();
        }
    }
}