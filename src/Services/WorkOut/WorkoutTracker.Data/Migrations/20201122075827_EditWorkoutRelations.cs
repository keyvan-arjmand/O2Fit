using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutTracker.Data.Migrations
{
    public partial class EditWorkoutRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkOuts_HowToDoId",
                table: "WorkOuts",
                column: "HowToDoId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOuts_RecommandationId",
                table: "WorkOuts",
                column: "RecommandationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOutAttributeValues_NameId",
                table: "WorkOutAttributeValues",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOutAttributes_NameId",
                table: "WorkOutAttributes",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOutAttributes_Translations_NameId",
                table: "WorkOutAttributes",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOutAttributeValues_Translations_NameId",
                table: "WorkOutAttributeValues",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOuts_Translations_HowToDoId",
                table: "WorkOuts",
                column: "HowToDoId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOuts_Translations_RecommandationId",
                table: "WorkOuts",
                column: "RecommandationId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOutAttributes_Translations_NameId",
                table: "WorkOutAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOutAttributeValues_Translations_NameId",
                table: "WorkOutAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOuts_Translations_HowToDoId",
                table: "WorkOuts");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOuts_Translations_RecommandationId",
                table: "WorkOuts");

            migrationBuilder.DropIndex(
                name: "IX_WorkOuts_HowToDoId",
                table: "WorkOuts");

            migrationBuilder.DropIndex(
                name: "IX_WorkOuts_RecommandationId",
                table: "WorkOuts");

            migrationBuilder.DropIndex(
                name: "IX_WorkOutAttributeValues_NameId",
                table: "WorkOutAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_WorkOutAttributes_NameId",
                table: "WorkOutAttributes");
        }
    }
}
