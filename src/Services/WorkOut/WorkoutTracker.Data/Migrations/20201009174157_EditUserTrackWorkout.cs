using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WorkoutTracker.Data.Migrations
{
    public partial class EditUserTrackWorkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOuts_WorkoutCategories_WorkoutCategoryId",
                table: "WorkOuts");

            migrationBuilder.DropTable(
                name: "WorkoutCategories");

            migrationBuilder.DropIndex(
                name: "IX_WorkOuts_WorkoutCategoryId",
                table: "WorkOuts");

            migrationBuilder.DropColumn(
                name: "WorkoutCategoryId",
                table: "WorkOuts");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "WorkOuts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetMuscle",
                table: "WorkOuts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WorkOutId",
                table: "UserTrackWorkOuts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "WorkOutAttributeValueId",
                table: "UserTrackWorkOuts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PersonalWorkOutId",
                table: "UserTrackWorkOuts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackWorkOuts_PersonalWorkOutId",
                table: "UserTrackWorkOuts",
                column: "PersonalWorkOutId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrackWorkOuts_PersonalWorkOuts_PersonalWorkOutId",
                table: "UserTrackWorkOuts",
                column: "PersonalWorkOutId",
                principalTable: "PersonalWorkOuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTrackWorkOuts_PersonalWorkOuts_PersonalWorkOutId",
                table: "UserTrackWorkOuts");

            migrationBuilder.DropIndex(
                name: "IX_UserTrackWorkOuts_PersonalWorkOutId",
                table: "UserTrackWorkOuts");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "WorkOuts");

            migrationBuilder.DropColumn(
                name: "TargetMuscle",
                table: "WorkOuts");

            migrationBuilder.DropColumn(
                name: "PersonalWorkOutId",
                table: "UserTrackWorkOuts");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutCategoryId",
                table: "WorkOuts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WorkOutId",
                table: "UserTrackWorkOuts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkOutAttributeValueId",
                table: "UserTrackWorkOuts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "WorkoutCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Classification = table.Column<int>(type: "integer", nullable: false),
                    ImageUri = table.Column<string>(type: "text", nullable: true),
                    NameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOuts_WorkoutCategoryId",
                table: "WorkOuts",
                column: "WorkoutCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOuts_WorkoutCategories_WorkoutCategoryId",
                table: "WorkOuts",
                column: "WorkoutCategoryId",
                principalTable: "WorkoutCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
