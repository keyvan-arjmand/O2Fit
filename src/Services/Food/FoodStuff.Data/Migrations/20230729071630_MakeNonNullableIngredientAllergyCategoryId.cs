using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class MakeNonNullableIngredientAllergyCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IngredientAllergyCategoryId",
                table: "DietPackAlerges",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IngredientAllergyCategoryId",
                table: "DietPackAlerges",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
