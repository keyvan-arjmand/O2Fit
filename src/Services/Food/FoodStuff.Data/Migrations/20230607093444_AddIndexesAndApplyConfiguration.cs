using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddIndexesAndApplyConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Recipes",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Tips_IsDelete",
                table: "Tips",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSteps_IsDelete",
                table: "RecipeSteps",
                column: "IsDelete");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_IsDelete_Status",
                table: "Recipes",
                columns: new[] { "IsDelete", "Status" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tips_IsDelete",
                table: "Tips");

            migrationBuilder.DropIndex(
                name: "IX_RecipeSteps_IsDelete",
                table: "RecipeSteps");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_IsDelete_Status",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Recipes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);
        }
    }
}
