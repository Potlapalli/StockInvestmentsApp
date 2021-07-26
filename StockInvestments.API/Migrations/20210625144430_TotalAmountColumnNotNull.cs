using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInvestments.API.Migrations
{
    public partial class TotalAmountColumnNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "AAPL",
                column: "TotalAmount",
                value: 710.45000000000005);

            migrationBuilder.UpdateData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "ETSY",
                column: "TotalAmount",
                value: 409.57999999999998);

            migrationBuilder.UpdateData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "NKLA",
                column: "TotalAmount",
                value: 163.25);

            migrationBuilder.UpdateData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "OSTK",
                column: "TotalAmount",
                value: 1832.5999999999999);

            migrationBuilder.UpdateData(
                table: "SoldPositions",
                keyColumn: "Number",
                keyValue: 1L,
                column: "TotalAmount",
                value: 917.70000000000005);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "AAPL",
                column: "TotalAmount",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "ETSY",
                column: "TotalAmount",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "NKLA",
                column: "TotalAmount",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "CurrentPositions",
                keyColumn: "Ticker",
                keyValue: "OSTK",
                column: "TotalAmount",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "SoldPositions",
                keyColumn: "Number",
                keyValue: 1L,
                column: "TotalAmount",
                value: 0.0);
        }
    }
}
