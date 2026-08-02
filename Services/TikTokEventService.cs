using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace YellowphaseWebsite.Services
{
    public class TikTokEventService
    {
        private readonly HttpClient _httpClient;
        private const string PIXEL_CODE = "D8G1T9RC77UANKFS9IU0";
        private const string ACCESS_TOKEN = "ded3ddf460f825bc2bcd3b325395e02fb92dba85";

        public TikTokEventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendEventAsync(
            string eventName,
            string email = null,
            string phone = null,
            string ip = null,
            string userAgent = null)
        {
            var url = "https://business-api.tiktok.com/open_api/v1.3/event/track/";
            var eventId = Guid.NewGuid().ToString();

            var payload = new
            {
                pixel_code = PIXEL_CODE,

                event_source = "web", // ✅ REQUIRED FIX
                event_source_id = PIXEL_CODE, // ✅ REQUIRED FIX

                data = new[]
                {
                    new
                    {
                        @event = eventName,
                        event_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        event_id = eventId,

                     
                        context = new
                        {
                            user = new
                            {
                                email = Hash(email),
                                phone_number = Hash(phone),
                                ip = ip,
                                user_agent = userAgent
                            }
                        },

                        properties = new
                        {
                            content_name = "Solar Quotation",
                            content_category = "Solar",
                            currency = "ZMW",
                            value = 1
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(payload);

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Access-Token", ACCESS_TOKEN);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.SendAsync(request);
                var responseText = await response.Content.ReadAsStringAsync();
                return responseText;
            }
            catch (Exception ex)
            {
                return $"Error sending conversion tracking metric event payload: {ex.Message}";
            }
        }

        private string Hash(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            using (var sha256 = SHA256.Create())
            {
                var normalized = input.Trim().ToLower();

                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(normalized));

                return BitConverter.ToString(bytes)
                    .Replace("-", "")
                    .ToLower();
            }
        }
    }
}