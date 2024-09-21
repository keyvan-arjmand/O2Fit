﻿// <auto-generated />
using System;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Advertising.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230613094522_AddIndexToIsActiveInAdvertiseTable")]
    partial class AddIndexToIsActiveInAdvertiseTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Advertising.Domain.Entities.Advertise.Advertise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BannerUri")
                        .HasColumnType("text");

                    b.Property<double>("ChargeAmount")
                        .HasColumnType("double precision");

                    b.Property<int>("ClickCount")
                        .HasColumnType("integer");

                    b.Property<double>("ClickPrice")
                        .HasColumnType("double precision");

                    b.Property<int>("DescriptionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("HintCount")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("ShortDescriptionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TitleId")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<int>("ViewCount")
                        .HasColumnType("integer");

                    b.Property<double>("ViewPrice")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("DescriptionId");

                    b.HasIndex("IsActive");

                    b.HasIndex("ShortDescriptionId");

                    b.HasIndex("TitleId");

                    b.ToTable("Advertises");
                });

            modelBuilder.Entity("Advertising.Domain.Entities.Advertise.AdvertiseCountry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AdvertiseId")
                        .HasColumnType("integer");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AdvertiseId");

                    b.ToTable("AdvertiseCountries");
                });

            modelBuilder.Entity("Advertising.Domain.Entities.Translation.Translation", b =>
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

            modelBuilder.Entity("Advertising.Domain.Entities.Advertise.Advertise", b =>
                {
                    b.HasOne("Advertising.Domain.Entities.Translation.Translation", "TranslationDescription")
                        .WithMany()
                        .HasForeignKey("DescriptionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Advertising.Domain.Entities.Translation.Translation", "TranslationShortDescription")
                        .WithMany()
                        .HasForeignKey("ShortDescriptionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Advertising.Domain.Entities.Translation.Translation", "TranslationTitle")
                        .WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Advertising.Domain.Entities.Advertise.AdvertiseCountry", b =>
                {
                    b.HasOne("Advertising.Domain.Entities.Advertise.Advertise", "Advertise")
                        .WithMany("AdvertiseCountries")
                        .HasForeignKey("AdvertiseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
