using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddIsDeleteIngredientAllergyAndAddIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "IngredientAllergies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientAllergies_IsDelete",
                table: "IngredientAllergies",
                column: "IsDelete");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IngredientAllergies_IsDelete",
                table: "IngredientAllergies");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "IngredientAllergies");
        }
    }
}
