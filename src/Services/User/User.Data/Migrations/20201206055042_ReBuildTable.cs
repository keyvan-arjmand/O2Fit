using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace User.Data.Migrations
{
    public partial class ReBuildTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecialDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    _id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDiseases", x => x.Id);
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
                name: "UserDisabilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    Disability = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDisabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    ImageUri = table.Column<string>(nullable: true),
                    WeightChangeRate = table.Column<double>(nullable: true),
                    FoodHabit = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    WeightTimeRange = table.Column<int>(nullable: true),
                    TargetStep = table.Column<int>(nullable: true),
                    TargetWeight = table.Column<double>(nullable: false),
                    TargetBust = table.Column<double>(nullable: true),
                    TargetArm = table.Column<double>(nullable: true),
                    TargetWaist = table.Column<double>(nullable: true),
                    TargetHighHip = table.Column<double>(nullable: true),
                    TargetThighSize = table.Column<double>(nullable: true),
                    TargetNeckSize = table.Column<double>(nullable: true),
                    TargetHip = table.Column<double>(nullable: true),
                    TargetShoulder = table.Column<double>(nullable: true),
                    TargetWrist = table.Column<double>(nullable: true),
                    HeightSize = table.Column<double>(nullable: false),
                    TargetWater = table.Column<double>(nullable: true),
                    DailyActivityRate = table.Column<int>(nullable: false),
                    PkExpireDate = table.Column<DateTime>(nullable: false),
                    ReferreralCount = table.Column<string>(nullable: true),
                    BounsCount = table.Column<string>(nullable: true),
                    FirstPay = table.Column<bool>(nullable: false),
                    Wallet = table.Column<double>(nullable: false),
                    TargetNutrient = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileSpecialDiseases",
                columns: table => new
                {
                    SpecialDiseaseId = table.Column<int>(nullable: false),
                    UserProfileId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileSpecialDiseases", x => new { x.UserProfileId, x.SpecialDiseaseId });
                    table.ForeignKey(
                        name: "FK_UserProfileSpecialDiseases_SpecialDiseases_SpecialDiseaseId",
                        column: x => x.SpecialDiseaseId,
                        principalTable: "SpecialDiseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfileSpecialDiseases_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTrackSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeightSize = table.Column<double>(nullable: true),
                    NeckSize = table.Column<double>(nullable: true),
                    ShoulderSize = table.Column<double>(nullable: true),
                    ArmSize = table.Column<double>(nullable: true),
                    WristSize = table.Column<double>(nullable: true),
                    BustSize = table.Column<double>(nullable: true),
                    WaistSize = table.Column<double>(nullable: true),
                    HighHipSize = table.Column<double>(nullable: true),
                    HipSize = table.Column<double>(nullable: true),
                    ThighSize = table.Column<double>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    UserProfileId = table.Column<int>(nullable: false),
                    _id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrackSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTrackSpecifications_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileSpecialDiseases_SpecialDiseaseId",
                table: "UserProfileSpecialDiseases",
                column: "SpecialDiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrackSpecifications_UserProfileId",
                table: "UserTrackSpecifications",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "UserDisabilities");

            migrationBuilder.DropTable(
                name: "UserProfileSpecialDiseases");

            migrationBuilder.DropTable(
                name: "UserTrackSpecifications");

            migrationBuilder.DropTable(
                name: "SpecialDiseases");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
