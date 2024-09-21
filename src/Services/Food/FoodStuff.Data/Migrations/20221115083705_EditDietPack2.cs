using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class EditDietPack2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietPacks_DietCategories_DietCategoryId",
                table: "DietPacks");

            migrationBuilder.DropTable(
                name: "DietPackCountries");

            migrationBuilder.DropTable(
                name: "DietPackSpecialDiseases");

            migrationBuilder.DropIndex(
                name: "IX_DietPacks_DietCategoryId",
                table: "DietPacks");

            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "DietPacks");

            migrationBuilder.DropColumn(
                name: "DietCategoryId",
                table: "DietPacks");

            migrationBuilder.DropColumn(
                name: "DietPackState",
                table: "DietPacks");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "DietPacks");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientCategory",
                table: "Ingredients",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DietPacks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "DietPackId",
                table: "DietPackFoods",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DietPackDietCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DietCategoryId = table.Column<int>(nullable: false),
                    DietPackId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPackDietCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietPackDietCategories_DietCategories_DietCategoryId",
                        column: x => x.DietCategoryId,
                        principalTable: "DietCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DietPackDietCategories_DietPacks_DietPackId",
                        column: x => x.DietPackId,
                        principalTable: "DietPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DietPackNationalities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NationalityId = table.Column<int>(nullable: false),
                    DietPackId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPackNationalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietPackNationalities_DietPacks_DietPackId",
                        column: x => x.DietPackId,
                        principalTable: "DietPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DietPackNationalities_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietPackDietCategories_DietCategoryId",
                table: "DietPackDietCategories",
                column: "DietCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackDietCategories_DietPackId",
                table: "DietPackDietCategories",
                column: "DietPackId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackNationalities_DietPackId",
                table: "DietPackNationalities",
                column: "DietPackId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackNationalities_NationalityId",
                table: "DietPackNationalities",
                column: "NationalityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietPackDietCategories");

            migrationBuilder.DropTable(
                name: "DietPackNationalities");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DietPacks");

            migrationBuilder.AlterColumn<int>(
                name: "IngredientCategory",
                table: "Ingredients",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BodyType",
                table: "DietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DietCategoryId",
                table: "DietPacks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DietPackState",
                table: "DietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "DietPacks",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DietPackId",
                table: "DietPackFoods",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "DietPackCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    DietPackId = table.Column<int>(type: "integer", nullable: true)
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
                name: "DietPackSpecialDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DietPackId = table.Column<int>(type: "integer", nullable: true),
                    SpecialDisease = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_DietPacks_DietCategoryId",
                table: "DietPacks",
                column: "DietCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackCountries_DietPackId",
                table: "DietPackCountries",
                column: "DietPackId");

            migrationBuilder.CreateIndex(
                name: "IX_DietPackSpecialDiseases_DietPackId",
                table: "DietPackSpecialDiseases",
                column: "DietPackId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietPacks_DietCategories_DietCategoryId",
                table: "DietPacks",
                column: "DietCategoryId",
                principalTable: "DietCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
