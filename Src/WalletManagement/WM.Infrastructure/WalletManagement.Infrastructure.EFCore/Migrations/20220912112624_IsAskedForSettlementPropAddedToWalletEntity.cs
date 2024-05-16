using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletManagement.Infrastructure.EFCore.Migrations
{
    public partial class IsAskedForSettlementPropAddedToWalletEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAskedForSettlement",
                table: "Wallets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAskedForSettlement",
                table: "Wallets");
        }
    }
}
