using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Blogging.Data.Migrations
{
    public partial class Frequently1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FrequentlyQuestions_Translations_ResponseId",
                table: "FrequentlyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_FrequentlyQuestions_ResponseId",
                table: "FrequentlyQuestions");

            migrationBuilder.DropColumn(
                name: "ResponseId",
                table: "FrequentlyQuestions");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "FrequentlyQuestions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FrequentlyResponses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ResponseId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    FrequentlyQuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrequentlyResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrequentlyResponses_FrequentlyQuestions_FrequentlyQuestionId",
                        column: x => x.FrequentlyQuestionId,
                        principalTable: "FrequentlyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FrequentlyResponses_Translations_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyResponses_FrequentlyQuestionId",
                table: "FrequentlyResponses",
                column: "FrequentlyQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyResponses_ResponseId",
                table: "FrequentlyResponses",
                column: "ResponseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FrequentlyResponses");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "FrequentlyQuestions");

            migrationBuilder.AddColumn<int>(
                name: "ResponseId",
                table: "FrequentlyQuestions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyQuestions_ResponseId",
                table: "FrequentlyQuestions",
                column: "ResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_FrequentlyQuestions_Translations_ResponseId",
                table: "FrequentlyQuestions",
                column: "ResponseId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
