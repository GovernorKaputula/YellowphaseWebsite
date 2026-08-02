using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using YellowphaseWebsite.Models.Configurations;

namespace YellowphaseWebsite.Services
{
    public class ZanacoService : IZanacoService
    {
        private readonly HttpClient _httpClient;
        private readonly ZanacoSettings _settings;
        private readonly IRsaSignatureService _rsaSignatureService;

        public ZanacoService(
            HttpClient httpClient,
            IOptions<ZanacoSettings> settings,
            IRsaSignatureService rsaSignatureService)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _rsaSignatureService = rsaSignatureService;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> NameLookupAsync(
            string mobileNumber,
            string mobileNetwork)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChargeRequestAsync(
            decimal amount,
            string mobileNumber,
            string mobileNetwork,
            string transactionReference)
        {
            throw new NotImplementedException();
        }
    }
}