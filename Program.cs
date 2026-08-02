using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YellowphaseWebsite.Data;
using YellowphaseWebsite.Models;
using YellowphaseWebsite.Services;
using YellowphaseWebsite.Models.Configurations;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


// =====================================================
// CONFIGURATION
// =====================================================

builder.Services.Configure<PayPalSettings>(
    builder.Configuration.GetSection("PayPal"));

builder.Services.Configure<ZanacoSettings>(
    builder.Configuration.GetSection("Zanaco"));

builder.Services.AddMemoryCache();


// =====================================================
// APPLICATION SERVICES
// =====================================================

builder.Services.AddScoped<PricingService>();
builder.Services.AddScoped<IRsaSignatureService, RsaSignatureService>();


// =====================================================
// HTTP CLIENT SERVICES
// =====================================================

builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>();
builder.Services.AddHttpClient<TikTokEventService>();
builder.Services.AddHttpClient<GeoService>();
builder.Services.AddHttpClient<MetaCapiService>();

builder.Services.AddScoped<MetaCapiService>();

builder.Services.AddHttpClient<IZanacoService, ZanacoService>();


// =====================================================
// IDENTITY
// =====================================================

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// =====================================================
// MVC
// =====================================================

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


// =====================================================
// ERROR HANDLING
// =====================================================

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


// =====================================================
// DATABASE MIGRATION & SEEDING
// =====================================================

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();

        await db.Database.MigrateAsync();

        await RoleSeeder.SeedRoles(services);
        await RoleSeeder.SeedAdminUser(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Database migration/seed failed during startup.");
    }
}


// =====================================================
// MIDDLEWARE
// =====================================================

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


// =====================================================
// ROUTING
// =====================================================

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();