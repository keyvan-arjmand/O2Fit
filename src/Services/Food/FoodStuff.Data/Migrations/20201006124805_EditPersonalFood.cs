using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class EditPersonalFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PersonalFoodId",
                table: "UserTrackFoods",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Insetdate",
                table: "PersonalFoods",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Isdelete",
                table: "PersonalFoods",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Insetdate",
                table: "PersonalFoods");

            migrationBuilder.DropColumn(
                name: "Isdelete",
                table: "PersonalFoods");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalFoodId",
                table: "UserTrackFoods",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
