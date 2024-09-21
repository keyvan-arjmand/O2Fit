using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class EditMeasureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MeasureUnits_NameId",
                table: "MeasureUnits",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasureUnits_Translations_NameId",
                table: "MeasureUnits",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasureUnits_Translations_NameId",
                table: "MeasureUnits");

            migrationBuilder.DropIndex(
                name: "IX_MeasureUnits_NameId",
                table: "MeasureUnits");
        }
    }
}
