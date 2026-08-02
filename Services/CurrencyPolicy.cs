using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YellowphaseWebsite.Services
{
    public static class CurrencyPolicy
    {
        private static readonly HashSet<string> AllowedCurrencies = new()
        {
            "ZMW",
            "USD",
            "EUR",
            "GBP"
        };

        public static string Normalize(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                return "ZMW";

            currency = currency.Trim().ToUpperInvariant();

            return AllowedCurrencies.Contains(currency)
                ? currency
                : "ZMW";
        }

        public static string GetCurrencyForRegion(string region)
        {
            if (string.IsNullOrEmpty(region)) return "USD";
            if (region.StartsWith("GB") || region.StartsWith("EU")) return "GBP";
            if (region.StartsWith("NG")) return "NGN";
            if (region.StartsWith("KE")) return "KES";
            return "USD";
        }
    }
}