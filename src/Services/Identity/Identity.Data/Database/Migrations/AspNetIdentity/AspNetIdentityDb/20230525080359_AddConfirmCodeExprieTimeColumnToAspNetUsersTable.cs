using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Data.Database.Migrations.AspNetIdentity.AspNetIdentityDb
{
    public partial class AddConfirmCodeExprieTimeColumnToAspNetUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmCodeExpireTime",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmCodeExpireTime",
                table: "AspNetUsers");
        }
    }
}
