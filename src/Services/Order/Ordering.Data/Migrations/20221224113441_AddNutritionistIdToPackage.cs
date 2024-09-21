using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.Data.Migrations
{
    public partial class AddNutritionistIdToPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerData",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "customerType",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "NutritionistId",
                table: "Packages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NutritionistId",
                table: "Packages");

            migrationBuilder.AddColumn<string>(
                name: "CustomerData",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "customerType",
                table: "Orders",
                type: "integer",
                nullable: true);
        }
    }
}
