using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddFoodTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Foods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Foods");
        }
    }
}
