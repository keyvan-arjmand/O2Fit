using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddIsActiveAndIsDeleteToRecipeCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RecipeCategores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "RecipeCategores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategores_IsActive_IsDelete",
                table: "RecipeCategores",
                columns: new[] { "IsActive", "IsDelete" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecipeCategores_IsActive_IsDelete",
                table: "RecipeCategores");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RecipeCategores");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "RecipeCategores");
        }
    }
}
