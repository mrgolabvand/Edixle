using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanManagement.Infrastructure.EFCore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EditorPlanOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EditorPageId = table.Column<long>(type: "bigint", nullable: false),
                    EditorPlanId = table.Column<long>(type: "bigint", nullable: false),
                    PayAmount = table.Column<double>(type: "float", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false),
                    RefId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditorPlanOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EditorPlans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ExpireDays = table.Column<short>(type: "smallint", nullable: false),
                    ChatUploadSizeLimit = table.Column<short>(type: "smallint", nullable: false),
                    PortfolioUploadSizeLimit = table.Column<short>(type: "smallint", nullable: false),
                    VipProjectOfferCount = table.Column<short>(type: "smallint", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditorPlans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditorPlanOrders");

            migrationBuilder.DropTable(
                name: "EditorPlans");
        }
    }
}
