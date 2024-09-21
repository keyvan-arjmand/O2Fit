using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Data.Migrations
{
    public partial class AddDeviceInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FirebaseToken",
                table: "UsersFirebaseTokens",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "UsersFirebaseTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppVersion",
                table: "UsersFirebaseTokens",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeviceModel",
                table: "UsersFirebaseTokens",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeviceOS",
                table: "UsersFirebaseTokens",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppVersion",
                table: "UsersFirebaseTokens");

            migrationBuilder.DropColumn(
                name: "DeviceModel",
                table: "UsersFirebaseTokens");

            migrationBuilder.DropColumn(
                name: "DeviceOS",
                table: "UsersFirebaseTokens");

            migrationBuilder.AlterColumn<string>(
                name: "FirebaseToken",
                table: "UsersFirebaseTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "UsersFirebaseTokens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
