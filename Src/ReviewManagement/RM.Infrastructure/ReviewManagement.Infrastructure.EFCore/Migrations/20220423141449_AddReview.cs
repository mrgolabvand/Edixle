using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewManagement.Infrastructure.EFCore.Migrations
{
    public partial class AddReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Strength = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Weakness = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Rate = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    IsHelpful = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    IsHarmful = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    BuildQuality = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    WorthThePurchase = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    Innovation = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    Options = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    EaseOfUse = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    Design = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    OwnerRecordId = table.Column<long>(type: "bigint", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
