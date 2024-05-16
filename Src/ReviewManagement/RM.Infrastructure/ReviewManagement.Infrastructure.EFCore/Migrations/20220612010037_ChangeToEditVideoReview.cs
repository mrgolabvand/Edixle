using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewManagement.Infrastructure.EFCore.Migrations
{
    public partial class ChangeToEditVideoReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorthThePurchase",
                table: "Reviews",
                newName: "UseProperVideoEffects");

            migrationBuilder.RenameColumn(
                name: "Options",
                table: "Reviews",
                newName: "UseProperSoundEffects");

            migrationBuilder.RenameColumn(
                name: "Innovation",
                table: "Reviews",
                newName: "UseProperMemes");

            migrationBuilder.RenameColumn(
                name: "EaseOfUse",
                table: "Reviews",
                newName: "UseProperFontAndColors");

            migrationBuilder.RenameColumn(
                name: "Design",
                table: "Reviews",
                newName: "EditVideoQuality");

            migrationBuilder.RenameColumn(
                name: "BuildQuality",
                table: "Reviews",
                newName: "EditSoundQuality");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UseProperVideoEffects",
                table: "Reviews",
                newName: "WorthThePurchase");

            migrationBuilder.RenameColumn(
                name: "UseProperSoundEffects",
                table: "Reviews",
                newName: "Options");

            migrationBuilder.RenameColumn(
                name: "UseProperMemes",
                table: "Reviews",
                newName: "Innovation");

            migrationBuilder.RenameColumn(
                name: "UseProperFontAndColors",
                table: "Reviews",
                newName: "EaseOfUse");

            migrationBuilder.RenameColumn(
                name: "EditVideoQuality",
                table: "Reviews",
                newName: "Design");

            migrationBuilder.RenameColumn(
                name: "EditSoundQuality",
                table: "Reviews",
                newName: "BuildQuality");
        }
    }
}
