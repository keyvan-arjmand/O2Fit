using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class AddFoodCommentAndLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonCount",
                table: "Foods",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FoodCommentAndLikes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FoodId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Like = table.Column<bool>(nullable: false),
                    AdminConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCommentAndLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodCommentAndLikes_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodCommentAndLikes_FoodId",
                table: "FoodCommentAndLikes",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodCommentAndLikes");

            migrationBuilder.DropColumn(
                name: "PersonCount",
                table: "Foods");
        }
    }
}
