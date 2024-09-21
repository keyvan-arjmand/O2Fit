using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddLocal_idFoodStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "UserTrackWaters",
                newName: "_id");

            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "UserTrackNutrients",
                newName: "_id");

            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "UserTrackFoods",
                newName: "_id");

            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "UserFoodFavorites",
                newName: "_id");

            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "UserFoodAlergies",
                newName: "_id");

            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "PersonalFoods",
                newName: "_id");

            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "PersonalFoodIngredients",
                newName: "_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_id",
                table: "UserTrackWaters",
                newName: "_Id");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "UserTrackNutrients",
                newName: "_Id");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "UserTrackFoods",
                newName: "_Id");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "UserFoodFavorites",
                newName: "_Id");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "UserFoodAlergies",
                newName: "_Id");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "PersonalFoods",
                newName: "_Id");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "PersonalFoodIngredients",
                newName: "_Id");
        }
    }
}
