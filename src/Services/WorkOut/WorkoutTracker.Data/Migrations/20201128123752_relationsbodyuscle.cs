using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutTracker.Data.Migrations
{
    public partial class relationsbodyuscle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BodyMuscles_NameId",
                table: "BodyMuscles",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyMuscles_Translations_NameId",
                table: "BodyMuscles",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyMuscles_Translations_NameId",
                table: "BodyMuscles");

            migrationBuilder.DropIndex(
                name: "IX_BodyMuscles_NameId",
                table: "BodyMuscles");
        }
    }
}
