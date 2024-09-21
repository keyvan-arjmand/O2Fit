using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Data.Database.Migrations.AspNetIdentity.AspNetIdentityDb
{
    public partial class AddConfirmCodeColumnToAspNetUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmCode",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmCode",
                table: "AspNetUsers");
        }
    }
}
