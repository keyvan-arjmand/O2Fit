using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddIsUpdateFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCopyable",
                table: "Foods");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdate",
                table: "Foods",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpdate",
                table: "Foods");

            migrationBuilder.AddColumn<bool>(
                name: "IsCopyable",
                table: "Foods",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
