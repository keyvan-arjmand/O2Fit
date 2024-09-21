using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class RemoveMainRecipeIdFromFoodsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Recipes_MainRecipeId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_FoodId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Foods_MainRecipeId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "MainRecipeId",
                table: "Foods");

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

            migrationBuilder.AddColumn<int>(
                name: "MainRecipeId",
                table: "Foods",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_FoodId",
                table: "Recipes",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_MainRecipeId",
                table: "Foods",
                column: "MainRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Recipes_MainRecipeId",
                table: "Foods",
                column: "MainRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
