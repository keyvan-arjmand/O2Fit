using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class RemoveTagInDietPack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "DietPacks");

            migrationBuilder.DropColumn(
                name: "TagArEn",
                table: "DietPacks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "DietPacks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TagArEn",
                table: "DietPacks",
                type: "text",
                nullable: true);
        }
    }
}
