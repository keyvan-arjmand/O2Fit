using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.Data.Migrations
{
    public partial class OrderTranslate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Discounts_NameId",
                table: "Discounts",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Translations_NameId",
                table: "Discounts",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Translations_NameId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_NameId",
                table: "Discounts");
        }
    }
}
