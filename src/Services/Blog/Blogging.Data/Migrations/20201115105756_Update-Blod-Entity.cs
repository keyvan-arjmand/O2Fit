using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Data.Migrations
{
    public partial class UpdateBlodEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Blogs_DescriptionId",
                table: "Blogs",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ShortDescriptionId",
                table: "Blogs",
                column: "ShortDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_TitleId",
                table: "Blogs",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Translations_DescriptionId",
                table: "Blogs",
                column: "DescriptionId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Translations_ShortDescriptionId",
                table: "Blogs",
                column: "ShortDescriptionId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Translations_TitleId",
                table: "Blogs",
                column: "TitleId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Translations_DescriptionId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Translations_ShortDescriptionId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Translations_TitleId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_DescriptionId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_ShortDescriptionId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_TitleId",
                table: "Blogs");
        }
    }
}
