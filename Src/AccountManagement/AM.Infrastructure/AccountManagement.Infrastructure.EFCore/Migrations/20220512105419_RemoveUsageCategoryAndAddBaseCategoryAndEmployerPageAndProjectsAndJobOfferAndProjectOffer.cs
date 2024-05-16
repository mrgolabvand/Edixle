using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Infrastructure.EFCore.Migrations
{
    public partial class RemoveUsageCategoryAndAddBaseCategoryAndEmployerPageAndProjectsAndJobOfferAndProjectOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_PortfolioUsageCategories_UsageCategoryId",
                table: "Portfolios");

            migrationBuilder.DropTable(
                name: "PortfolioUsageCategories");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_UsageCategoryId",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "UsageCategoryId",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "PortfolioCategories");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "PortfolioCategories");

            migrationBuilder.RenameColumn(
                name: "File",
                table: "Portfolios",
                newName: "Video");

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

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "PortfolioCategories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureAlt",
                table: "PortfolioCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureTitle",
                table: "PortfolioCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PortfolioBaseCategoryId",
                table: "PortfolioCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsBusy",
                table: "PersonalPages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "EmployerPages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerPages_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioBaseCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PictureAlt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioBaseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobOffers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EditorPageId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerPageId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsAccept = table.Column<bool>(type: "bit", nullable: false),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOffers_EmployerPages_EmployerPageId",
                        column: x => x.EmployerPageId,
                        principalTable: "EmployerPages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobOffers_PersonalPages_EditorPageId",
                        column: x => x.EditorPageId,
                        principalTable: "PersonalPages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Budget = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsOpened = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EmployerPageId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_EmployerPages_EmployerPageId",
                        column: x => x.EmployerPageId,
                        principalTable: "EmployerPages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectOffers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    EditorPageId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsAccept = table.Column<bool>(type: "bit", nullable: false),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOffers_PersonalPages_EditorPageId",
                        column: x => x.EditorPageId,
                        principalTable: "PersonalPages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectOffers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCategories_PortfolioBaseCategoryId",
                table: "PortfolioCategories",
                column: "PortfolioBaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerPages_AccountId",
                table: "EmployerPages",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_EditorPageId",
                table: "JobOffers",
                column: "EditorPageId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_EmployerPageId",
                table: "JobOffers",
                column: "EmployerPageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOffers_EditorPageId",
                table: "ProjectOffers",
                column: "EditorPageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOffers_ProjectId",
                table: "ProjectOffers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_EmployerPageId",
                table: "Projects",
                column: "EmployerPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioCategories_PortfolioBaseCategories_PortfolioBaseCategoryId",
                table: "PortfolioCategories",
                column: "PortfolioBaseCategoryId",
                principalTable: "PortfolioBaseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioCategories_PortfolioBaseCategories_PortfolioBaseCategoryId",
                table: "PortfolioCategories");

            migrationBuilder.DropTable(
                name: "JobOffers");

            migrationBuilder.DropTable(
                name: "PortfolioBaseCategories");

            migrationBuilder.DropTable(
                name: "ProjectOffers");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "EmployerPages");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioCategories_PortfolioBaseCategoryId",
                table: "PortfolioCategories");

            migrationBuilder.DropColumn(
                name: "JudgesRate",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "PeopleRate",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "PortfolioCategories");

            migrationBuilder.DropColumn(
                name: "PictureAlt",
                table: "PortfolioCategories");

            migrationBuilder.DropColumn(
                name: "PictureTitle",
                table: "PortfolioCategories");

            migrationBuilder.DropColumn(
                name: "PortfolioBaseCategoryId",
                table: "PortfolioCategories");

            migrationBuilder.DropColumn(
                name: "IsBusy",
                table: "PersonalPages");

            migrationBuilder.RenameColumn(
                name: "Video",
                table: "Portfolios",
                newName: "File");

            migrationBuilder.AddColumn<long>(
                name: "UsageCategoryId",
                table: "Portfolios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "PortfolioCategories",
                type: "nvarchar(55)",
                maxLength: 55,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "PortfolioCategories",
                type: "nvarchar(55)",
                maxLength: 55,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PortfolioUsageCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioUsageCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_UsageCategoryId",
                table: "Portfolios",
                column: "UsageCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_PortfolioUsageCategories_UsageCategoryId",
                table: "Portfolios",
                column: "UsageCategoryId",
                principalTable: "PortfolioUsageCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
