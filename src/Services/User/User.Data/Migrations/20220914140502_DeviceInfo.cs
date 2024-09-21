using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace User.Data.Migrations
{
    public partial class DeviceInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceInformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    OS = table.Column<int>(nullable: false),
                    Brand = table.Column<string>(nullable: false),
                    PhoneModel = table.Column<string>(nullable: true),
                    Brightness = table.Column<string>(nullable: true),
                    IPAddress = table.Column<string>(nullable: true),
                    FontScale = table.Column<string>(nullable: true),
                    Display = table.Column<string>(nullable: true),
                    ApiLevelSdk = table.Column<string>(nullable: true),
                    AndroidId = table.Column<string>(nullable: true),
                    IsTablet = table.Column<bool>(nullable: true),
                    IsEmulator = table.Column<bool>(nullable: true),
                    AppVersion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceInformations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceInformations");
        }
    }
}
