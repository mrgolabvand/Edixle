using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatManagement.Infrastructure.EFCore.Migrations
{
    public partial class AddTitleAndPricePropsToChatRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ChatRooms",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ChatRooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ChatRoomOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployerPageId = table.Column<long>(type: "bigint", nullable: false),
                    ChatRoomId = table.Column<long>(type: "bigint", nullable: false),
                    PayAmount = table.Column<double>(type: "float", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false),
                    RefId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoomOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatRoomOrders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ChatRooms");
        }
    }
}
