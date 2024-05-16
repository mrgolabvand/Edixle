using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatManagement.Infrastructure.EFCore.Migrations
{
    public partial class AddIsPayedPropToChatRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "ChatRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "ChatRooms");
        }
    }
}
