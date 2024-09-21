using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class EditNationalities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nationalities_Translations_NameTranslationId",
                table: "Nationalities");

            migrationBuilder.DropIndex(
                name: "IX_Nationalities_NameTranslationId",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "NameTranslationId",
                table: "Nationalities");

            migrationBuilder.CreateIndex(
                name: "IX_Nationalities_NameId",
                table: "Nationalities",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nationalities_Translations_NameId",
                table: "Nationalities",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nationalities_Translations_NameId",
                table: "Nationalities");

            migrationBuilder.DropIndex(
                name: "IX_Nationalities_NameId",
                table: "Nationalities");

            migrationBuilder.AddColumn<int>(
                name: "NameTranslationId",
                table: "Nationalities",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nationalities_NameTranslationId",
                table: "Nationalities",
                column: "NameTranslationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nationalities_Translations_NameTranslationId",
                table: "Nationalities",
                column: "NameTranslationId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
