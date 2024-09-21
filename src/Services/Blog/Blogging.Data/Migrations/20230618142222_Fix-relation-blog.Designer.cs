﻿// <auto-generated />
using System;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Blogging.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230618142222_Fix-relation-blog")]
    partial class Fixrelationblog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.BlogApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BannerUri")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("FirstPagePost")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Keyword")
                        .HasColumnType("text");

                    b.Property<string>("Language")
                        .HasColumnType("text");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("ThumbUri")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<string>("VideoUri")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("BlogApplications");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.BlogCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsInPage")
                        .HasColumnType("boolean");

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("BlogCategories");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.BlogWebApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BannerUri")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("FirstPagePost")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Keyword")
                        .HasColumnType("text");

                    b.Property<string>("Language")
                        .HasColumnType("text");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("ThumbUri")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<string>("VideoUri")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("BlogWebApplications");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.CommentApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlogAppId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(450)")
                        .HasMaxLength(450);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlogAppId");

                    b.ToTable("CommentApplications");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.CommentWebApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlogWebId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(450)")
                        .HasMaxLength(450);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlogWebId");

                    b.ToTable("CommentWebApplications");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.LikeApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlogAppId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLike")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlogAppId");

                    b.ToTable("LikeApplications");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.LikeWebApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlogWebId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLike")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlogWebId");

                    b.ToTable("LikeWebApplications");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.SubBlogCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlogCategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("DescId")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlogCategoryId");

                    b.HasIndex("DescId");

                    b.HasIndex("NameId");

                    b.ToTable("SubBlogCategories");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.ViewCountApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlogAppId")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlogAppId");

                    b.ToTable("ViewCountApplications");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.ViewCountWebApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BlogWebId")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlogWebId");

                    b.ToTable("ViewCountWebApplications");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Features.FeaturesCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("FeaturesCategories");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Features.FeaturesInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DescriptionId")
                        .HasColumnType("integer");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<int>("ImageId")
                        .HasColumnType("integer");

                    b.Property<int>("SubTitleId")
                        .HasColumnType("integer");

                    b.Property<int>("TitleId")
                        .HasColumnType("integer");

                    b.Property<int>("VideoId")
                        .HasColumnType("integer");

                    b.Property<int?>("featuresCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DescriptionId");

                    b.HasIndex("ImageId");

                    b.HasIndex("SubTitleId");

                    b.HasIndex("TitleId");

                    b.HasIndex("VideoId");

                    b.HasIndex("featuresCategoryId");

                    b.ToTable("FeaturesInformations");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.FrequentlyQuestion.FrequentlyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("FrequentlyQuestionCategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FrequentlyQuestionCategoryId");

                    b.HasIndex("QuestionId");

                    b.ToTable("FrequentlyQuestions");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.FrequentlyQuestion.FrequentlyQuestionCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("FrequentlyQuestionCategories");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.FrequentlyQuestion.FrequentlyResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("FrequentlyQuestionId")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<int>("ResponseId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FrequentlyQuestionId");

                    b.HasIndex("ResponseId");

                    b.ToTable("FrequentlyResponses");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Translation.Translation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Arabic")
                        .HasColumnType("text");

                    b.Property<string>("English")
                        .HasColumnType("text");

                    b.Property<string>("Persian")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Translations");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.BlogApplication", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Blog.SubBlogCategory", "SubBlogCategory")
                        .WithMany("BlogApps")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.BlogCategory", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "Translation")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.BlogWebApplication", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Blog.SubBlogCategory", "SubBlogCategory")
                        .WithMany("BlogWebs")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.CommentApplication", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Blog.BlogApplication", "BlogApplication")
                        .WithMany("CommentApplication")
                        .HasForeignKey("BlogAppId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.CommentWebApplication", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Blog.BlogWebApplication", "BlogWebApplication")
                        .WithMany("CommentWebApplication")
                        .HasForeignKey("BlogWebId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.LikeApplication", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Blog.BlogApplication", "BlogApplication")
                        .WithMany("LikeApplication")
                        .HasForeignKey("BlogAppId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.LikeWebApplication", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Blog.BlogWebApplication", "BlogWebApplication")
                        .WithMany("LikeWebApplication")
                        .HasForeignKey("BlogWebId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.SubBlogCategory", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Blog.BlogCategory", "BlogCategory")
                        .WithMany("SubBlogCategories")
                        .HasForeignKey("BlogCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "Description")
                        .WithMany()
                        .HasForeignKey("DescId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "Translation")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.ViewCountApplication", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Blog.BlogApplication", "BlogApplication")
                        .WithMany("ViewCountApplication")
                        .HasForeignKey("BlogAppId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Blog.ViewCountWebApplication", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Blog.BlogWebApplication", "BlogWebApplication")
                        .WithMany("ViewCountWebApplication")
                        .HasForeignKey("BlogWebId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Features.FeaturesCategory", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "Translation")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.Features.FeaturesInformation", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "Description")
                        .WithMany()
                        .HasForeignKey("DescriptionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "SubTitle")
                        .WithMany()
                        .HasForeignKey("SubTitleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "Title")
                        .WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Blogging.Domain.Entities.Features.FeaturesCategory", "featuresCategory")
                        .WithMany("featuresInformation")
                        .HasForeignKey("featuresCategoryId");
                });

            modelBuilder.Entity("Blogging.Domain.Entities.FrequentlyQuestion.FrequentlyQuestion", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.FrequentlyQuestion.FrequentlyQuestionCategory", "FrequentlyQuestionCategory")
                        .WithMany("FrequentlyQuestions")
                        .HasForeignKey("FrequentlyQuestionCategoryId");

                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "QuestionTranslation")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.FrequentlyQuestion.FrequentlyQuestionCategory", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "Translation")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Blogging.Domain.Entities.FrequentlyQuestion.FrequentlyResponse", b =>
                {
                    b.HasOne("Blogging.Domain.Entities.FrequentlyQuestion.FrequentlyQuestion", "FrequentlyQuestion")
                        .WithMany("FrequentlyResponses")
                        .HasForeignKey("FrequentlyQuestionId");

                    b.HasOne("Blogging.Domain.Entities.Translation.Translation", "ResponseTranslation")
                        .WithMany()
                        .HasForeignKey("ResponseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
