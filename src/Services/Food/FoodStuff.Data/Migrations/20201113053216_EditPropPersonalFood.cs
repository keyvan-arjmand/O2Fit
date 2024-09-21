using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class EditPropPersonalFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recip",
                table: "PersonalFoods");

            migrationBuilder.AddColumn<string>(
                name: "Recipe",
                table: "PersonalFoods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recipe",
                table: "PersonalFoods");

            migrationBuilder.AddColumn<string>(
                name: "Recip",
                table: "PersonalFoods",
                type: "text",
                nullable: true);
        }
    }
}
