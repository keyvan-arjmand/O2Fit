using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.Data.Migrations
{
    public partial class SaleRefrenceid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SaleReferenceId",
                table: "BankTransactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleReferenceId",
                table: "BankTransactions");
        }
    }
}
