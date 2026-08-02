using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YellowphaseWebsite.Migrations
{
    /// <inheritdoc />
    public partial class addseller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerEmail",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SellerName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SellerPhone",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sellers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Quantity", "SellerId" },
                values: new object[] { 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_UserId",
                table: "Sellers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sellers_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sellers_SellerId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Products_SellerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "SellerEmail",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SellerName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SellerPhone",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "SellerEmail", "SellerName", "SellerPhone" },
                values: new object[] { "info@yellowphase.org", "YellowPhase", "0962227741" });
        }
    }
}
