using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class ChangeTypeOfRelationShipOfFoodAndRecipeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipes_FoodId",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "MainRecipeId",
                table: "Foods",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_FoodId",
                table: "Recipes",
                column: "FoodId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipes_FoodId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MainRecipeId",
                table: "Foods");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_FoodId",
                table: "Recipes",
                column: "FoodId");
        }
    }
}
