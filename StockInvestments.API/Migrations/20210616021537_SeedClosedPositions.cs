using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInvestments.API.Migrations
{
    public partial class SeedClosedPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClosedPositions",
                columns: new[] { "Number", "Company", "FinalValue", "Ticker" },
                values: new object[] { 1L, "Novavax", 185.0, "NVAX" });

            migrationBuilder.InsertData(
                table: "ClosedPositions",
                columns: new[] { "Number", "Company", "FinalValue", "Ticker" },
                values: new object[] { 2L, "WayFair", -3.8098999999999998, "W" });

            migrationBuilder.InsertData(
                table: "ClosedPositions",
                columns: new[] { "Number", "Company", "FinalValue", "Ticker" },
                values: new object[] { 3L, "Shopify", 10.54946, "SHOP" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClosedPositions",
                keyColumn: "Number",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ClosedPositions",
                keyColumn: "Number",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ClosedPositions",
                keyColumn: "Number",
                keyValue: 3L);
        }
    }
}
