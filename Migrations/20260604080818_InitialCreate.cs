using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YellowphaseWebsite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactLeads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactLeads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SellerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUserSubmitted = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ImageUrl", "IsApproved", "IsUserSubmitted", "Name", "Price", "ProductCode", "ProductType", "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[,]
                {
                    { 1, "Solar", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "High efficiency mono-crystalline solar panel.", "/images/products/solar1.webp", true, false, "Solar Panel 550W", 3200m, "SOLAR-550W", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 2, "Solar", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hybrid inverter for solar systems.", "/images/products/inverter1.png", true, false, "Hybrid Inverter 8kW", 18500m, "INVERTER-8KW", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 3, "Solar", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Energy storage battery system.", "/images/products/battery1.png", true, false, "Lithium Battery 10kWh", 45000m, "BATTERY-10KWH", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 4, "Electrical", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "DB Box", "/images/products/db1.webp", true, false, "Distribution Board", 1200m, "DB-BOARD", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 5, "Electrical", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Copper cable", "/images/products/cable1.jpg", true, false, "Electrical Cable 2.5mm", 35m, "CABLE-2.5", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 6, "Electrical", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor lighting", "/images/products/light1.jpg", true, false, "LED Flood Light 200W", 950m, "LED-200W", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 7, "ICT", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Security system", "/images/products/cctv1.webp", true, false, "CCTV Camera Kit", 6200m, "CCTV-KIT", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 8, "Mechanical", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water pump", "/images/products/pump1.jpg", true, false, "Water Pump 1.5HP", 3800m, "PUMP-1.5HP", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 9, "Mechanical", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Workshop compressor", "/images/products/compressor1.png", true, false, "Air Compressor 50L", 5200m, "COMP-50L", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 10, "ICT", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business router", "/images/products/router1.webp", true, false, "Networking Router Pro", 2800m, "ROUTER-PRO", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 11, "ICT", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "ICT service", "/images/services/it.jpg", true, false, "Laptop Repair Service", 800m, "LAPTOP-REPAIR", "Service", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 12, "Software", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "/images/products/village-banking.jpg", true, false, "Village Banking Suite", 25000m, "VILLAGE-BANKING", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 13, "Software", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gov system", "/images/products/complaint.jpg", true, false, "Complaint System", 30000m, "COMPLAINT-SYS", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 14, "Web", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Event system", "/images/products/wedding.jpg", true, false, "Wedding Platform", 5000m, "WEDDING-APP", "Product", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 15, "Service", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Software service", "/images/services/app-dev.jpg", true, false, "App Development", 15000m, "APP-DEV", "Service", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 16, "Service", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hosting service", "/images/services/cloud.jpg", true, false, "Cloud Hosting", 2000m, "CLOUD-HOST", "Service", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 17, "Service", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upgrade service", "/images/services/upgrade.jpg", true, false, "System Upgrades", 10000m, "SYS-UPGRADE", "Service", "info@yellowphase.org", "YellowPhase", "0962227741" },
                    { 18, "Service", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Support service", "/images/services/it.jpg", true, false, "IT Maintenance", 5000m, "IT-MAINT", "Service", "info@yellowphase.org", "YellowPhase", "0962227741" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContactLeads");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
