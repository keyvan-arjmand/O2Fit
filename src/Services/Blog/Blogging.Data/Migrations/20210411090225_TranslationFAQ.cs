using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Data.Migrations
{
    public partial class TranslationFAQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyQuestions_QuestionId",
                table: "FrequentlyQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyQuestions_ResponseId",
                table: "FrequentlyQuestions",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyQuestionCategories_NameId",
                table: "FrequentlyQuestionCategories",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_FrequentlyQuestionCategories_Translations_NameId",
                table: "FrequentlyQuestionCategories",
                column: "NameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FrequentlyQuestions_Translations_QuestionId",
                table: "FrequentlyQuestions",
                column: "QuestionId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FrequentlyQuestions_Translations_ResponseId",
                table: "FrequentlyQuestions",
                column: "ResponseId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FrequentlyQuestionCategories_Translations_NameId",
                table: "FrequentlyQuestionCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_FrequentlyQuestions_Translations_QuestionId",
                table: "FrequentlyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_FrequentlyQuestions_Translations_ResponseId",
                table: "FrequentlyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_FrequentlyQuestions_QuestionId",
                table: "FrequentlyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_FrequentlyQuestions_ResponseId",
                table: "FrequentlyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_FrequentlyQuestionCategories_NameId",
                table: "FrequentlyQuestionCategories");
        }
    }
}
