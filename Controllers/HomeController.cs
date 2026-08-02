using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YellowphaseWebsite.Data;
using YellowphaseWebsite.Models;
using YellowphaseWebsite.Models.ViewModels;
using YellowphaseWebsite.Services;
using YellowphaseWebsite.ViewModels;

namespace YellowphaseWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly PricingService _pricingService;
        private readonly GeoService _geoService;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, PricingService pricingService, GeoService geoService, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger)
        {
            _db = db;
            _pricingService = pricingService;
            _geoService = geoService;
            _logger = logger;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
            }

            return View();
        }

        public async Task<IActionResult> Solutions()
        {
            // Load featured products by Ids used on the Solutions page (only website packages)
            var ids = new[] { 19, 20, 21 };
            var products = _db.Products.Where(p => ids.Contains(p.Id) && p.IsApproved).ToList();

            var currency = await _geoService.GetCurrencyAsync(HttpContext.Connection.RemoteIpAddress?.ToString() ?? "");

            var convertTasks = products.Select(async p =>
            {
                var converted = await _pricingService.ConvertAsync(p.Price, currency);
                var vm = new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = converted.Price,
                    Currency = converted.Currency,
                    Category = p.Category
                };

                // Compute a culture-aware display string for the currency
                var culture = GetCultureForCurrency(converted.Currency);
                vm.DisplayPrice = converted.Price.ToString("C", culture);
                return vm;
            }).ToArray();

            var vmList = (await Task.WhenAll(convertTasks)).ToList();

            // Log model contents for debugging
            foreach (var vm in vmList)
            {
                _logger.LogInformation("Solutions VM: Id={Id} Price={Price} Currency={Currency} DisplayPrice={DisplayPrice}", vm.Id, vm.Price, vm.Currency, vm.DisplayPrice);
            }

            return View(vmList);
        }

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
                        var ci = new System.Globalization.CultureInfo("en-ZM");
                        return ci;
                    }
                    catch
                    {
                        var ci = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.InvariantCulture.Clone();
                        var nf = (System.Globalization.NumberFormatInfo)ci.NumberFormat.Clone();
                        nf.CurrencySymbol = "K";
                        ci.NumberFormat = nf;
                        return ci;
                    }
                default:
                    var fallback = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture.Clone();
                    var nf2 = (System.Globalization.NumberFormatInfo)fallback.NumberFormat.Clone();
                    nf2.CurrencySymbol = currencyCode + " ";
                    fallback.NumberFormat = nf2;
                    return fallback;
            }
        }

        public async Task<IActionResult> About()
        {

            return View();

        }
        public async Task<IActionResult> Contact()
        {

            return View();

        }

        public async Task<IActionResult> Services()
        {

            return View();

        }

        public async Task<IActionResult> Projects()
        {

            return View();

        }
        public async Task<IActionResult> SolarPackages()
        {
            // 1. Fetch raw datasets from database
            var rawResidential = await _db.Products
                .Where(p => p.Category == "Solar Package")
                .OrderBy(p => p.Price)
                .ToListAsync();

            var rawCommercial = await _db.Products
                .Where(p => p.Category == "Commercial Solar")
                .ToListAsync();

            var viewModel = new SolarPackagesViewModel();

            // 2. Map Residential Packages + Local Images
            foreach (var product in rawResidential)
            {
                var images = GetProductImages(product.ProductCode, product.ImageUrl);
                viewModel.ResidentialPackages.Add((product, images));
            }

            // 3. Map Commercial Packages + Local Images
            foreach (var product in rawCommercial)
            {
                var images = GetProductImages(product.ProductCode, product.ImageUrl);
                viewModel.CommercialPackages.Add((product, images));
            }

            return View(viewModel);
        }

        // Helper method to look into the folder for matching files
        private List<string> GetProductImages(string productCode, string defaultImageUrl)
        {
            var imagesList = new List<string>();

            if (string.IsNullOrEmpty(productCode))
            {
                if (!string.IsNullOrEmpty(defaultImageUrl)) imagesList.Add(defaultImageUrl);
                return imagesList;
            }

            // Define your images directory path inside wwwroot
            string relativeFolder = "/images/packages/";
            string absoluteFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "packages");

            if (Directory.Exists(absoluteFolder))
            {
                // Find all files that start with the ProductCode (e.g., "SOL-2KW")
                var matchedFiles = Directory.GetFiles(absoluteFolder, $"{productCode}*")
                                            .Select(Path.GetFileName)
                                            .OrderBy(f => f)
                                            .ToList();

                if (matchedFiles.Any())
                {
                    foreach (var file in matchedFiles)
                    {
                        imagesList.Add($"{relativeFolder}{file}");
                    }
                    return imagesList;
                }
            }

            // Fallback: If no files found in folder matching the code, use the DB column fallback
            if (!string.IsNullOrEmpty(defaultImageUrl))
            {
                imagesList.Add(defaultImageUrl);
            }

            return imagesList;
        }
    }
}