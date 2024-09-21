using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.Data.Migrations
{
    public partial class AddCustomerTypeToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerData",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "customerType",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerData",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "customerType",
                table: "Orders");
        }
    }
}
