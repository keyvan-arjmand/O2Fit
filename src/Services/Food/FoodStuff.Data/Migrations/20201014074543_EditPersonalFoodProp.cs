using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class EditPersonalFoodProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Insetdate",
                table: "PersonalFoods");

            migrationBuilder.AddColumn<DateTime>(
                name: "Insertdate",
                table: "PersonalFoods",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Insertdate",
                table: "PersonalFoods");

            migrationBuilder.AddColumn<DateTime>(
                name: "Insetdate",
                table: "PersonalFoods",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
