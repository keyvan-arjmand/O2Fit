using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class checkpersonalfood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalFoodId",
                table: "UserTrackFoods");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonalFoodId",
                table: "UserTrackFoods",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
