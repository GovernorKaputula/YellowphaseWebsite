using System.Net.Http.Json;

namespace YellowphaseWebsite.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _http;

        public CurrencyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<decimal> ConvertAsync(decimal amount, string toCurrency)
        {
            toCurrency = CurrencyPolicy.Normalize(toCurrency);

            // No conversion needed
            if (toCurrency == "ZMW")
                return amount;

            try
            {
                var url =
                    $"https://api.exchangerate.host/convert?from=ZMW&to={toCurrency}&amount={amount}";


                var res =
                    await _http.GetFromJsonAsync<ExchangeResponse>(url);


                return res?.result ?? amount;
            }
            catch (Exception ex)
            {
               

                return amount;
            }
        }
    }

    public class ExchangeResponse
    {
        public decimal result { get; set; }
    }
}