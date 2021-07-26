using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInvestments.API.Migrations
{
    public partial class SeedInitialSoldPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CurrentPositions",
                columns: new[] { "Ticker", "Company", "PurchasePrice", "TotalShares" },
                values: new object[] { "OSTK", "Overstock", 91.629999999999995, 20.0 });

            migrationBuilder.InsertData(
                table: "SoldPositions",
                columns: new[] { "Number", "CurrentPositionTicker", "SellingPrice", "Ticker", "TotalShares" },
                values: new object[] { 1L, null, 91.769999999999996, "OSTK", 10.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "OSTK");

            migrationBuilder.DeleteData(
                table: "SoldPositions",
                keyColumn: "Number",
                keyValue: 1L);
        }
    }
}
