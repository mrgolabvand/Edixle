using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Infrastructure.EFCore.Migrations
{
    public partial class AddManyToManyRelationBetweenPortfolioAndPortoflioCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_PortfolioCategories_CategoryId",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_CategoryId",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Portfolios");

            migrationBuilder.CreateTable(
                name: "PortfolioAndCategoryLinks",
                columns: table => new
                {
                    PortfolioId = table.Column<long>(type: "bigint", nullable: false),
                    PortfolioCategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAndCategoryLinks", x => new { x.PortfolioId, x.PortfolioCategoryId });
                    table.ForeignKey(
                        name: "FK_PortfolioAndCategoryLinks_PortfolioCategories_PortfolioCategoryId",
                        column: x => x.PortfolioCategoryId,
                        principalTable: "PortfolioCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioAndCategoryLinks_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioAndCategoryLinks_PortfolioCategoryId",
                table: "PortfolioAndCategoryLinks",
                column: "PortfolioCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioAndCategoryLinks");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Portfolios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_CategoryId",
                table: "Portfolios",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_PortfolioCategories_CategoryId",
                table: "Portfolios",
                column: "CategoryId",
                principalTable: "PortfolioCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
