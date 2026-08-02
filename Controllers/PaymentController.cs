using Microsoft.AspNetCore.Mvc;

namespace YellowPhase.Controllers
{
    public class PaymentController : Controller
    {
        [HttpPost]
        public IActionResult Success([FromBody] PayPalPaymentDto model)
        {
            // Save to database here
            // Example:
            // model.OrderId
            // model.Amount
            // model.PayerEmail

            return Json(new { status = "ok" });
        }

        public IActionResult Success()
        {
            return View();
        }
    }

    public class PayPalPaymentDto
    {
        public string OrderId { get; set; }
        public string PayerName { get; set; }
        public string PayerEmail { get; set; }
        public decimal Amount { get; set; }
        public string ItemName { get; set; }
        public int ProductId { get; internal set; }
    }
}