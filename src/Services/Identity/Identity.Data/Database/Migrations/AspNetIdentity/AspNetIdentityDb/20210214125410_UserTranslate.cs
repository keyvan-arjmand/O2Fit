using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Data.Database.Migrations.AspNetIdentity.AspNetIdentityDb
{
    public partial class UserTranslate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Countries_NameId",
                table: "Countries",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Translations_NameId",
                table: "Countries",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Translations_NameId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_NameId",
                table: "Countries");
        }
    }
}
