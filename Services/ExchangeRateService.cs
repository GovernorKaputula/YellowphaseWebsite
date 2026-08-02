using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace YellowphaseWebsite.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ExchangeRateService> _logger;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

        public ExchangeRateService(HttpClient httpClient, IMemoryCache cache, ILogger<ExchangeRateService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Returns the exchange rate expressed as: 1 USD = {rate} {currency}
        // Example: GetRateAsync("ZMW") -> 20.5 means 1 USD = 20.5 ZMW
        public async Task<decimal> GetRateAsync(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                return 1m; // fallback
            }

            currency = currency.Trim().ToUpperInvariant();

            // USD is base: 1 USD = 1 USD
            if (currency == "USD")
            {
                return 1m;
            }

            var cacheKey = $"fx_rate_{currency}";
            if (_cache.TryGetValue(cacheKey, out decimal cached))
            {
               
                return cached;
            }

            try
            {
                // Use open.er-api.com as primary provider (single source) to avoid slow double lookups
                var url = $"https://open.er-api.com/v6/latest/USD";
              

                using var resp = await _httpClient.GetAsync(url);
                resp.EnsureSuccessStatusCode();
                var json = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("rates", out var ratesEl) && ratesEl.TryGetProperty(currency, out var rateEl))
                {
                    decimal rate;
                    if (rateEl.ValueKind == JsonValueKind.Number)
                    {
                        if (rateEl.TryGetDecimal(out rate))
                        {
                            _cache.Set(cacheKey, rate, CacheDuration);
                          
                            return rate;
                        }
                        if (rateEl.TryGetDouble(out var d))
                        {
                            rate = Convert.ToDecimal(d);
                            _cache.Set(cacheKey, rate, CacheDuration);
                        
                            return rate;
                        }
                    }
                    else if (rateEl.ValueKind == JsonValueKind.String)
                    {
                        var s = rateEl.GetString();
                        if (decimal.TryParse(s, out rate))
                        {
                            _cache.Set(cacheKey, rate, CacheDuration);
                          
                            return rate;
                        }
                    }

                   
                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
               
            }

            // fallback to 1 to avoid crashing callers; callers should handle unrealistic result
            return 1m;
        }
    }
}