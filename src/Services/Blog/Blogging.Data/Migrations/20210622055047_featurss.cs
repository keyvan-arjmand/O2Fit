using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogging.Data.Migrations
{
    public partial class featurss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeaturesInformations_FeaturesCategories_FeaturesCategoryId",
                table: "FeaturesInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_FeaturesInformations_FrequentlyQuestionCategories_frequentl~",
                table: "FeaturesInformations");

            migrationBuilder.DropIndex(
                name: "IX_FeaturesInformations_frequentlyQuestionCategoryId",
                table: "FeaturesInformations");

            migrationBuilder.DropColumn(
                name: "frequentlyQuestionCategoryId",
                table: "FeaturesInformations");

            migrationBuilder.RenameColumn(
                name: "FeaturesCategoryId",
                table: "FeaturesInformations",
                newName: "featuresCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_FeaturesInformations_FeaturesCategoryId",
                table: "FeaturesInformations",
                newName: "IX_FeaturesInformations_featuresCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeaturesInformations_FeaturesCategories_featuresCategoryId",
                table: "FeaturesInformations",
                column: "featuresCategoryId",
                principalTable: "FeaturesCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeaturesInformations_FeaturesCategories_featuresCategoryId",
                table: "FeaturesInformations");

            migrationBuilder.RenameColumn(
                name: "featuresCategoryId",
                table: "FeaturesInformations",
                newName: "FeaturesCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_FeaturesInformations_featuresCategoryId",
                table: "FeaturesInformations",
                newName: "IX_FeaturesInformations_FeaturesCategoryId");

            migrationBuilder.AddColumn<int>(
                name: "frequentlyQuestionCategoryId",
                table: "FeaturesInformations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_frequentlyQuestionCategoryId",
                table: "FeaturesInformations",
                column: "frequentlyQuestionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeaturesInformations_FeaturesCategories_FeaturesCategoryId",
                table: "FeaturesInformations",
                column: "FeaturesCategoryId",
                principalTable: "FeaturesCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FeaturesInformations_FrequentlyQuestionCategories_frequentl~",
                table: "FeaturesInformations",
                column: "frequentlyQuestionCategoryId",
                principalTable: "FrequentlyQuestionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
