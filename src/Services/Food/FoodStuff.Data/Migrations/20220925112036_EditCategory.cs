using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class EditCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Translations_NameTranslationId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_NameTranslationId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "NameTranslationId",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_NameId",
                table: "Categories",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Translations_NameId",
                table: "Categories",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Translations_NameId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_NameId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "DescriptionId",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameTranslationId",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_NameTranslationId",
                table: "Categories",
                column: "NameTranslationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Translations_NameTranslationId",
                table: "Categories",
                column: "NameTranslationId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
