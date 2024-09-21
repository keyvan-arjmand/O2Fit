using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class DietPackMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(nullable: false),
                    DescriptionId = table.Column<int>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietCategories_Translations_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DietCategories_Translations_NameId",
                        column: x => x.NameId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DietPacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(nullable: false),
                    FoodMeal = table.Column<int>(nullable: false),
                    BodyType = table.Column<int>(nullable: false),
                    DietCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietPacks_DietCategories_DietCategoryId",
                        column: x => x.DietCategoryId,
                        principalTable: "DietCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DietPacks_Translations_NameId",
                        column: x => x.NameId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DietPackAlerges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IngredientId = table.Column<int>(nullable: true),
                    DietPackId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPackAlerges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietPackAlerges_DietPacks_DietPackId",
                        column: x => x.DietPackId,
                        principalTable: "DietPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DietPackAlerges_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DietPackCountries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(nullable: false),
                    DietPackId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPackCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietPackCountries_DietPacks_DietPackId",
                        column: x => x.DietPackId,
                        principalTable: "DietPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DietPackFoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<double>(nullable: false),
                    FoodId = table.Column<int>(nullable: true),
                    MeasureUnitId = table.Column<int>(nullable: true),
                    DietPackId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPackFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietPackFoods_DietPacks_DietPackId",
                        column: x => x.DietPackId,
                        principalTable: "DietPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DietPackFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DietPackFoods_MeasureUnits_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DietPackSpecialDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DietPackId = table.Column<int>(nullable: true),
                    SpecialDisease = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPackSpecialDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietPackSpecialDiseases_DietPacks_DietPackId",
                        column: x => x.DietPackId,
                        principalTable: "DietPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTrackDietPacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    DietPackId = table.Column<int>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackDietPacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTrackDietPacks_DietPacks_DietPackId",
                        column: x => x.DietPackId,
                        principalTable: "DietPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietCategories_DescriptionId",
                table: "DietCategories",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DietCategories_NameId",
                table: "DietCategories",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackAlerges_DietPackId",
                table: "DietPackAlerges",
                column: "DietPackId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackAlerges_IngredientId",
                table: "DietPackAlerges",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackCountries_DietPackId",
                table: "DietPackCountries",
                column: "DietPackId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackFoods_DietPackId",
                table: "DietPackFoods",
                column: "DietPackId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackFoods_FoodId",
                table: "DietPackFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackFoods_MeasureUnitId",
                table: "DietPackFoods",
                column: "MeasureUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPacks_DietCategoryId",
                table: "DietPacks",
                column: "DietCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPacks_NameId",
                table: "DietPacks",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackSpecialDiseases_DietPackId",
                table: "DietPackSpecialDiseases",
                column: "DietPackId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackDietPacks_DietPackId",
                table: "UserTrackDietPacks",
                column: "DietPackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietPackAlerges");

            migrationBuilder.DropTable(
                name: "DietPackCountries");

            migrationBuilder.DropTable(
                name: "DietPackFoods");

            migrationBuilder.DropTable(
                name: "DietPackSpecialDiseases");

            migrationBuilder.DropTable(
                name: "UserTrackDietPacks");

            migrationBuilder.DropTable(
                name: "DietPacks");

            migrationBuilder.DropTable(
                name: "DietCategories");
        }
    }
}
