using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Advertising.Data.Migrations
{
    public partial class UpdateTranslation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_DescriptionId",
                table: "Advertises",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_ShortDescriptionId",
                table: "Advertises",
                column: "ShortDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_TitleId",
                table: "Advertises",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertises_Translations_DescriptionId",
                table: "Advertises",
                column: "DescriptionId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertises_Translations_ShortDescriptionId",
                table: "Advertises",
                column: "ShortDescriptionId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertises_Translations_TitleId",
                table: "Advertises",
                column: "TitleId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertises_Translations_DescriptionId",
                table: "Advertises");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertises_Translations_ShortDescriptionId",
                table: "Advertises");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertises_Translations_TitleId",
                table: "Advertises");

            migrationBuilder.DropIndex(
                name: "IX_Advertises_DescriptionId",
                table: "Advertises");

            migrationBuilder.DropIndex(
                name: "IX_Advertises_ShortDescriptionId",
                table: "Advertises");

            migrationBuilder.DropIndex(
                name: "IX_Advertises_TitleId",
                table: "Advertises");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ParentCategoryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");
        }
    }
}
