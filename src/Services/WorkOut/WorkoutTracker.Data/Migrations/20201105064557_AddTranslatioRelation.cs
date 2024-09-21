using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutTracker.Data.Migrations
{
    public partial class AddTranslatioRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkOuts_NameId",
                table: "WorkOuts",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOuts_Translations_NameId",
                table: "WorkOuts",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOuts_Translations_NameId",
                table: "WorkOuts");

            migrationBuilder.DropIndex(
                name: "IX_WorkOuts_NameId",
                table: "WorkOuts");
        }
    }
}
