﻿// <auto-generated />
using System;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SocialMessaging.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201020123555_AddContactUsMessageTables")]
    partial class AddContactUsMessageTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("SocialMessaging.Domain.Entities.ContactUs.ContactUsMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AdminId")
                        .HasColumnType("integer");

                    b.Property<bool>("CanReply")
                        .HasColumnType("boolean");

                    b.Property<int>("Classification")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsForce")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsGeneral")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsReadAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<int>("ReplyToMessage")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<bool>("ToAdmin")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ContactUsMessages");
                });

            modelBuilder.Entity("SocialMessaging.Domain.Entities.ContactUs.ContactUsMessageReader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("MessageId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ContactUsMessageReaders");
                });
#pragma warning restore 612, 618
        }
    }
}
