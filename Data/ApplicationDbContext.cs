using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YellowPhase.Models;
using YellowphaseWebsite.Models;

namespace YellowphaseWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // =========================
        // DBSets
        // =========================
        public DbSet<ContactLead> ContactLeads { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
     

        public DbSet<AppSetting> AppSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // =========================
            // PRECISION RULES
            // =========================
            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Entity<Order>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            // =========================
            // PRODUCT CONFIGURATION (IMPORTANT FIX)
            // =========================
            builder.Entity<Product>()
                .Property(p => p.ProductCode)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Entity<Product>()
                .Property(p => p.ProductType)
                .HasMaxLength(20);

            builder.Entity<Product>()
                .Property(p => p.Category)
                .HasMaxLength(50);

            builder.Entity<Product>()
                .Property(p => p.ImageUrl)
                .HasMaxLength(255);

            // =========================
            // CONTACT LEAD CONFIG
            // =========================
            builder.Entity<ContactLead>()
                .Property(c => c.Name)
                .HasMaxLength(150);

            builder.Entity<ContactLead>()
                .Property(c => c.Email)
                .HasMaxLength(150);

            builder.Entity<ContactLead>()
                .Property(c => c.Message)
                .HasMaxLength(1000);

            // =========================
            // SEED PRODUCTS (FIXED + CONSISTENT)
            // =========================
            builder.Entity<Product>().HasData(

      // ===================== SOLAR (3) =====================
      new Product
      {
          Id = 1,
          Name = "Solar Panel 550W",
          Description = "High efficiency mono-crystalline solar panel.",
          Price = 3200,
          Category = "Solar",
          ProductType = "Product",
          ProductCode = "SOLAR-550W",
          ImageUrl = "/images/products/solar1.webp",
          IsUserSubmitted = false,
          IsApproved = true,
          CreatedAt = new DateTime(2026, 06, 03)
        
      },

      new Product
      {
          Id = 2,
          Name = "Hybrid Inverter 8kW",
          Description = "Hybrid inverter for solar systems.",
          Price = 18500,
          Category = "Solar",
          ProductType = "Product",
          ProductCode = "INVERTER-8KW",
          ImageUrl = "/images/products/inverter1.png",
          IsUserSubmitted = false,
          IsApproved = true,
          CreatedAt = new DateTime(2026, 06, 03)
         
      },

      new Product
      {
          Id = 3,
          Name = "Lithium Battery 10kWh",
          Description = "Energy storage battery system.",
          Price = 45000,
          Category = "Solar",
          ProductType = "Product",
          ProductCode = "BATTERY-10KWH",
          ImageUrl = "/images/products/battery1.png",
          IsUserSubmitted = false,
          IsApproved = true,
          CreatedAt = new DateTime(2026, 06, 03)
        
      },

      // ===================== ELECTRICAL (4) =====================
      new Product { Id = 4, Name = "Distribution Board", Description = "DB Box", Price = 1200, Category = "Electrical", ProductType = "Product", ProductCode = "DB-BOARD", ImageUrl = "/images/products/db1.webp", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)},

      new Product { Id = 5, Name = "Electrical Cable 2.5mm", Description = "Copper cable", Price = 35, Category = "Electrical", ProductType = "Product", ProductCode = "CABLE-2.5", ImageUrl = "/images/products/cable1.jpg", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)},

      new Product {Id = 6, Name = "LED Flood Light 200W", Description = "Outdoor lighting", Price = 950, Category = "Electrical", ProductType = "Product", ProductCode = "LED-200W", ImageUrl = "/images/products/light1.jpg", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03) },

      new Product { Id = 7, Name = "CCTV Camera Kit", Description = "Security system", Price = 6200, Category = "ICT", ProductType = "Product", ProductCode = "CCTV-KIT", ImageUrl = "/images/products/cctv1.webp", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)},

      // ===================== MECHANICAL (2) =====================
      new Product { Id = 8, Name = "Water Pump 1.5HP", Description = "Water pump", Price = 3800, Category = "Mechanical", ProductType = "Product", ProductCode = "PUMP-1.5HP", ImageUrl = "/images/products/pump1.jpg", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)},

      new Product { Id = 9, Name = "Air Compressor 50L", Description = "Workshop compressor", Price = 5200, Category = "Mechanical", ProductType = "Product", ProductCode = "COMP-50L", ImageUrl = "/images/products/compressor1.png", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)},

      // ===================== ICT (2) =====================
      new Product { Id = 10, Name = "Networking Router Pro", Description = "Business router", Price = 2800, Category = "ICT", ProductType = "Product", ProductCode = "ROUTER-PRO", ImageUrl = "/images/products/router1.webp", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)},

      new Product { Id = 11, Name = "Laptop Repair Service", Description = "ICT service", Price = 800, Category = "ICT", ProductType = "Service", ProductCode = "LAPTOP-REPAIR", ImageUrl = "/images/services/it.jpg", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03), },

      // ===================== SOFTWARE PRODUCTS (3) =====================
      new Product { Id = 12, Name = "Village Banking Suite", Description = "System", Price = 25000, Category = "Software", ProductType = "Product", ProductCode = "VILLAGE-BANKING", ImageUrl = "/images/products/village-banking.jpg", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)},

      new Product { Id = 13, Name = "Complaint System", Description = "Gov system", Price = 30000, Category = "Software", ProductType = "Product", ProductCode = "COMPLAINT-SYS", ImageUrl = "/images/products/complaint.jpg", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)},

      new Product { Id = 14, Name = "Wedding Platform", Description = "Event system", Price = 5000, Category = "Web", ProductType = "Product", ProductCode = "WEDDING-APP", ImageUrl = "/images/products/wedding.jpg", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)},

      // ===================== SERVICES (4) =====================
      new Product { Id = 15, Name = "App Development", Description = "Software service", Price = 15000, Category = "Service", ProductType = "Service", ProductCode = "APP-DEV", ImageUrl = "/images/services/app-dev.jpg", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)},

      new Product { Id = 16, Name = "Cloud Hosting", Description = "Hosting service", Price = 2000, Category = "Service", ProductType = "Service", ProductCode = "CLOUD-HOST", ImageUrl = "/images/services/cloud.jpg", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)},

      new Product {Id = 17, Name = "System Upgrades", Description = "Upgrade service", Price = 10000, Category = "Service", ProductType = "Service", ProductCode = "SYS-UPGRADE", ImageUrl = "/images/services/upgrade.jpg", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03) },

      new Product { Id = 18, Name = "IT Maintenance", Description = "Support service", Price = 5000, Category = "Service", ProductType = "Service", ProductCode = "IT-MAINT", ImageUrl = "/images/services/it.jpg", IsApproved = true, IsUserSubmitted = false, CreatedAt = new DateTime(2026, 06, 03)}
      
      
             );
        }
    }
}