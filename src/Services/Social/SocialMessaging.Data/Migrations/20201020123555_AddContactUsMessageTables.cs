using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SocialMessaging.Data.Migrations
{
    public partial class AddContactUsMessageTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactUsMessageReaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    MessageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsMessageReaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUsMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    AdminId = table.Column<int>(nullable: false),
                    ToAdmin = table.Column<bool>(nullable: false),
                    IsGeneral = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    IsForce = table.Column<bool>(nullable: false),
                    Classification = table.Column<int>(nullable: false),
                    IsReadAdmin = table.Column<bool>(nullable: false),
                    ReplyToMessage = table.Column<int>(nullable: false),
                    CanReply = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsMessages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUsMessageReaders");

            migrationBuilder.DropTable(
                name: "ContactUsMessages");
        }
    }
}
