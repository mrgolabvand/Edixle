using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Infrastructure.EFCore.Migrations
{
    public partial class RelationWithPortfolioAddedToPortfolioDownload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PortfolioDownloads_PortfolioId",
                table: "PortfolioDownloads",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioDownloads_Portfolios_PortfolioId",
                table: "PortfolioDownloads",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioDownloads_Portfolios_PortfolioId",
                table: "PortfolioDownloads");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioDownloads_PortfolioId",
                table: "PortfolioDownloads");
        }
    }
}
