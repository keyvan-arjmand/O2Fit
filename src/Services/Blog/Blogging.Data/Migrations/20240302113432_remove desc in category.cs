using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Data.Migrations
{
    public partial class removedescincategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Translations_DescriptionId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DescriptionId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptionId",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DescriptionId",
                table: "Categories",
                column: "DescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Translations_DescriptionId",
                table: "Categories",
                column: "DescriptionId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
