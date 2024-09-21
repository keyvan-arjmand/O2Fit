using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class AddDietPackSpecialDisease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialDisease",
                table: "DietPacks");

            migrationBuilder.CreateTable(
                name: "DietPackSpecialDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DietPackId = table.Column<int>(nullable: false),
                    SpecialDisease = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPackSpecialDiseases", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietPackSpecialDiseases");

            migrationBuilder.AddColumn<int[]>(
                name: "SpecialDisease",
                table: "DietPacks",
                type: "integer[]",
                nullable: true);
        }
    }
}
