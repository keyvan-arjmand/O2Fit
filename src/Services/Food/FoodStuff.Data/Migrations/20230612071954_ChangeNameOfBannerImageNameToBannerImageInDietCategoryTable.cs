using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class ChangeNameOfBannerImageNameToBannerImageInDietCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImageName",
                table: "DietCategories");

            migrationBuilder.AddColumn<string>(
                name: "BannerImage",
                table: "DietCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImage",
                table: "DietCategories");

            migrationBuilder.AddColumn<string>(
                name: "BannerImageName",
                table: "DietCategories",
                type: "text",
                nullable: true);
        }
    }
}
