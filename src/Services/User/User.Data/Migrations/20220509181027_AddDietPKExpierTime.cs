using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Data.Migrations
{
    public partial class AddDietPKExpierTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DietPkExpireDate",
                table: "UserProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DietPkExpireDate",
                table: "UserProfiles");
        }
    }
}
