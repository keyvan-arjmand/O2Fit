using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WorkoutTracker.Data.Migrations
{
    public partial class EditWorkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyMuscles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMuscles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BurnedWorkOutCalories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurnedWorkOutCalories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalWorkOuts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    Calorie = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalWorkOuts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Persian = table.Column<string>(nullable: true),
                    English = table.Column<string>(nullable: true),
                    Arabic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTrackSleeps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    Rate = table.Column<int>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    BurnedCalories = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackSleeps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTrackSteps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    StepsCount = table.Column<int>(nullable: false),
                    BurnedCalories = table.Column<double>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackSteps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(nullable: false),
                    ImageUri = table.Column<string>(nullable: true),
                    Classification = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOuts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(nullable: false),
                    IconUri = table.Column<string>(nullable: true),
                    ImageUri = table.Column<string>(nullable: true),
                    BurnedCalories = table.Column<double>(nullable: false),
                    VideoUrl = table.Column<string>(nullable: true),
                    WorkoutCategoryId = table.Column<int>(nullable: false),
                    RecommandationId = table.Column<int>(nullable: false),
                    HowToDoId = table.Column<int>(nullable: false),
                    Classification = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOuts_WorkoutCategories_WorkoutCategoryId",
                        column: x => x.WorkoutCategoryId,
                        principalTable: "WorkoutCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteWorkOuts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    WorkOutId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteWorkOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavoriteWorkOuts_WorkOuts_WorkOutId",
                        column: x => x.WorkOutId,
                        principalTable: "WorkOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOutAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkOutId = table.Column<int>(nullable: false),
                    NameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOutAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOutAttributes_WorkOuts_WorkOutId",
                        column: x => x.WorkOutId,
                        principalTable: "WorkOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutBodyMuscles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkoutId = table.Column<int>(nullable: false),
                    BodyMuscleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutBodyMuscles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutBodyMuscles_BodyMuscles_BodyMuscleId",
                        column: x => x.BodyMuscleId,
                        principalTable: "BodyMuscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutBodyMuscles_WorkOuts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "WorkOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOutAttributeValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkOutAttributeId = table.Column<int>(nullable: false),
                    NameId = table.Column<int>(nullable: false),
                    BurnedCalories = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOutAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOutAttributeValues_WorkOutAttributes_WorkOutAttributeId",
                        column: x => x.WorkOutAttributeId,
                        principalTable: "WorkOutAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTrackWorkOuts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkOutId = table.Column<int>(nullable: false),
                    BurnedCalories = table.Column<double>(nullable: false),
                    WorkOutAttributeValueId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackWorkOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTrackWorkOuts_WorkOutAttributeValues_WorkOutAttributeVa~",
                        column: x => x.WorkOutAttributeValueId,
                        principalTable: "WorkOutAttributeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTrackWorkOuts_WorkOuts_WorkOutId",
                        column: x => x.WorkOutId,
                        principalTable: "WorkOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteWorkOuts_WorkOutId",
                table: "UserFavoriteWorkOuts",
                column: "WorkOutId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackWorkOuts_WorkOutAttributeValueId",
                table: "UserTrackWorkOuts",
                column: "WorkOutAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackWorkOuts_WorkOutId",
                table: "UserTrackWorkOuts",
                column: "WorkOutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOutAttributes_WorkOutId",
                table: "WorkOutAttributes",
                column: "WorkOutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOutAttributeValues_WorkOutAttributeId",
                table: "WorkOutAttributeValues",
                column: "WorkOutAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutBodyMuscles_BodyMuscleId",
                table: "WorkoutBodyMuscles",
                column: "BodyMuscleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutBodyMuscles_WorkoutId",
                table: "WorkoutBodyMuscles",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOuts_WorkoutCategoryId",
                table: "WorkOuts",
                column: "WorkoutCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BurnedWorkOutCalories");

            migrationBuilder.DropTable(
                name: "PersonalWorkOuts");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "UserFavoriteWorkOuts");

            migrationBuilder.DropTable(
                name: "UserTrackSleeps");

            migrationBuilder.DropTable(
                name: "UserTrackSteps");

            migrationBuilder.DropTable(
                name: "UserTrackWorkOuts");

            migrationBuilder.DropTable(
                name: "WorkoutBodyMuscles");

            migrationBuilder.DropTable(
                name: "WorkOutAttributeValues");

            migrationBuilder.DropTable(
                name: "BodyMuscles");

            migrationBuilder.DropTable(
                name: "WorkOutAttributes");

            migrationBuilder.DropTable(
                name: "WorkOuts");

            migrationBuilder.DropTable(
                name: "WorkoutCategories");
        }
    }
}
