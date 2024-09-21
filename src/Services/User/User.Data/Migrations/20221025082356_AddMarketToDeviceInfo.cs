using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Data.Migrations
{
    public partial class AddMarketToDeviceInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Market",
                table: "DeviceInformations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Market",
                table: "DeviceInformations");
        }
    }
}
