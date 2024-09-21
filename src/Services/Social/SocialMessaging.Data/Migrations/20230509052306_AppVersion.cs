using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SocialMessaging.Data.Migrations
{
    public partial class AppVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketMessages_Translations_ButtonNameId",
                table: "MarketMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketMessages_Translations_DescriptionId",
                table: "MarketMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketMessages_Translations_TitleId",
                table: "MarketMessages");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppVersions");

            migrationBuilder.AddColumn<int>(
                name: "DescId",
                table: "AppVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TranslationDtos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Persian = table.Column<string>(nullable: true),
                    English = table.Column<string>(nullable: true),
                    Arabic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationDtos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppVersions_DescId",
                table: "AppVersions",
                column: "DescId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppVersions_TranslationDtos_DescId",
                table: "AppVersions",
                column: "DescId",
                principalTable: "TranslationDtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketMessages_TranslationDtos_ButtonNameId",
                table: "MarketMessages",
                column: "ButtonNameId",
                principalTable: "TranslationDtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketMessages_TranslationDtos_DescriptionId",
                table: "MarketMessages",
                column: "DescriptionId",
                principalTable: "TranslationDtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketMessages_TranslationDtos_TitleId",
                table: "MarketMessages",
                column: "TitleId",
                principalTable: "TranslationDtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppVersions_TranslationDtos_DescId",
                table: "AppVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketMessages_TranslationDtos_ButtonNameId",
                table: "MarketMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketMessages_TranslationDtos_DescriptionId",
                table: "MarketMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketMessages_TranslationDtos_TitleId",
                table: "MarketMessages");

            migrationBuilder.DropTable(
                name: "TranslationDtos");

            migrationBuilder.DropIndex(
                name: "IX_AppVersions_DescId",
                table: "AppVersions");

            migrationBuilder.DropColumn(
                name: "DescId",
                table: "AppVersions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppVersions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Arabic = table.Column<string>(type: "text", nullable: true),
                    English = table.Column<string>(type: "text", nullable: true),
                    Persian = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MarketMessages_Translations_ButtonNameId",
                table: "MarketMessages",
                column: "ButtonNameId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketMessages_Translations_DescriptionId",
                table: "MarketMessages",
                column: "DescriptionId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketMessages_Translations_TitleId",
                table: "MarketMessages",
                column: "TitleId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
