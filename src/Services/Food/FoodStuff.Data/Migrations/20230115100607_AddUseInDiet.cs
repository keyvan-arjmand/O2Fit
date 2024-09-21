using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class AddUseInDiet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UseInDiet",
                table: "Foods",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_DietPackSpecialDiseases_DietPackId",
                table: "DietPackSpecialDiseases",
                column: "DietPackId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietPackSpecialDiseases_DietPacks_DietPackId",
                table: "DietPackSpecialDiseases",
                column: "DietPackId",
                principalTable: "DietPacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietPackSpecialDiseases_DietPacks_DietPackId",
                table: "DietPackSpecialDiseases");

            migrationBuilder.DropIndex(
                name: "IX_DietPackSpecialDiseases_DietPackId",
                table: "DietPackSpecialDiseases");

            migrationBuilder.DropColumn(
                name: "UseInDiet",
                table: "Foods");
        }
    }
}
