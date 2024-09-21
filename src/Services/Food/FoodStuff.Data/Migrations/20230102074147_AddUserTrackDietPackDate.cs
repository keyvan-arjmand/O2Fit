using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class AddUserTrackDietPackDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakfastDietPackageId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "BreakfastRepeat",
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
                name: "LunchDietPackageId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "LunchRepeat",
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

            migrationBuilder.AddColumn<int>(
                name: "Meal",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Repeat",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserTrackDietPackDateId",
                table: "UserTrackDietPacks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserTrackDietPackDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserTrackDietPackId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackDietPackDates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackDietPacks_UserTrackDietPackDateId",
                table: "UserTrackDietPacks",
                column: "UserTrackDietPackDateId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrackDietPacks_UserTrackDietPackDates_UserTrackDietPack~",
                table: "UserTrackDietPacks",
                column: "UserTrackDietPackDateId",
                principalTable: "UserTrackDietPackDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTrackDietPacks_UserTrackDietPackDates_UserTrackDietPack~",
                table: "UserTrackDietPacks");

            migrationBuilder.DropTable(
                name: "UserTrackDietPackDates");

            migrationBuilder.DropIndex(
                name: "IX_UserTrackDietPacks_UserTrackDietPackDateId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "Meal",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "Repeat",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "UserTrackDietPackDateId",
                table: "UserTrackDietPacks");

            migrationBuilder.AddColumn<int>(
                name: "BreakfastDietPackageId",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BreakfastRepeat",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DinnerDietPackageId",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DinnerRepeat",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "UserTrackDietPacks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LunchDietPackageId",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LunchRepeat",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack1DietPackageId",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack1Repeat",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack2DietPackageId",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack2Repeat",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack3DietPackageId",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Snack3Repeat",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "UserTrackDietPacks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
