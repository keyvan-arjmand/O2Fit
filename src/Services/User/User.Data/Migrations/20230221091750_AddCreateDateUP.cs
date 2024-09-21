using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace User.Data.Migrations
{
    public partial class AddCreateDateUP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "UserProfiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "UserDietPackNutritionists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    NutritionistId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    DietType = table.Column<int>(nullable: false),
                    DietPkExpireDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDietPackNutritionists", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDietPackNutritionists");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "UserProfiles");
        }
    }
}
