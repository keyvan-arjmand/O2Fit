using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class CacheAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "UserTrackWaters",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "UserTrackWaters",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "UserTrackNutrients",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "UserTrackNutrients",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "UserTrackFoods",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "UserTrackFoods",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "UserFoodFavorites",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "UserFoodFavorites",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "UserFoodAlergies",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "UserFoodAlergies",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Translations",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Translations",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "PersonalFoods",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "PersonalFoods",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "PersonalFoodIngredients",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "PersonalFoodIngredients",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "NutrientMeasureUnits",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "NutrientMeasureUnits",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "MeasureUnits",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "MeasureUnits",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Ingredients",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "IngredientMeasureUnits",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "IngredientMeasureUnits",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Foods",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Foods",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "FoodMeasureUnits",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "FoodMeasureUnits",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "FoodIngredients",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "FoodIngredients",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "DailyTargetNutrients",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "DailyTargetNutrients",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Brands",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "UserTrackWaters");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "UserTrackWaters");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "UserTrackNutrients");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "UserTrackNutrients");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "UserTrackFoods");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "UserTrackFoods");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "UserFoodFavorites");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "UserFoodFavorites");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "UserFoodAlergies");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "UserFoodAlergies");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Translations");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Translations");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "PersonalFoods");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "PersonalFoods");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "PersonalFoodIngredients");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "PersonalFoodIngredients");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "NutrientMeasureUnits");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "NutrientMeasureUnits");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "MeasureUnits");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "MeasureUnits");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "IngredientMeasureUnits");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "IngredientMeasureUnits");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "FoodMeasureUnits");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "FoodMeasureUnits");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "FoodIngredients");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "FoodIngredients");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "DailyTargetNutrients");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "DailyTargetNutrients");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Brands");
        }
    }
}
