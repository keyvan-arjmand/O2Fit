using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class EditUserTrackDietDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTrackDietPackDetails_UserTrackDietPackDietPackDetails_U~",
                table: "UserTrackDietPackDetails");

            migrationBuilder.DropTable(
                name: "UserTrackDietPackDietPackDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserTrackDietPackDetails_UserTrackDietPackDetailId",
                table: "UserTrackDietPackDetails");

            migrationBuilder.DropColumn(
                name: "UserTrackDietPackDetailId",
                table: "UserTrackDietPackDetails");

            migrationBuilder.AddColumn<int>(
                name: "UserTrackDietPackId",
                table: "UserTrackDietPackDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackDietPackDetails_UserTrackDietPackId",
                table: "UserTrackDietPackDetails",
                column: "UserTrackDietPackId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrackDietPackDetails_UserTrackDietPacks_UserTrackDietPa~",
                table: "UserTrackDietPackDetails",
                column: "UserTrackDietPackId",
                principalTable: "UserTrackDietPacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTrackDietPackDetails_UserTrackDietPacks_UserTrackDietPa~",
                table: "UserTrackDietPackDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserTrackDietPackDetails_UserTrackDietPackId",
                table: "UserTrackDietPackDetails");

            migrationBuilder.DropColumn(
                name: "UserTrackDietPackId",
                table: "UserTrackDietPackDetails");

            migrationBuilder.AddColumn<int>(
                name: "UserTrackDietPackDetailId",
                table: "UserTrackDietPackDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserTrackDietPackDietPackDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserTrackDietPackDetailId = table.Column<int>(type: "integer", nullable: false),
                    UserTrackDietPackId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackDietPackDietPackDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTrackDietPackDietPackDetails_UserTrackDietPacks_UserTra~",
                        column: x => x.UserTrackDietPackId,
                        principalTable: "UserTrackDietPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackDietPackDetails_UserTrackDietPackDetailId",
                table: "UserTrackDietPackDetails",
                column: "UserTrackDietPackDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackDietPackDietPackDetails_UserTrackDietPackId",
                table: "UserTrackDietPackDietPackDetails",
                column: "UserTrackDietPackId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrackDietPackDetails_UserTrackDietPackDietPackDetails_U~",
                table: "UserTrackDietPackDetails",
                column: "UserTrackDietPackDetailId",
                principalTable: "UserTrackDietPackDietPackDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
