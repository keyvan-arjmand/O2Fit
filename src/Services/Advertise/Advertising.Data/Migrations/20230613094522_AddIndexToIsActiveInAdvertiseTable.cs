using Microsoft.EntityFrameworkCore.Migrations;

namespace Advertising.Data.Migrations
{
    public partial class AddIndexToIsActiveInAdvertiseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Advertises_IsActive",
                table: "Advertises",
                column: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Advertises_IsActive",
                table: "Advertises");
        }
    }
}
