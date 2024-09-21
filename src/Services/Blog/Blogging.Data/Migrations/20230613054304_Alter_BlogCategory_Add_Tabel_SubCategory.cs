using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Blogging.Data.Migrations
{
    public partial class Alter_BlogCategory_Add_Tabel_SubCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogApplications_BlogCategories_BlogCategoryId",
                table: "BlogApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogWebApplications_BlogCategories_BlogCategoryId",
                table: "BlogWebApplications");

            migrationBuilder.DropIndex(
                name: "IX_BlogWebApplications_BlogCategoryId",
                table: "BlogWebApplications");

            migrationBuilder.DropIndex(
                name: "IX_BlogApplications_BlogCategoryId",
                table: "BlogApplications");

            migrationBuilder.DropColumn(
                name: "BlogCategoryId",
                table: "BlogWebApplications");

            migrationBuilder.DropColumn(
                name: "BlogCategoryId",
                table: "BlogApplications");

            migrationBuilder.AddColumn<int>(
                name: "SubBlogCategoryId",
                table: "BlogWebApplications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "BlogWebApplications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "BlogCategories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SubBlogCategoryId",
                table: "BlogApplications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "BlogApplications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubBlogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(nullable: false),
                    DescId = table.Column<int>(nullable: false),
                    ImageUri = table.Column<string>(nullable: true),
                    BlogCategoryId = table.Column<int>(nullable: false),
                    BlogBlogCategoryId = table.Column<int>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubBlogCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubBlogCategories_BlogCategories_BlogBlogCategoryId",
                        column: x => x.BlogBlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubBlogCategories_Translations_DescId",
                        column: x => x.DescId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubBlogCategories_Translations_NameId",
                        column: x => x.NameId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogWebApplications_SubBlogCategoryId",
                table: "BlogWebApplications",
                column: "SubBlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplications_SubBlogCategoryId",
                table: "BlogApplications",
                column: "SubBlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubBlogCategories_BlogBlogCategoryId",
                table: "SubBlogCategories",
                column: "BlogBlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubBlogCategories_DescId",
                table: "SubBlogCategories",
                column: "DescId");

            migrationBuilder.CreateIndex(
                name: "IX_SubBlogCategories_NameId",
                table: "SubBlogCategories",
                column: "NameId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogApplications_SubBlogCategories_SubBlogCategoryId",
                table: "BlogApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogWebApplications_SubBlogCategories_SubBlogCategoryId",
                table: "BlogWebApplications");

            migrationBuilder.DropTable(
                name: "SubBlogCategories");

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
                name: "SubCategoryId",
                table: "BlogWebApplications");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "SubBlogCategoryId",
                table: "BlogApplications");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "BlogApplications");

            migrationBuilder.AddColumn<int>(
                name: "BlogCategoryId",
                table: "BlogWebApplications",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BlogCategoryId",
                table: "BlogApplications",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogWebApplications_BlogCategoryId",
                table: "BlogWebApplications",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplications_BlogCategoryId",
                table: "BlogApplications",
                column: "BlogCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogApplications_BlogCategories_BlogCategoryId",
                table: "BlogApplications",
                column: "BlogCategoryId",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogWebApplications_BlogCategories_BlogCategoryId",
                table: "BlogWebApplications",
                column: "BlogCategoryId",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
