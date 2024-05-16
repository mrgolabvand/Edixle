using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Infrastructure.EFCore.Migrations
{
    public partial class AddJobHistoryAndRemoveSomePropertyFromPersonalPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Card",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "City",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "PortfolioDescription",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "SkillsDescription",
                table: "PersonalPages");

            migrationBuilder.RenameColumn(
                name: "Resume",
                table: "PersonalPages",
                newName: "MetaDescription");

            migrationBuilder.RenameColumn(
                name: "Linkdin",
                table: "PersonalPages",
                newName: "Linkedin");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "PersonalPages",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "PersonalPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaxPay",
                table: "PersonalPages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinPay",
                table: "PersonalPages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayDate",
                table: "PersonalPages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EmployerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PageId = table.Column<long>(type: "bigint", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobHistories_PersonalPages_PageId",
                        column: x => x.PageId,
                        principalTable: "PersonalPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobHistories_PageId",
                table: "JobHistories",
                column: "PageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobHistories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "MaxPay",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "MinPay",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "PayDate",
                table: "PersonalPages");

            migrationBuilder.RenameColumn(
                name: "MetaDescription",
                table: "PersonalPages",
                newName: "Resume");

            migrationBuilder.RenameColumn(
                name: "Linkedin",
                table: "PersonalPages",
                newName: "Linkdin");

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "PersonalPages",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Card",
                table: "PersonalPages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PersonalPages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "PersonalPages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "PersonalPages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortfolioDescription",
                table: "PersonalPages",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkillsDescription",
                table: "PersonalPages",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);
        }
    }
}
