using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class AddFoodHabit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodHabit",
                table: "Foods");

            migrationBuilder.CreateTable(
                name: "FoodFoodHabits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FoodId = table.Column<int>(nullable: false),
                    FoodHabit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodFoodHabits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodFoodHabits_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodFoodHabits_FoodId",
                table: "FoodFoodHabits",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodFoodHabits");

            migrationBuilder.AddColumn<int>(
                name: "FoodHabit",
                table: "Foods",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
