using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Infrastructure.EFCore.Migrations
{
    public partial class AddEditorPlanPropsToPersonalPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JudgesRate",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "PeopleRate",
                table: "Portfolios");

            migrationBuilder.AddColumn<short>(
                name: "ChatUploadSizeLimit",
                table: "PersonalPages",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "PersonalPages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<short>(
                name: "PortfolioUploadSizeLimit",
                table: "PersonalPages",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "VipProjectOfferCount",
                table: "PersonalPages",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatUploadSizeLimit",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "PortfolioUploadSizeLimit",
                table: "PersonalPages");

            migrationBuilder.DropColumn(
                name: "VipProjectOfferCount",
                table: "PersonalPages");

            migrationBuilder.AddColumn<int>(
                name: "JudgesRate",
                table: "Portfolios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeopleRate",
                table: "Portfolios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
