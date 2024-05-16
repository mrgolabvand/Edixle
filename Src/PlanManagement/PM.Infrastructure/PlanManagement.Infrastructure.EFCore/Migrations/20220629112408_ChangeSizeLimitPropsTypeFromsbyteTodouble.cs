using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanManagement.Infrastructure.EFCore.Migrations
{
    public partial class ChangeSizeLimitPropsTypeFromsbyteTodouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PortfolioUploadSizeLimit",
                table: "EditorPlans",
                type: "float",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<double>(
                name: "ChatUploadSizeLimit",
                table: "EditorPlans",
                type: "float",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "PortfolioUploadSizeLimit",
                table: "EditorPlans",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<short>(
                name: "ChatUploadSizeLimit",
                table: "EditorPlans",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
