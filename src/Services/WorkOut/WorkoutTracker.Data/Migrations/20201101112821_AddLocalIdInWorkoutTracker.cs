using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutTracker.Data.Migrations
{
    public partial class AddLocalIdInWorkoutTracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "UserTrackWorkOuts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "UserTrackSteps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "UserTrackSleeps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "UserFavoriteWorkOuts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "PersonalWorkOuts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "BurnedWorkOutCalories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_Id",
                table: "UserTrackWorkOuts");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "UserTrackSteps");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "UserTrackSleeps");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "UserFavoriteWorkOuts");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "PersonalWorkOuts");

            migrationBuilder.DropColumn(
                name: "_Id",
                table: "BurnedWorkOutCalories");
        }
    }
}
