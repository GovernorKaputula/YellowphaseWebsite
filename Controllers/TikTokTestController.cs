using Microsoft.AspNetCore.Mvc;
using YellowphaseWebsite.Services;

namespace YellowphaseWebsite.Controllers
{
    public class TikTokTestController : Controller
    {
        private readonly TikTokEventService _tikTokEventService;

        public TikTokTestController(TikTokEventService tikTokEventService)
        {
            _tikTokEventService = tikTokEventService;
        }

        [HttpGet]
        public async Task<IActionResult> SendTestEvent()
        {
            var result = await _tikTokEventService.SendEventAsync(
                eventName: "TestEvent",
                email: "test@email.com",
                phone: "260971234567",
                ip: HttpContext.Connection.RemoteIpAddress?.ToString(),
                userAgent: Request.Headers["User-Agent"]
            );

            return Content(result);
        }
    }
}