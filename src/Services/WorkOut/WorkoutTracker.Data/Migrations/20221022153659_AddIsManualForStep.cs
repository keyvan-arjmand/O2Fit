using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutTracker.Data.Migrations
{
    public partial class AddIsManualForStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAuto",
                table: "UserTrackSteps");

            migrationBuilder.AddColumn<bool>(
                name: "IsManual",
                table: "UserTrackSteps",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsManual",
                table: "UserTrackSteps");

            migrationBuilder.AddColumn<bool>(
                name: "IsAuto",
                table: "UserTrackSteps",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
