using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Data.Database.Migrations.AspNetIdentity.AspNetIdentityDb
{
    public partial class ReferreralcountBuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReferreralCountBuy",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferreralCountBuy",
                table: "AspNetUsers");
        }
    }
}
