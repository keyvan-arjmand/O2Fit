using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddLocalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "UserTrackWaters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "UserTrackNutrients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "UserTrackFoods",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "UserFoodFavorites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "UserFoodAlergies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "PersonalFoods",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "PersonalFoodIngredients",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Foods",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_Id",
                table: "UserTrackWaters");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "UserTrackNutrients");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "UserTrackFoods");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "UserFoodFavorites");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "UserFoodAlergies");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "PersonalFoods");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "PersonalFoodIngredients");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Foods");

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "UserTrackWaters",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "UserTrackWaters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "UserTrackNutrients",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "UserTrackNutrients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "UserTrackFoods",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "UserTrackFoods",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "UserFoodFavorites",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "UserFoodFavorites",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "UserFoodAlergies",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "UserFoodAlergies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Translations",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Translations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "PersonalFoods",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "PersonalFoods",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "PersonalFoodIngredients",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "PersonalFoodIngredients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "NutrientMeasureUnits",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "NutrientMeasureUnits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "MeasureUnits",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "MeasureUnits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Ingredients",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Ingredients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "IngredientMeasureUnits",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "IngredientMeasureUnits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Foods",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Foods",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "FoodMeasureUnits",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "FoodMeasureUnits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "FoodIngredients",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "FoodIngredients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "DailyTargetNutrients",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "DailyTargetNutrients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Brands",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Brands",
                type: "text",
                nullable: true);
        }
    }
}
