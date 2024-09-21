using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodStuff.Data.Migrations
{
    public partial class ChangeRecipeStatusDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Recipes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(70)",
                oldMaxLength: 70);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Recipes",
                type: "character varying(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
