using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class CreateUserTrackDietPack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTrackDietPacks_DietPacks_DietPackId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropIndex(
                name: "IX_UserTrackDietPacks_DietPackId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "DietPackId",
                table: "UserTrackDietPacks");

            migrationBuilder.AddColumn<int>(
                name: "BreakfastDietPackageId",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BreakfastRepeat",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DinnerDietPackageId",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DinnerRepeat",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LunchDietPackageId",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LunchRepeat",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NutritionistId",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack1DietPackageId",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack1Repeat",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack2DietPackageId",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack2Repeat",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack3DietPackageId",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack3Repeat",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakfastDietPackageId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "BreakfastRepeat",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "DinnerDietPackageId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "DinnerRepeat",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "LunchDietPackageId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "LunchRepeat",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "NutritionistId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "Snack1DietPackageId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "Snack1Repeat",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "Snack2DietPackageId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "Snack2Repeat",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "Snack3DietPackageId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "Snack3Repeat",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "UserTrackDietPacks");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "UserTrackDietPacks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DietPackId",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackDietPacks_DietPackId",
                table: "UserTrackDietPacks",
                column: "DietPackId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrackDietPacks_DietPacks_DietPackId",
                table: "UserTrackDietPacks",
                column: "DietPackId",
                principalTable: "DietPacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
