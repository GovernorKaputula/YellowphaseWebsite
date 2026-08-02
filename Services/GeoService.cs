using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;

namespace YellowphaseWebsite.Services
{
    public class GeoService
    {
        private readonly HttpClient _http;
        private readonly IMemoryCache _cache;

        public GeoService(HttpClient http, IMemoryCache cache)
        {
            _http = http;
            _cache = cache;
        }

        public async Task<string> GetCurrencyAsync(string ip)
        {
           

            var cacheKey = $"currency_debug_{ip}_{DateTime.UtcNow:yyyyMMddHHmm}";

            if (_cache.TryGetValue(cacheKey, out string cachedCurrency))
            {
                
                return cachedCurrency;
            }

            try
            {
                var url = $"https://ipapi.co/{ip}/json/";

             

                var res = await _http.GetFromJsonAsync<IpApiResponse>(url);

             

                var currency = res?.currency ?? "USD";

        

                _cache.Set(cacheKey, currency, TimeSpan.FromHours(6));

                return currency;
            }
            catch (Exception ex)
            {
              

                return "USD";
            }
        }
    }

    public class IpApiResponse
    {
        public string country { get; set; }
        public string currency { get; set; }
    }
}