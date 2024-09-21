using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMessaging.Data.Migrations
{
    public partial class EditContactUsMesage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUri",
                table: "ContactUsMessages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "ContactUsMessages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ContactUsMessages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUri",
                table: "ContactUsMessages");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "ContactUsMessages");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ContactUsMessages");
        }
    }
}
