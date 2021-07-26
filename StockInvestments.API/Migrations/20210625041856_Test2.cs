using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInvestments.API.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SoldPositions",
                keyColumn: "Number",
                keyValue: 1L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SoldPositions",
                columns: new[] { "Number", "CurrentPositionTicker", "SellingPrice", "Ticker", "TotalShares" },
                values: new object[] { 1L, null, 91.769999999999996, "OSTK", 10.0 });
        }
    }
}
