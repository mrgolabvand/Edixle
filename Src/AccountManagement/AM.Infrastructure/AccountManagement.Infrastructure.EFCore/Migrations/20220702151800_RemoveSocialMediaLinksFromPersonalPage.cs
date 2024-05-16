using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Infrastructure.EFCore.Migrations
{
    public partial class RemoveSocialMediaLinksFromPersonalPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "Linkedin",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "Telegram",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "PersonalPages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "PersonalPages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Linkedin",
                table: "PersonalPages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telegram",
                table: "PersonalPages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "PersonalPages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
