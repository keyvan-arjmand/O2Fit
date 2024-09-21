using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class AddIngredientAllergyCategoryIngredientAllergyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientAllergies_IngredientAllergyCategories_IngredientA~",
                table: "IngredientAllergies");

            migrationBuilder.DropIndex(
                name: "IX_IngredientAllergies_IngredientAllergyCategoryId",
                table: "IngredientAllergies");

            migrationBuilder.DropColumn(
                name: "IngredientAllergyCategoryId",
                table: "IngredientAllergies");

            migrationBuilder.CreateTable(
                name: "IngredientAllergyCategoryIngredientAllergies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IngredientAllergyCategoryId = table.Column<int>(nullable: false),
                    IngredientAllergyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientAllergyCategoryIngredientAllergies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientAllergyCategoryIngredientAllergies_IngredientAlle~",
                        column: x => x.IngredientAllergyCategoryId,
                        principalTable: "IngredientAllergyCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientAllergyCategoryIngredientAllergies_IngredientAll~1",
                        column: x => x.IngredientAllergyId,
                        principalTable: "IngredientAllergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientAllergyCategoryIngredientAllergies_IngredientAlle~",
                table: "IngredientAllergyCategoryIngredientAllergies",
                column: "IngredientAllergyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientAllergyCategoryIngredientAllergies_IngredientAll~1",
                table: "IngredientAllergyCategoryIngredientAllergies",
                column: "IngredientAllergyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientAllergyCategoryIngredientAllergies");

            migrationBuilder.AddColumn<int>(
                name: "IngredientAllergyCategoryId",
                table: "IngredientAllergies",
                type: "integer",
                nullable: true);

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
    }
}
