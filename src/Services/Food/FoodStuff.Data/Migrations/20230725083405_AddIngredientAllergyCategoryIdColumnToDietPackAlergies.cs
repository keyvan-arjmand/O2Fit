using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddIngredientAllergyCategoryIdColumnToDietPackAlergies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IngredientAllergyCategoryId",
                table: "DietPackAlerges",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DietPackAlerges_IngredientAllergyCategoryId",
                table: "DietPackAlerges",
                column: "IngredientAllergyCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietPackAlerges_IngredientAllergyCategories_IngredientAller~",
                table: "DietPackAlerges",
                column: "IngredientAllergyCategoryId",
                principalTable: "IngredientAllergyCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietPackAlerges_IngredientAllergyCategories_IngredientAller~",
                table: "DietPackAlerges");

            migrationBuilder.DropIndex(
                name: "IX_DietPackAlerges_IngredientAllergyCategoryId",
                table: "DietPackAlerges");

            migrationBuilder.DropColumn(
                name: "IngredientAllergyCategoryId",
                table: "DietPackAlerges");
        }
    }
}
