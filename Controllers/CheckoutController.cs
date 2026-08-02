using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YellowphaseWebsite.Data;
using Microsoft.EntityFrameworkCore;
using YellowphaseWebsite.Models.ViewModels;
using YellowphaseWebsite.Services;
using YellowphaseWebsite.Models;
using System;

namespace YellowPhaseWebsite.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly PricingService _pricingService;
        private readonly GeoService _geoService;
        private readonly MetaCapiService _meta;

        public CheckoutController(
            ApplicationDbContext db,
            PricingService pricingService,
            GeoService geoService,
            MetaCapiService meta)
        {
            _db = db;
            _pricingService = pricingService;
            _geoService = geoService;
            _meta = meta;
        }

        // ===============================
        // GET: /Checkout?productId=1
        // ===============================
        [HttpGet]
        public async Task<IActionResult> Index(int productId, int quantity = 1, string currency = null)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product == null) return NotFound();

            currency ??= await _geoService.GetCurrencyAsync(
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? ""
            );

            var converted = await _pricingService.ConvertAsync(product.Price, currency);

            var vm = new CheckoutViewModel
            {
                ProductId = product.Id,
                Name = product.Name,
                ItemName = product.Name,
                Quantity = quantity,
                Amount = converted.Price * quantity,
                Currency = converted.Currency,
                ItemType = product.ProductType
            };

            vm.DisplayPrice = vm.Amount.ToString("C", GetCultureForCurrency(converted.Currency));

            // ===============================
            // META: Initiate Checkout Event
            // ===============================
            var checkoutEventId = Guid.NewGuid().ToString();

            await _meta.SendEvent(
                "InitiateCheckout",
                vm.Amount,
                vm.Currency,
                "",
                checkoutEventId
            );

            return View(vm);
        }

        // ===============================
        // POST: Submit Order
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var product = await _db.Products
                .FirstOrDefaultAsync(p => p.Name == model.ItemName);

            var order = new Order
            {
                ProductId = product?.Id ?? 0,
                ItemName = model.ItemName,
                ItemType = model.ItemType,
                Amount = model.Amount,
                Quantity = model.Quantity,
                CustomerName = model.CustomerName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CompanyName = model.CompanyName,
                Notes = model.Notes
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Success));
        }

        // ===============================
        // GET: Success Page
        // ===============================
        public IActionResult Success()
        {
            return View();
        }

        // ===============================
        // POST: PayPal Callback Handler
        // ===============================
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PayPalPaymentDto model, int productId)
        {
            var product = await _db.Products.FindAsync(productId);

            if (product == null)
                return BadRequest("Invalid product.");

            var order = new Order
            {
                ProductId = product.Id,
                ItemName = product.Name,
                ItemType = product.ProductType,
                Amount = product.Price,
                Quantity = model.Quantity,
                CustomerName = model.PayerName,
                Email = model.PayerEmail,
                PhoneNumber = "",
                Status = "Paid",
                CreatedAt = DateTime.UtcNow
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            // ===============================
            // META: Purchase Event (LIVE)
            // ===============================
            var purchaseEventId = Guid.NewGuid().ToString();

            await _meta.SendEvent(
                eventName: "Purchase",
                value: product.Price * model.Quantity,
                currency: "ZMW",
                email: model.PayerEmail,
                eventId: purchaseEventId
            // ❌ testEventCode REMOVED for LIVE
            );

            return Ok(new { status = "ok" });
        }

        // ===============================
        // Currency Formatting Helper
        // ===============================
        private System.Globalization.CultureInfo GetCultureForCurrency(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                return System.Globalization.CultureInfo.CurrentCulture;

            switch (currencyCode.ToUpperInvariant())
            {
                case "USD":
                    return new System.Globalization.CultureInfo("en-US");

                case "ZMW":
                    try
                    {
                        return new System.Globalization.CultureInfo("en-ZM");
                    }
                    catch
                    {
                        var ci = (System.Globalization.CultureInfo)
                            System.Globalization.CultureInfo.InvariantCulture.Clone();

                        var nf = (System.Globalization.NumberFormatInfo)ci.NumberFormat.Clone();
                        nf.CurrencySymbol = "K";
                        ci.NumberFormat = nf;

                        return ci;
                    }

                default:
                    var fallback = (System.Globalization.CultureInfo)
                        System.Globalization.CultureInfo.CurrentCulture.Clone();

                    var nf2 = (System.Globalization.NumberFormatInfo)fallback.NumberFormat.Clone();
                    nf2.CurrencySymbol = currencyCode + " ";
                    fallback.NumberFormat = nf2;

                    return fallback;
            }
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> TestProcessPayment(int productId, int quantity = 1)
        {
            var product = await _db.Products.FindAsync(productId);

            if (product == null)
                return BadRequest("Invalid product.");

            var order = new Order
            {
                ProductId = product.Id,
                ItemName = product.Name,
                ItemType = product.ProductType,
                Amount = product.Price,
                Quantity = quantity,
                CustomerName = "TEST USER",
                Email = "test@local.com",
                PhoneNumber = "",
                Status = "Paid (TEST)",
                CreatedAt = DateTime.UtcNow
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            // 🔥 Trigger Meta CAPI without PayPal
            await _meta.SendEvent(
                "Purchase",
                product.Price * quantity,
                "ZMW",
                "test@local.com",
                Guid.NewGuid().ToString(),
                "TEST45057"
            );

            return Ok(new { status = "test success" });
        }

    }
}