using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class EditFoodMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(nullable: false),
                    LogoUri = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyTargetNutrients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nutrient = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    FromAge = table.Column<int>(nullable: false),
                    ToAge = table.Column<int>(nullable: false),
                    NutrientValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTargetNutrients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(nullable: false),
                    ThumbUri = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    IngredientCategory = table.Column<int>(nullable: true),
                    NutrientValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasureUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(nullable: false),
                    Value = table.Column<float>(nullable: false),
                    MeasureUnitCategory = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalFoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Recip = table.Column<string>(nullable: true),
                    BakingType = table.Column<int>(nullable: false),
                    BakingTime = table.Column<TimeSpan>(nullable: false),
                    ImageUri = table.Column<string>(nullable: true),
                    ThumbUri = table.Column<string>(nullable: true),
                    WeightBeforBaking = table.Column<double>(nullable: false),
                    WeightAfterBaking = table.Column<double>(nullable: false),
                    EvaporatedWater = table.Column<double>(nullable: false),
                    DryIngredient = table.Column<double>(nullable: false),
                    Water = table.Column<double>(nullable: false),
                    ParentFoodId = table.Column<int>(nullable: true),
                    IsCopyable = table.Column<bool>(nullable: false),
                    NutrientValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalFoods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
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
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTrackNutrients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackNutrients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTrackWaters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackWaters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(nullable: false),
                    RecipeId = table.Column<int>(nullable: false),
                    FoodCode = table.Column<int>(nullable: false),
                    BakingType = table.Column<int>(nullable: false),
                    BakingTime = table.Column<TimeSpan>(nullable: false),
                    BarcodeGs1 = table.Column<string>(nullable: true),
                    BarcodeNational = table.Column<string>(nullable: true),
                    FoodHabit = table.Column<int>(nullable: false),
                    ImageUri = table.Column<string>(nullable: true),
                    ImageThumb = table.Column<string>(nullable: true),
                    WeightBeforBaking = table.Column<double>(nullable: false),
                    WeightAfterBaking = table.Column<double>(nullable: false),
                    EvaporatedWater = table.Column<double>(nullable: false),
                    DryIngredient = table.Column<double>(nullable: false),
                    FoodType = table.Column<int>(nullable: false),
                    IsCopyable = table.Column<bool>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    NutrientValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFoodAlergies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFoodAlergies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFoodAlergies_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientMeasureUnits",
                columns: table => new
                {
                    IngredientId = table.Column<int>(nullable: false),
                    MeasureUnitId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientMeasureUnits", x => new { x.IngredientId, x.MeasureUnitId });
                    table.ForeignKey(
                        name: "FK_IngredientMeasureUnits_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientMeasureUnits_MeasureUnits_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NutrientMeasureUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nutrient = table.Column<int>(nullable: false),
                    MeasureUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutrientMeasureUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutrientMeasureUnits_MeasureUnits_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalFoodIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonalFoodId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false),
                    MeasureUnitId = table.Column<int>(nullable: false),
                    IngredientValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalFoodIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalFoodIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalFoodIngredients_MeasureUnits_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalFoodIngredients_PersonalFoods_PersonalFoodId",
                        column: x => x.PersonalFoodId,
                        principalTable: "PersonalFoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FoodIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FoodId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false),
                    MeasureUnitId = table.Column<int>(nullable: false),
                    IngredientValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodIngredients_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodIngredients_MeasureUnits_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FoodMeasureUnits",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false),
                    MeasureUnitId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodMeasureUnits", x => new { x.FoodId, x.MeasureUnitId });
                    table.ForeignKey(
                        name: "FK_FoodMeasureUnits_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodMeasureUnits_MeasureUnits_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFoodFavorites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    FoodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFoodFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFoodFavorites_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTrackFoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    PersonalFoodId = table.Column<int>(nullable: true),
                    FoodId = table.Column<int>(nullable: false),
                    Value = table.Column<float>(nullable: false),
                    MeasureUnitId = table.Column<int>(nullable: false),
                    FoodMeal = table.Column<int>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    FoodNutrientValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTrackFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTrackFoods_MeasureUnits_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalTable: "MeasureUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredients_FoodId",
                table: "FoodIngredients",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredients_IngredientId",
                table: "FoodIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredients_MeasureUnitId",
                table: "FoodIngredients",
                column: "MeasureUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodMeasureUnits_MeasureUnitId",
                table: "FoodMeasureUnits",
                column: "MeasureUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_BrandId",
                table: "Foods",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientMeasureUnits_MeasureUnitId",
                table: "IngredientMeasureUnits",
                column: "MeasureUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_NutrientMeasureUnits_MeasureUnitId",
                table: "NutrientMeasureUnits",
                column: "MeasureUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalFoodIngredients_IngredientId",
                table: "PersonalFoodIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalFoodIngredients_MeasureUnitId",
                table: "PersonalFoodIngredients",
                column: "MeasureUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalFoodIngredients_PersonalFoodId",
                table: "PersonalFoodIngredients",
                column: "PersonalFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFoodAlergies_IngredientId",
                table: "UserFoodAlergies",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFoodFavorites_FoodId",
                table: "UserFoodFavorites",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackFoods_FoodId",
                table: "UserTrackFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackFoods_MeasureUnitId",
                table: "UserTrackFoods",
                column: "MeasureUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTargetNutrients");

            migrationBuilder.DropTable(
                name: "FoodIngredients");

            migrationBuilder.DropTable(
                name: "FoodMeasureUnits");

            migrationBuilder.DropTable(
                name: "IngredientMeasureUnits");

            migrationBuilder.DropTable(
                name: "NutrientMeasureUnits");

            migrationBuilder.DropTable(
                name: "PersonalFoodIngredients");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "UserFoodAlergies");

            migrationBuilder.DropTable(
                name: "UserFoodFavorites");

            migrationBuilder.DropTable(
                name: "UserTrackFoods");

            migrationBuilder.DropTable(
                name: "UserTrackNutrients");

            migrationBuilder.DropTable(
                name: "UserTrackWaters");

            migrationBuilder.DropTable(
                name: "PersonalFoods");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "MeasureUnits");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
