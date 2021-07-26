using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInvestments.API.Migrations
{
    public partial class SeedCurrentPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CurrentPositions",
                columns: new[] { "Ticker", "Company", "PurchasePrice", "TotalShares" },
                values: new object[] { "NKLA", "Nikola", 81.625, 2.0 });

            migrationBuilder.InsertData(
                table: "CurrentPositions",
                columns: new[] { "Ticker", "Company", "PurchasePrice", "TotalShares" },
                values: new object[] { "ETSY", "Etsy", 204.78999999999999, 2.0 });

            migrationBuilder.InsertData(
                table: "CurrentPositions",
                columns: new[] { "Ticker", "Company", "PurchasePrice", "TotalShares" },
                values: new object[] { "AAPL", "Apple", 142.09, 5.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "AAPL");

            migrationBuilder.DeleteData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "ETSY");

            migrationBuilder.DeleteData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "NKLA");
        }
    }
}
