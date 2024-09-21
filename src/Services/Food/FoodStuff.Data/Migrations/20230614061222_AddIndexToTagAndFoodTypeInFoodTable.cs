using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddIndexToTagAndFoodTypeInFoodTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Foods_IsActive_IsDelete",
                table: "Foods");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_IsActive_IsDelete_Tag_FoodType",
                table: "Foods",
                columns: new[] { "IsActive", "IsDelete", "Tag", "FoodType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Foods_IsActive_IsDelete_Tag_FoodType",
                table: "Foods");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_IsActive_IsDelete",
                table: "Foods",
                columns: new[] { "IsActive", "IsDelete" });
        }
    }
}
