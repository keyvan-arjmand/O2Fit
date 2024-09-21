using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Blogging.Data.Migrations
{
    public partial class Blog_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.CreateTable(
                name: "BlogApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(maxLength: 500, nullable: true),
                    ThumbUri = table.Column<string>(nullable: true),
                    ImageUri = table.Column<string>(nullable: true),
                    BannerUri = table.Column<string>(nullable: true),
                    VideoUri = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    Keyword = table.Column<string>(nullable: true),
                    FirstPagePost = table.Column<bool>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    BlogCategoryId = table.Column<int>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogApplications_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogWebApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(maxLength: 500, nullable: true),
                    ThumbUri = table.Column<string>(nullable: true),
                    ImageUri = table.Column<string>(nullable: true),
                    BannerUri = table.Column<string>(nullable: true),
                    VideoUri = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    Keyword = table.Column<string>(nullable: true),
                    FirstPagePost = table.Column<bool>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    BlogCategoryId = table.Column<int>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogWebApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogWebApplications_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    BlogAppId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 450, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentApplications_BlogApplications_BlogAppId",
                        column: x => x.BlogAppId,
                        principalTable: "BlogApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LikeApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    BlogAppId = table.Column<int>(nullable: false),
                    IsLike = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeApplications_BlogApplications_BlogAppId",
                        column: x => x.BlogAppId,
                        principalTable: "BlogApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ViewCountApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    BlogAppId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewCountApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewCountApplications_BlogApplications_BlogAppId",
                        column: x => x.BlogAppId,
                        principalTable: "BlogApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentWebApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    BlogWebId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 450, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentWebApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentWebApplications_BlogWebApplications_BlogWebId",
                        column: x => x.BlogWebId,
                        principalTable: "BlogWebApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LikeWebApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    BlogWebId = table.Column<int>(nullable: false),
                    IsLike = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeWebApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeWebApplications_BlogWebApplications_BlogWebId",
                        column: x => x.BlogWebId,
                        principalTable: "BlogWebApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ViewCountWebApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    BlogWebId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewCountWebApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewCountWebApplications_BlogWebApplications_BlogWebId",
                        column: x => x.BlogWebId,
                        principalTable: "BlogWebApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogApplications_BlogCategoryId",
                table: "BlogApplications",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogWebApplications_BlogCategoryId",
                table: "BlogWebApplications",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentApplications_BlogAppId",
                table: "CommentApplications",
                column: "BlogAppId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentWebApplications_BlogWebId",
                table: "CommentWebApplications",
                column: "BlogWebId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeApplications_BlogAppId",
                table: "LikeApplications",
                column: "BlogAppId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeWebApplications_BlogWebId",
                table: "LikeWebApplications",
                column: "BlogWebId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewCountApplications_BlogAppId",
                table: "ViewCountApplications",
                column: "BlogAppId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewCountWebApplications_BlogWebId",
                table: "ViewCountWebApplications",
                column: "BlogWebId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentApplications");

            migrationBuilder.DropTable(
                name: "CommentWebApplications");

            migrationBuilder.DropTable(
                name: "LikeApplications");

            migrationBuilder.DropTable(
                name: "LikeWebApplications");

            migrationBuilder.DropTable(
                name: "ViewCountApplications");

            migrationBuilder.DropTable(
                name: "ViewCountWebApplications");

            migrationBuilder.DropTable(
                name: "BlogApplications");

            migrationBuilder.DropTable(
                name: "BlogWebApplications");

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BannerUri = table.Column<string>(type: "text", nullable: true),
                    BlogCategoryId = table.Column<int>(type: "integer", nullable: true),
                    CommentCount = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FirstPagePost = table.Column<bool>(type: "boolean", nullable: false),
                    ImageUri = table.Column<string>(type: "text", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Keyword = table.Column<string>(type: "text", nullable: true),
                    Language = table.Column<string>(type: "text", nullable: true),
                    LikeCount = table.Column<int>(type: "integer", nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: true),
                    ThumbUri = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    VideoUri = table.Column<string>(type: "text", nullable: true),
                    ViewCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogCategoryId",
                table: "Blogs",
                column: "BlogCategoryId");
        }
    }
}
