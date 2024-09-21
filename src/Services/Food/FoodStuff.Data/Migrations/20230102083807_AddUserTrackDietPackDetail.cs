using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class AddUserTrackDietPackDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<double>(
                name: "DailyCalorie",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PackageName",
                table: "UserTrackDietPacks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "UserTrackDietPacks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "UserTrackDietPackDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PackageId = table.Column<int>(nullable: false),
                    Repeat = table.Column<int>(nullable: false),
                    Meal = table.Column<int>(nullable: false),
                    UserTrackDietPackId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackDietPackDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTrackDietPackDetails_UserTrackDietPacks_UserTrackDietPa~",
                        column: x => x.UserTrackDietPackId,
                        principalTable: "UserTrackDietPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackDietPackDetails_UserTrackDietPackId",
                table: "UserTrackDietPackDetails",
                column: "UserTrackDietPackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTrackDietPackDetails");

            migrationBuilder.DropColumn(
                name: "DailyCalorie",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "PackageName",
                table: "UserTrackDietPacks");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "UserTrackDietPacks");

            migrationBuilder.AddColumn<int>(
                name: "Meal",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Repeat",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserTrackDietPackDateId",
                table: "UserTrackDietPacks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserTrackDietPackDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserTrackDietPackId = table.Column<int>(type: "integer", nullable: false)
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
    }
}
