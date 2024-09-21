using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Data.Database.Migrations.AspNetIdentity.AspNetIdentityDb
{
    public partial class RemoveIndexFromRegisterDateInAspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RegisterDate",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RegisterDate",
                table: "AspNetUsers",
                column: "RegisterDate");
        }
    }
}
