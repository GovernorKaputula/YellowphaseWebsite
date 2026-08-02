using Microsoft.AspNetCore.Mvc;
using YellowphaseWebsite.Services;

namespace YellowphaseWebsite.Controllers
{
    public class TestController : Controller
    {
        private readonly IRsaSignatureService _rsaSignatureService;

        public TestController(
            IRsaSignatureService rsaSignatureService)
        {
            _rsaSignatureService = rsaSignatureService;
        }

        public IActionResult SignatureTest()
        {
            var payload =
                "{\"amount\":100}";

            var signature =
                _rsaSignatureService.Sign(payload);

            var isValid =
                _rsaSignatureService.Verify(
                    payload,
                    signature);

            return Json(new
            {
                Payload = payload,
                Signature = signature,
                IsValid = isValid
            });
        }
    }
}