using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddParentIdToDitePack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBase",
                table: "DietPacks");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "DietPacks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "DietPacks");

            migrationBuilder.AddColumn<bool>(
                name: "IsBase",
                table: "DietPacks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
