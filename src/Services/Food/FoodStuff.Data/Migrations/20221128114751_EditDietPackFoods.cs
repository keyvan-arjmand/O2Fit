using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class EditDietPackFoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "DietPackFoods");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "DietPacks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "DietPacks");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "DietPackFoods",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
