using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class IngredientAllergyCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IngredientAllergyCategoryId",
                table: "IngredientAllergies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IngredientAllergyCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IngredientAllergyCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientAllergyCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientAllergies_IngredientAllergyCategoryId",
                table: "IngredientAllergies",
                column: "IngredientAllergyCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientAllergies_IngredientAllergyCategories_IngredientA~",
                table: "IngredientAllergies",
                column: "IngredientAllergyCategoryId",
                principalTable: "IngredientAllergyCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientAllergies_IngredientAllergyCategories_IngredientA~",
                table: "IngredientAllergies");

            migrationBuilder.DropTable(
                name: "IngredientAllergyCategories");

            migrationBuilder.DropIndex(
                name: "IX_IngredientAllergies_IngredientAllergyCategoryId",
                table: "IngredientAllergies");

            migrationBuilder.DropColumn(
                name: "IngredientAllergyCategoryId",
                table: "IngredientAllergies");
        }
    }
}
