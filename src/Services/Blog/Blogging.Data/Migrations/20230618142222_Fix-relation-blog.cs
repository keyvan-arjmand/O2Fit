using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Data.Migrations
{
    public partial class Fixrelationblog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogApplications_SubBlogCategories_SubBlogCategoryId",
                table: "BlogApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogWebApplications_SubBlogCategories_SubBlogCategoryId",
                table: "BlogWebApplications");

            migrationBuilder.DropIndex(
                name: "IX_BlogWebApplications_SubBlogCategoryId",
                table: "BlogWebApplications");

            migrationBuilder.DropIndex(
                name: "IX_BlogApplications_SubBlogCategoryId",
                table: "BlogApplications");

            migrationBuilder.DropColumn(
                name: "SubBlogCategoryId",
                table: "BlogWebApplications");

            migrationBuilder.DropColumn(
                name: "SubBlogCategoryId",
                table: "BlogApplications");

            migrationBuilder.CreateIndex(
                name: "IX_BlogWebApplications_SubCategoryId",
                table: "BlogWebApplications",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplications_SubCategoryId",
                table: "BlogApplications",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogApplications_SubBlogCategories_SubCategoryId",
                table: "BlogApplications",
                column: "SubCategoryId",
                principalTable: "SubBlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogWebApplications_SubBlogCategories_SubCategoryId",
                table: "BlogWebApplications",
                column: "SubCategoryId",
                principalTable: "SubBlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogApplications_SubBlogCategories_SubCategoryId",
                table: "BlogApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogWebApplications_SubBlogCategories_SubCategoryId",
                table: "BlogWebApplications");

            migrationBuilder.DropIndex(
                name: "IX_BlogWebApplications_SubCategoryId",
                table: "BlogWebApplications");

            migrationBuilder.DropIndex(
                name: "IX_BlogApplications_SubCategoryId",
                table: "BlogApplications");

            migrationBuilder.AddColumn<int>(
                name: "SubBlogCategoryId",
                table: "BlogWebApplications",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubBlogCategoryId",
                table: "BlogApplications",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogWebApplications_SubBlogCategoryId",
                table: "BlogWebApplications",
                column: "SubBlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplications_SubBlogCategoryId",
                table: "BlogApplications",
                column: "SubBlogCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogApplications_SubBlogCategories_SubBlogCategoryId",
                table: "BlogApplications",
                column: "SubBlogCategoryId",
                principalTable: "SubBlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogWebApplications_SubBlogCategories_SubBlogCategoryId",
                table: "BlogWebApplications",
                column: "SubBlogCategoryId",
                principalTable: "SubBlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
