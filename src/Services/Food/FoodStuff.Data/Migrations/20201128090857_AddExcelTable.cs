using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    public partial class AddExcelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcelTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<int>(nullable: false),
                    ArabicName = table.Column<string>(nullable: true),
                    EnglishName = table.Column<string>(nullable: true),
                    PersianName = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    IRCode = table.Column<string>(nullable: true),
                    GS1 = table.Column<string>(nullable: true),
                    V1 = table.Column<double>(nullable: false),
                    V2 = table.Column<double>(nullable: false),
                    V3 = table.Column<double>(nullable: false),
                    V4 = table.Column<double>(nullable: false),
                    V5 = table.Column<double>(nullable: false),
                    V6 = table.Column<double>(nullable: false),
                    V7 = table.Column<double>(nullable: false),
                    V8 = table.Column<double>(nullable: false),
                    V9 = table.Column<double>(nullable: false),
                    V10 = table.Column<double>(nullable: false),
                    V11 = table.Column<double>(nullable: false),
                    V12 = table.Column<double>(nullable: false),
                    V13 = table.Column<double>(nullable: false),
                    V14 = table.Column<double>(nullable: false),
                    V15 = table.Column<double>(nullable: false),
                    V16 = table.Column<double>(nullable: false),
                    V17 = table.Column<double>(nullable: false),
                    V18 = table.Column<double>(nullable: false),
                    V19 = table.Column<double>(nullable: false),
                    V20 = table.Column<double>(nullable: false),
                    V21 = table.Column<double>(nullable: false),
                    V22 = table.Column<double>(nullable: false),
                    V23 = table.Column<double>(nullable: false),
                    V24 = table.Column<double>(nullable: false),
                    V25 = table.Column<double>(nullable: false),
                    V26 = table.Column<double>(nullable: false),
                    V27 = table.Column<double>(nullable: false),
                    V28 = table.Column<double>(nullable: false),
                    V29 = table.Column<double>(nullable: false),
                    V30 = table.Column<double>(nullable: false),
                    V31 = table.Column<double>(nullable: false),
                    V32 = table.Column<double>(nullable: false),
                    V33 = table.Column<double>(nullable: false),
                    V34 = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelTables", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelTables");
        }
    }
}
