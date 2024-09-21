using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class EditNationality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nationalities_Translations_NameId",
                table: "Nationalities");

            migrationBuilder.DropIndex(
                name: "IX_Nationalities_NameId",
                table: "Nationalities");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "Nationalities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptionId",
                table: "Nationalities",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nationalities_NameId",
                table: "Nationalities",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nationalities_Translations_NameId",
                table: "Nationalities",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
