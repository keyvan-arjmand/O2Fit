using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Data.Migrations
{
    public partial class AddTranslationRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainBanner",
                table: "Blogs");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_NameId",
                table: "BlogCategories",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Translations_NameId",
                table: "BlogCategories",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Translations_NameId",
                table: "BlogCategories");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategories_NameId",
                table: "BlogCategories");

            migrationBuilder.AddColumn<string>(
                name: "MainBanner",
                table: "Blogs",
                type: "text",
                nullable: true);
        }
    }
}
