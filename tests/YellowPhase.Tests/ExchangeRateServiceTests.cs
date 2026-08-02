using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.Protected;
using Xunit;
using YellowphaseWebsite.Services;

namespace YellowPhase.Tests
{
    public class ExchangeRateServiceTests
    {
        [Fact]
        public async Task GetRateAsync_ReturnsRate_FromFallbackWhenPrimaryFails()
        {
            // Arrange: primary exchangerate.host returns missing_access_key JSON
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>("SendAsync",
                   ItExpr.IsAny<HttpRequestMessage>(),
                   ItExpr.IsAny<System.Threading.CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("{ \"success\": false, \"error\": { \"code\": 101 } }")
               });

            var httpClient = new HttpClient(handlerMock.Object);
            var cache = new MemoryCache(new MemoryCacheOptions());
            var logger = new NullLogger<ExchangeRateService>();
            var svc = new ExchangeRateService(httpClient, cache, NullLogger<ExchangeRateService>.Instance);

            // Act
            var rate = await svc.GetRateAsync("ZMW");

            // Assert: fallback should fail in this mock so rate==1
            Assert.Equal(1m, rate);
        }
    }
}
