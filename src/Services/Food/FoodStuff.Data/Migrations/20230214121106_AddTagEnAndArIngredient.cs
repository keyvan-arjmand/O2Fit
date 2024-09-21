using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddTagEnAndArIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngredientCategory",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "TagAr",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TagEn",
                table: "Ingredients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagAr",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "TagEn",
                table: "Ingredients");

            migrationBuilder.AddColumn<int>(
                name: "IngredientCategory",
                table: "Ingredients",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
