using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMessaging.Data.Migrations
{
    public partial class EditAppVersion1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppVersions_Version",
                table: "AppVersions",
                column: "Version",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppVersions_Version",
                table: "AppVersions");
        }
    }
}
