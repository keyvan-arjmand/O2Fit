using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddDietPackState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DietPacks");

            migrationBuilder.AddColumn<int>(
                name: "DietPackState",
                table: "DietPacks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DietPackState",
                table: "DietPacks");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DietPacks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
