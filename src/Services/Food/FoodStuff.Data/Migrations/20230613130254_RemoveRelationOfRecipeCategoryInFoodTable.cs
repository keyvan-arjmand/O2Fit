using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class RemoveRelationOfRecipeCategoryInFoodTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_RecipeCategores_RecipeCategoryId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_RecipeCategoryId",
                table: "Foods");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Foods_RecipeCategoryId",
                table: "Foods",
                column: "RecipeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_RecipeCategores_RecipeCategoryId",
                table: "Foods",
                column: "RecipeCategoryId",
                principalTable: "RecipeCategores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
