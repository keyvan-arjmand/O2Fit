using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMessaging.Data.Migrations
{
    public partial class EditLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "ContactUsMessages",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "ContactUsMessages",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
