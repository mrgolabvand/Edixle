using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletManagement.Infrastructure.EFCore.Migrations
{
    public partial class AddIsSuccessPropToTransactionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "Transactions");
        }
    }
}
