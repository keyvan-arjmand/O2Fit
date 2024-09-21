using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class EditDietPack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DietPacks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "DietPacks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TagArEn",
                table: "DietPacks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DietPacks");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "DietPacks");

            migrationBuilder.DropColumn(
                name: "TagArEn",
                table: "DietPacks");
        }
    }
}
