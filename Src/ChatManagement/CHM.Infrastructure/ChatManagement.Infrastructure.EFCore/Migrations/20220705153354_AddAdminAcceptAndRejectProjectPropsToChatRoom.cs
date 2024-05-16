using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatManagement.Infrastructure.EFCore.Migrations
{
    public partial class AddAdminAcceptAndRejectProjectPropsToChatRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdminJudgmentIsAcceptProject",
                table: "ChatRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AdminJudgmentIsRejectProject",
                table: "ChatRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminJudgmentIsAcceptProject",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "AdminJudgmentIsRejectProject",
                table: "ChatRooms");
        }
    }
}
