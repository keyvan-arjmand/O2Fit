using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Blogging.Data.Migrations
{
    public partial class FeaturesInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeaturesCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturesCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturesCategories_Translations_NameId",
                        column: x => x.NameId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeaturesInformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleId = table.Column<int>(nullable: false),
                    SubTitleId = table.Column<int>(nullable: false),
                    DescriptionId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    VideoId = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    frequentlyQuestionCategoryId = table.Column<int>(nullable: true),
                    FeaturesCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturesInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_Translations_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_FeaturesCategories_FeaturesCategoryId",
                        column: x => x.FeaturesCategoryId,
                        principalTable: "FeaturesCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_Translations_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_Translations_SubTitleId",
                        column: x => x.SubTitleId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_Translations_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_Translations_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_FrequentlyQuestionCategories_frequentl~",
                        column: x => x.frequentlyQuestionCategoryId,
                        principalTable: "FrequentlyQuestionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesCategories_NameId",
                table: "FeaturesCategories",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_DescriptionId",
                table: "FeaturesInformations",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_FeaturesCategoryId",
                table: "FeaturesInformations",
                column: "FeaturesCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_ImageId",
                table: "FeaturesInformations",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_SubTitleId",
                table: "FeaturesInformations",
                column: "SubTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_TitleId",
                table: "FeaturesInformations",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_VideoId",
                table: "FeaturesInformations",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_frequentlyQuestionCategoryId",
                table: "FeaturesInformations",
                column: "frequentlyQuestionCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturesInformations");

            migrationBuilder.DropTable(
                name: "FeaturesCategories");
        }
    }
}
