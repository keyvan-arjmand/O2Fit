using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class checkpersonalfood2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FoodId",
                table: "UserTrackFoods",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PersonalFoodId",
                table: "UserTrackFoods",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackFoods_PersonalFoodId",
                table: "UserTrackFoods",
                column: "PersonalFoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrackFoods_PersonalFoods_PersonalFoodId",
                table: "UserTrackFoods",
                column: "PersonalFoodId",
                principalTable: "PersonalFoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTrackFoods_PersonalFoods_PersonalFoodId",
                table: "UserTrackFoods");

            migrationBuilder.DropIndex(
                name: "IX_UserTrackFoods_PersonalFoodId",
                table: "UserTrackFoods");

            migrationBuilder.DropColumn(
                name: "PersonalFoodId",
                table: "UserTrackFoods");

            migrationBuilder.AlterColumn<int>(
                name: "FoodId",
                table: "UserTrackFoods",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
