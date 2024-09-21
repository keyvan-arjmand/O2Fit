using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Data.Migrations
{
    public partial class EditBlogLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ShortDescriptionId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "TitleId",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "DescriptionId",
                table: "Blogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShortDescriptionId",
                table: "Blogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TitleId",
                table: "Blogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
    }
}
