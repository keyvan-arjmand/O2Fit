using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class FoodStuffTranslation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Foods_NameId",
                table: "Foods",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_RecipeId",
                table: "Foods",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Translations_NameId",
                table: "Foods",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Translations_RecipeId",
                table: "Foods",
                column: "RecipeId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Translations_NameId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Translations_RecipeId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_NameId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_RecipeId",
                table: "Foods");
        }
    }
}
