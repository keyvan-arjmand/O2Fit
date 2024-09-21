using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.Data.Migrations
{
    public partial class PackageTranslation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Packages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Packages_DescriptionId",
                table: "Packages",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_NameId",
                table: "Packages",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Translations_DescriptionId",
                table: "Packages",
                column: "DescriptionId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Translations_NameId",
                table: "Packages",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Translations_DescriptionId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Translations_NameId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_DescriptionId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_NameId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Packages");
        }
    }
}
