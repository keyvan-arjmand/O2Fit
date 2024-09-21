using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMessaging.Data.Migrations
{
    public partial class AddDescriptionToAppVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "AppVersions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "AppVersionMarketTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppVersions");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "AppVersionMarketTypes");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "AppVersions",
                type: "text",
                nullable: true);
        }
    }
}
