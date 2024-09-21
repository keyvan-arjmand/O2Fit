using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMessaging.Data.Migrations
{
    public partial class AddIndexToContactUsMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ContactUsMessages_IsReadAdmin_IsForce_IsGeneral_ToAdmin",
                table: "ContactUsMessages",
                columns: new[] { "IsReadAdmin", "IsForce", "IsGeneral", "ToAdmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactUsMessages_IsReadAdmin_IsForce_IsGeneral_ToAdmin",
                table: "ContactUsMessages");
        }
    }
}
