using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Data.Migrations
{
    public partial class Edit_Column_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubBlogCategories_BlogCategories_BlogBlogCategoryId",
                table: "SubBlogCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubBlogCategories_BlogBlogCategoryId",
                table: "SubBlogCategories");

            migrationBuilder.DropColumn(
                name: "BlogBlogCategoryId",
                table: "SubBlogCategories");

            migrationBuilder.CreateIndex(
                name: "IX_SubBlogCategories_BlogCategoryId",
                table: "SubBlogCategories",
                column: "BlogCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubBlogCategories_BlogCategories_BlogCategoryId",
                table: "SubBlogCategories",
                column: "BlogCategoryId",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubBlogCategories_BlogCategories_BlogCategoryId",
                table: "SubBlogCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubBlogCategories_BlogCategoryId",
                table: "SubBlogCategories");

            migrationBuilder.AddColumn<int>(
                name: "BlogBlogCategoryId",
                table: "SubBlogCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubBlogCategories_BlogBlogCategoryId",
                table: "SubBlogCategories",
                column: "BlogBlogCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubBlogCategories_BlogCategories_BlogBlogCategoryId",
                table: "SubBlogCategories",
                column: "BlogBlogCategoryId",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
