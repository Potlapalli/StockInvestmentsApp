using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInvestments.API.Migrations
{
    public partial class Test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "OSTK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CurrentPositions",
                columns: new[] { "Ticker", "Company", "PurchasePrice", "TotalShares" },
                values: new object[] { "OSTK", "Overstock", 91.629999999999995, 20.0 });
        }
    }
}
