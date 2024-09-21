using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddTagArEn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TagArEn",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TagArEn",
                table: "Foods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagArEn",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "TagArEn",
                table: "Foods");
        }
    }
}
