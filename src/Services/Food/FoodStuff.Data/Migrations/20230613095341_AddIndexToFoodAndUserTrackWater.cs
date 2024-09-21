using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddIndexToFoodAndUserTrackWater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserTrackWaters_InsertDate_UserId",
                table: "UserTrackWaters",
                columns: new[] { "InsertDate", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_IsActive_IsDelete",
                table: "Foods",
                columns: new[] { "IsActive", "IsDelete" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserTrackWaters_InsertDate_UserId",
                table: "UserTrackWaters");

            migrationBuilder.DropIndex(
                name: "IX_Foods_IsActive_IsDelete",
                table: "Foods");
        }
    }
}
