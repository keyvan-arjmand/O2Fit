using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class EditIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_NameId",
                table: "Ingredients",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Translations_NameId",
                table: "Ingredients",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Translations_NameId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_NameId",
                table: "Ingredients");
        }
    }
}
