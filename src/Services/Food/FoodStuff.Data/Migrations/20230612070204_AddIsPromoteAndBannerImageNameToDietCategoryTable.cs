using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddIsPromoteAndBannerImageNameToDietCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BannerImageName",
                table: "DietCategories",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPromote",
                table: "DietCategories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImageName",
                table: "DietCategories");

            migrationBuilder.DropColumn(
                name: "IsPromote",
                table: "DietCategories");
        }
    }
}
