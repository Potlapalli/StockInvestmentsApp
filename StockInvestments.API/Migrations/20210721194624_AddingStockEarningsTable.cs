using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInvestments.API.Migrations
{
    public partial class AddingStockEarningsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockEarnings",
                columns: table => new
                {
                    Ticker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EarningsDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockEarnings", x => x.Ticker);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockEarnings");
        }
    }
}
