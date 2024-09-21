using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutTracker.Data.Migrations
{
    public partial class AddLocal_idInWorkoutTracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "UserTrackWorkOuts",
                newName: "_id");

            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "UserTrackSteps",
                newName: "_id");

            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "UserTrackSleeps",
                newName: "_id");

            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "UserFavoriteWorkOuts",
                newName: "_id");

            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "PersonalWorkOuts",
                newName: "_id");

            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "BurnedWorkOutCalories",
                newName: "_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_id",
                table: "UserTrackWorkOuts",
                newName: "_Id");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "UserTrackSteps",
                newName: "_Id");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "UserTrackSleeps",
                newName: "_Id");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "UserFavoriteWorkOuts",
                newName: "_Id");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "PersonalWorkOuts",
                newName: "_Id");

            migrationBuilder.RenameColumn(
                name: "_id",
                table: "BurnedWorkOutCalories",
                newName: "_Id");
        }
    }
}
