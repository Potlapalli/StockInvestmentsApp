using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInvestments.API.Migrations
{
    public partial class SetSoldPositionForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldPositions_CurrentPositions_CurrentPositionTicker",
                table: "SoldPositions");

            migrationBuilder.DropIndex(
                name: "IX_SoldPositions_CurrentPositionTicker",
                table: "SoldPositions");

            migrationBuilder.DropColumn(
                name: "CurrentPositionTicker",
                table: "SoldPositions");

            migrationBuilder.AlterColumn<string>(
                name: "Ticker",
                table: "SoldPositions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoldPositions_Ticker",
                table: "SoldPositions",
                column: "Ticker");

            migrationBuilder.AddForeignKey(
                name: "FK_SoldPositions_CurrentPositions_Ticker",
                table: "SoldPositions",
                column: "Ticker",
                principalTable: "CurrentPositions",
                principalColumn: "Ticker",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldPositions_CurrentPositions_Ticker",
                table: "SoldPositions");

            migrationBuilder.DropIndex(
                name: "IX_SoldPositions_Ticker",
                table: "SoldPositions");

            migrationBuilder.AlterColumn<string>(
                name: "Ticker",
                table: "SoldPositions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentPositionTicker",
                table: "SoldPositions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoldPositions_CurrentPositionTicker",
                table: "SoldPositions",
                column: "CurrentPositionTicker");

            migrationBuilder.AddForeignKey(
                name: "FK_SoldPositions_CurrentPositions_CurrentPositionTicker",
                table: "SoldPositions",
                column: "CurrentPositionTicker",
                principalTable: "CurrentPositions",
                principalColumn: "Ticker",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
