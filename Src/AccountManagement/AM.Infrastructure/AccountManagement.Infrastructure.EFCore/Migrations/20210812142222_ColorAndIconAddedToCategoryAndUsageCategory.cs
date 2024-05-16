using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Infrastructure.EFCore.Migrations
{
    public partial class ColorAndIconAddedToCategoryAndUsageCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "PortfolioUsageCategories",
                type: "nvarchar(55)",
                maxLength: 55,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "PortfolioUsageCategories",
                type: "nvarchar(55)",
                maxLength: 55,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "PortfolioCategories",
                type: "nvarchar(55)",
                maxLength: 55,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "PortfolioCategories",
                type: "nvarchar(55)",
                maxLength: 55,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "PortfolioUsageCategories");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "PortfolioUsageCategories");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "PortfolioCategories");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "PortfolioCategories");
        }
    }
}
