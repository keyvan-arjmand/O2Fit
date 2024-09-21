using Microsoft.EntityFrameworkCore.Migrations;

namespace Advertising.Data.Migrations
{
    public partial class EditAdvertiseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxClickCount",
                table: "Advertises");

            migrationBuilder.DropColumn(
                name: "MaxView",
                table: "Advertises");

            migrationBuilder.DropColumn(
                name: "MaxViewCount",
                table: "Advertises");

            migrationBuilder.AddColumn<double>(
                name: "ChargeAmount",
                table: "Advertises",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ClickPrice",
                table: "Advertises",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "HintCount",
                table: "Advertises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Advertises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Advertises",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ViewPrice",
                table: "Advertises",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeAmount",
                table: "Advertises");

            migrationBuilder.DropColumn(
                name: "ClickPrice",
                table: "Advertises");

            migrationBuilder.DropColumn(
                name: "HintCount",
                table: "Advertises");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Advertises");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Advertises");

            migrationBuilder.DropColumn(
                name: "ViewPrice",
                table: "Advertises");

            migrationBuilder.AddColumn<int>(
                name: "MaxClickCount",
                table: "Advertises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxView",
                table: "Advertises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxViewCount",
                table: "Advertises",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
