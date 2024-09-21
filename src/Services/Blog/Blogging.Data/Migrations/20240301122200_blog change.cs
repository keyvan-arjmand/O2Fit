using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Blogging.Data.Migrations
{
    public partial class blogchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentApplications");

            migrationBuilder.DropTable(
                name: "CommentWebApplications");

            migrationBuilder.DropTable(
                name: "FeaturesInformations");

            migrationBuilder.DropTable(
                name: "FrequentlyResponses");

            migrationBuilder.DropTable(
                name: "LikeApplications");

            migrationBuilder.DropTable(
                name: "LikeWebApplications");

            migrationBuilder.DropTable(
                name: "ViewCountApplications");

            migrationBuilder.DropTable(
                name: "ViewCountWebApplications");

            migrationBuilder.DropTable(
                name: "FeaturesCategories");

            migrationBuilder.DropTable(
                name: "FrequentlyQuestions");

            migrationBuilder.DropTable(
                name: "BlogApplications");

            migrationBuilder.DropTable(
                name: "BlogWebApplications");

            migrationBuilder.DropTable(
                name: "FrequentlyQuestionCategories");

            migrationBuilder.DropTable(
                name: "SubBlogCategories");

            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Translations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    TitleId = table.Column<int>(nullable: false),
                    DescriptionId = table.Column<int>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    AltImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Translations_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Translations_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    AltImage = table.Column<string>(nullable: true),
                    TitleId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubCategories_Translations_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDelete = table.Column<bool>(nullable: false),
                    TitleId = table.Column<int>(nullable: false),
                    DescriptionId = table.Column<int>(nullable: false),
                    ShortDescriptionId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    AltImage = table.Column<string>(nullable: true),
                    ThumbName = table.Column<string>(nullable: true),
                    AltThumb = table.Column<string>(nullable: true),
                    BannerName = table.Column<string>(nullable: true),
                    AltBanner = table.Column<string>(nullable: true),
                    Like = table.Column<long>(nullable: false),
                    View = table.Column<long>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Translations_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_Translations_ShortDescriptionId",
                        column: x => x.ShortDescriptionId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_Translations_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_DescriptionId",
                table: "Blogs",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ShortDescriptionId",
                table: "Blogs",
                column: "ShortDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_SubCategoryId",
                table: "Blogs",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_TitleId",
                table: "Blogs",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DescriptionId",
                table: "Categories",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TitleId",
                table: "Categories",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_TitleId",
                table: "SubCategories",
                column: "TitleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Translations");

            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUri = table.Column<string>(type: "text", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    IsInPage = table.Column<bool>(type: "boolean", nullable: false),
                    NameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogCategories_Translations_NameId",
                        column: x => x.NameId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeaturesCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturesCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturesCategories_Translations_NameId",
                        column: x => x.NameId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FrequentlyQuestionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrequentlyQuestionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrequentlyQuestionCategories_Translations_NameId",
                        column: x => x.NameId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubBlogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogCategoryId = table.Column<int>(type: "integer", nullable: false),
                    DescId = table.Column<int>(type: "integer", nullable: false),
                    ImageUri = table.Column<string>(type: "text", nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    NameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubBlogCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubBlogCategories_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubBlogCategories_Translations_DescId",
                        column: x => x.DescId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubBlogCategories_Translations_NameId",
                        column: x => x.NameId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeaturesInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DescriptionId = table.Column<int>(type: "integer", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    ImageId = table.Column<int>(type: "integer", nullable: false),
                    SubTitleId = table.Column<int>(type: "integer", nullable: false),
                    TitleId = table.Column<int>(type: "integer", nullable: false),
                    VideoId = table.Column<int>(type: "integer", nullable: false),
                    featuresCategoryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturesInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_Translations_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_Translations_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_Translations_SubTitleId",
                        column: x => x.SubTitleId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_Translations_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_Translations_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesInformations_FeaturesCategories_featuresCategoryId",
                        column: x => x.featuresCategoryId,
                        principalTable: "FeaturesCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FrequentlyQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FrequentlyQuestionCategoryId = table.Column<int>(type: "integer", nullable: true),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    VideoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrequentlyQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrequentlyQuestions_FrequentlyQuestionCategories_Frequently~",
                        column: x => x.FrequentlyQuestionCategoryId,
                        principalTable: "FrequentlyQuestionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FrequentlyQuestions_Translations_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BannerUri = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FirstPagePost = table.Column<bool>(type: "boolean", nullable: false),
                    ImageUri = table.Column<string>(type: "text", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    Keyword = table.Column<string>(type: "text", nullable: true),
                    Language = table.Column<string>(type: "text", nullable: true),
                    ShortDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SubCategoryId = table.Column<int>(type: "integer", nullable: false),
                    ThumbUri = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    VideoUri = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogApplications_SubBlogCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubBlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogWebApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BannerUri = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FirstPagePost = table.Column<bool>(type: "boolean", nullable: false),
                    ImageUri = table.Column<string>(type: "text", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    Keyword = table.Column<string>(type: "text", nullable: true),
                    Language = table.Column<string>(type: "text", nullable: true),
                    ShortDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SubCategoryId = table.Column<int>(type: "integer", nullable: false),
                    ThumbUri = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    VideoUri = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogWebApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogWebApplications_SubBlogCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubBlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FrequentlyResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FrequentlyQuestionId = table.Column<int>(type: "integer", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    ResponseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrequentlyResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FrequentlyResponses_FrequentlyQuestions_FrequentlyQuestionId",
                        column: x => x.FrequentlyQuestionId,
                        principalTable: "FrequentlyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FrequentlyResponses_Translations_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogAppId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogAppId = table.Column<int>(type: "integer", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    IsLike = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogAppId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogWebId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogWebId = table.Column<int>(type: "integer", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    IsLike = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogWebId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
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
                name: "IX_BlogApplications_SubCategoryId",
                table: "BlogApplications",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_NameId",
                table: "BlogCategories",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogWebApplications_SubCategoryId",
                table: "BlogWebApplications",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentApplications_BlogAppId",
                table: "CommentApplications",
                column: "BlogAppId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentWebApplications_BlogWebId",
                table: "CommentWebApplications",
                column: "BlogWebId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesCategories_NameId",
                table: "FeaturesCategories",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_DescriptionId",
                table: "FeaturesInformations",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_ImageId",
                table: "FeaturesInformations",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_SubTitleId",
                table: "FeaturesInformations",
                column: "SubTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_TitleId",
                table: "FeaturesInformations",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_VideoId",
                table: "FeaturesInformations",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesInformations_featuresCategoryId",
                table: "FeaturesInformations",
                column: "featuresCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyQuestionCategories_NameId",
                table: "FrequentlyQuestionCategories",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyQuestions_FrequentlyQuestionCategoryId",
                table: "FrequentlyQuestions",
                column: "FrequentlyQuestionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyQuestions_QuestionId",
                table: "FrequentlyQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyResponses_FrequentlyQuestionId",
                table: "FrequentlyResponses",
                column: "FrequentlyQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FrequentlyResponses_ResponseId",
                table: "FrequentlyResponses",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeApplications_BlogAppId",
                table: "LikeApplications",
                column: "BlogAppId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeWebApplications_BlogWebId",
                table: "LikeWebApplications",
                column: "BlogWebId");

            migrationBuilder.CreateIndex(
                name: "IX_SubBlogCategories_BlogCategoryId",
                table: "SubBlogCategories",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubBlogCategories_DescId",
                table: "SubBlogCategories",
                column: "DescId");

            migrationBuilder.CreateIndex(
                name: "IX_SubBlogCategories_NameId",
                table: "SubBlogCategories",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewCountApplications_BlogAppId",
                table: "ViewCountApplications",
                column: "BlogAppId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewCountWebApplications_BlogWebId",
                table: "ViewCountWebApplications",
                column: "BlogWebId");
        }
    }
}
