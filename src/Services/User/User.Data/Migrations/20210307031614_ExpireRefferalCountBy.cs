using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Data.Migrations
{
    public partial class ExpireRefferalCountBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PkExpireReferreralCountBuy",
                table: "UserProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PkExpireReferreralCountBuy",
                table: "UserProfiles");
        }
    }
}
