using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class EditUserTrackDietPackDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "UserTrackDietPackDetails");

            migrationBuilder.AddColumn<int>(
                name: "DietPackId",
                table: "UserTrackDietPackDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackDietPackDetails_DietPackId",
                table: "UserTrackDietPackDetails",
                column: "DietPackId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrackDietPackDetails_DietPacks_DietPackId",
                table: "UserTrackDietPackDetails",
                column: "DietPackId",
                principalTable: "DietPacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTrackDietPackDetails_DietPacks_DietPackId",
                table: "UserTrackDietPackDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserTrackDietPackDetails_DietPackId",
                table: "UserTrackDietPackDetails");

            migrationBuilder.DropColumn(
                name: "DietPackId",
                table: "UserTrackDietPackDetails");

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "UserTrackDietPackDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
