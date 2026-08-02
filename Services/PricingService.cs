using Microsoft.Extensions.Logging;
using YellowphaseWebsite.Services;

namespace YellowphaseWebsite.Services
{
    public class PricingService
    {
        private readonly IExchangeRateService _fx;
        private readonly ILogger<PricingService> _logger;

        public PricingService(
            IExchangeRateService fx,
            ILogger<PricingService> logger)
        {
            _fx = fx;
            _logger = logger;

           
        }

        public async Task<(decimal Price, string Currency)> ConvertAsync(decimal basePriceZmw, string targetCurrency)
        {
          

            // 1. Normalize requested currency
            targetCurrency = CurrencyPolicy.Normalize(targetCurrency);
    

            // 2. If no conversion needed
            if (targetCurrency == "ZMW")
            {
               
                return (basePriceZmw, "ZMW");
            }

            try
            {
                // 3. STEP 1: Obtain rates expressed as "amount of currency per 1 USD"
                //    ExchangeRateService.GetRateAsync(currency) returns currency_per_USD.
                //    To convert from ZMW (base) to target we compute:
                //      amount_in_target = (base_in_zmw / (ZMW_per_USD)) * (target_per_USD)
                //    i.e. convert base to USD, then USD to target.

                var rZmw = await _fx.GetRateAsync("ZMW");
        

                if (rZmw <= 0)
                {
      
                    return (basePriceZmw, "ZMW");
                }

                // 4. STEP 2: Obtain target currency rate (target per USD)
          
                var rTarget = await _fx.GetRateAsync(targetCurrency);
        

                if (rTarget <= 0)
                {
                 
                    return (basePriceZmw, "ZMW");
                }

                var priceInUsd = basePriceZmw / rZmw;
                var finalPrice = Math.Round(priceInUsd * rTarget, 2);

                // 5. FINAL RESULT
         

                return (finalPrice, targetCurrency);
            }
            catch (Exception ex)
            {
           

                return (basePriceZmw, "ZMW");
            }
        }
    }
}