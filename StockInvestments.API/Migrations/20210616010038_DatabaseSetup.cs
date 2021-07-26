using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInvestments.API.Migrations
{
    public partial class DatabaseSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClosedPositions",
                columns: table => new
                {
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedPositions", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "CurrentPositions",
                columns: table => new
                {
                    Ticker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchasePrice = table.Column<float>(type: "real", nullable: false),
                    TotalShares = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentPositions", x => x.Ticker);
                });

            migrationBuilder.CreateTable(
                name: "SoldPositions",
                columns: table => new
                {
                    Number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellingPrice = table.Column<float>(type: "real", nullable: false),
                    TotalShares = table.Column<float>(type: "real", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentPositionTicker = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldPositions", x => x.Number);
                    table.ForeignKey(
                        name: "FK_SoldPositions_CurrentPositions_CurrentPositionTicker",
                        column: x => x.CurrentPositionTicker,
                        principalTable: "CurrentPositions",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoldPositions_CurrentPositionTicker",
                table: "SoldPositions",
                column: "CurrentPositionTicker");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosedPositions");

            migrationBuilder.DropTable(
                name: "SoldPositions");

            migrationBuilder.DropTable(
                name: "CurrentPositions");
        }
    }
}
