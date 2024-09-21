using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddTranslationRelationShipToRecipeStepsAndTips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tips");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RecipeSteps");

            migrationBuilder.AddColumn<int>(
                name: "DescriptionId",
                table: "Tips",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DescriptionId",
                table: "RecipeSteps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tips_DescriptionId",
                table: "Tips",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSteps_DescriptionId",
                table: "RecipeSteps",
                column: "DescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeSteps_Translations_DescriptionId",
                table: "RecipeSteps",
                column: "DescriptionId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Translations_DescriptionId",
                table: "Tips",
                column: "DescriptionId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeSteps_Translations_DescriptionId",
                table: "RecipeSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Translations_DescriptionId",
                table: "Tips");

            migrationBuilder.DropIndex(
                name: "IX_Tips_DescriptionId",
                table: "Tips");

            migrationBuilder.DropIndex(
                name: "IX_RecipeSteps_DescriptionId",
                table: "RecipeSteps");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "Tips");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "RecipeSteps");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tips",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RecipeSteps",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);
        }
    }
}
