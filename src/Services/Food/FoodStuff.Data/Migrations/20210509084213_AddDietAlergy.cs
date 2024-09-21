using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class AddDietAlergy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietAlergies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MainAlergyId = table.Column<int>(nullable: false),
                    IngredientAlergyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietAlergies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietAlergies_Ingredients_MainAlergyId",
                        column: x => x.MainAlergyId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietAlergies_MainAlergyId",
                table: "DietAlergies",
                column: "MainAlergyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietAlergies");
        }
    }
}
