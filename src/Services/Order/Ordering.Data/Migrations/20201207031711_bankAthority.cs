using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.Data.Migrations
{
    public partial class bankAthority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Authority",
                table: "BankTransactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authority",
                table: "BankTransactions");
        }
    }
}
