using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletManagement.Infrastructure.EFCore.Migrations
{
    public partial class RemoveIsSuccessPropFromWalletEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "Wallets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "Wallets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
