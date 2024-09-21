using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.Data.Migrations
{
    public partial class orderingRefId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefId",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefId",
                table: "Orders");
        }
    }
}
