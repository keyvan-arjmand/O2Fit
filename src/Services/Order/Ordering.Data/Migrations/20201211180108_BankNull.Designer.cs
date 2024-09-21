﻿// <auto-generated />
using System;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ordering.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201211180108_BankNull")]
    partial class BankNull
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Ordering.Domain.Entities.Discount.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.Property<int>("Percent")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("UsableCount")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Ordering.Domain.Entities.Order.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("Authority")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("DiscountId")
                        .HasColumnType("integer");

                    b.Property<int?>("DiscountUser")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpireTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Mobile")
                        .HasColumnType("text");

                    b.Property<int>("PackageId")
                        .HasColumnType("integer");

                    b.Property<string>("PostalCode")
                        .HasColumnType("text");

                    b.Property<string>("RefId")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.HasIndex("PackageId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Ordering.Domain.Entities.Package.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Currency")
                        .HasColumnType("integer");

                    b.Property<int>("DescriptionId")
                        .HasColumnType("integer");

                    b.Property<double?>("DiscountPercent")
                        .HasColumnType("double precision");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("Sort")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DescriptionId");

                    b.HasIndex("NameId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Ordering.Domain.Entities.Payment.BankTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("Authority")
                        .HasColumnType("text");

                    b.Property<int?>("Bank")
                        .HasColumnType("integer");

                    b.Property<string>("CID")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<string>("RefNum")
                        .HasColumnType("text");

                    b.Property<string>("ResNum")
                        .HasColumnType("text");

                    b.Property<string>("SecurePan")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("StateCode")
                        .HasColumnType("text");

                    b.Property<string>("TraceNo")
                        .HasColumnType("text");

                    b.Property<float>("Wage")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("BankTransactions");
                });

            modelBuilder.Entity("Ordering.Domain.Entities.Translation.Translation", b =>
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

            modelBuilder.Entity("Ordering.Domain.Entities.Wallet.WalletHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("Currency")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("WalletHistories");
                });

            modelBuilder.Entity("Ordering.Domain.Entities.Discount.Discount", b =>
                {
                    b.HasOne("Ordering.Domain.Entities.Translation.Translation", "Translation")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Ordering.Domain.Entities.Order.Order", b =>
                {
                    b.HasOne("Ordering.Domain.Entities.Discount.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId");

                    b.HasOne("Ordering.Domain.Entities.Package.Package", "Package")
                        .WithMany()
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Ordering.Domain.Entities.Package.Package", b =>
                {
                    b.HasOne("Ordering.Domain.Entities.Translation.Translation", "TranslationDescription")
                        .WithMany()
                        .HasForeignKey("DescriptionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ordering.Domain.Entities.Translation.Translation", "TranslationName")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Ordering.Domain.Entities.Payment.BankTransaction", b =>
                {
                    b.HasOne("Ordering.Domain.Entities.Order.Order", "Order")
                        .WithOne("BankTransaction")
                        .HasForeignKey("Ordering.Domain.Entities.Payment.BankTransaction", "OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
