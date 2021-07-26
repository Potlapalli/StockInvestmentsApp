using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInvestments.API.Migrations
{
    public partial class SetSoldPositionForeignKeyNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldPositions_CurrentPositions_Ticker",
                table: "SoldPositions");

            migrationBuilder.AlterColumn<string>(
                name: "Ticker",
                table: "SoldPositions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SoldPositions_CurrentPositions_Ticker",
                table: "SoldPositions",
                column: "Ticker",
                principalTable: "CurrentPositions",
                principalColumn: "Ticker",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldPositions_CurrentPositions_Ticker",
                table: "SoldPositions");

            migrationBuilder.AlterColumn<string>(
                name: "Ticker",
                table: "SoldPositions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_SoldPositions_CurrentPositions_Ticker",
                table: "SoldPositions",
                column: "Ticker",
                principalTable: "CurrentPositions",
                principalColumn: "Ticker",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
