using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddBrandTranslationRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Brands_NameId",
                table: "Brands",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Translations_NameId",
                table: "Brands",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Translations_NameId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_NameId",
                table: "Brands");
        }
    }
}
