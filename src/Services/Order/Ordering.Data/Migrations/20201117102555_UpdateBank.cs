using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.Data.Migrations
{
    public partial class UpdateBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_OrderId",
                table: "BankTransactions",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BankTransactions_Orders_OrderId",
                table: "BankTransactions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankTransactions_Orders_OrderId",
                table: "BankTransactions");

            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_OrderId",
                table: "BankTransactions");
        }
    }
}
