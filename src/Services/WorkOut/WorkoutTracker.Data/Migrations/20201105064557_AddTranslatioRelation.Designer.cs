﻿// <auto-generated />
using System;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WorkoutTracker.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201105064557_AddTranslatioRelation")]
    partial class AddTranslatioRelation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.Translation.Translation", b =>
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

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.BodyMuscle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("BodyMuscles");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.BurnedWorkOutCalories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BurnedWorkOutCalories");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.PersonalWorkOut", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Calorie")
                        .HasColumnType("double precision");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PersonalWorkOuts");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.UserFavoriteWorkOut", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkOutId")
                        .HasColumnType("integer");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("WorkOutId");

                    b.ToTable("UserFavoriteWorkOuts");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.UserTrackSleep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("BurnedCalories")
                        .HasColumnType("double precision");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Rate")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserTrackSleeps");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.UserTrackSteps", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("BurnedCalories")
                        .HasColumnType("double precision");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("interval");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("StepsCount")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserTrackSteps");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.UserTrackWorkOut", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("BurnedCalories")
                        .HasColumnType("double precision");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("PersonalWorkOutId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("WorkOutAttributeValueId")
                        .HasColumnType("integer");

                    b.Property<int?>("WorkOutId")
                        .HasColumnType("integer");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PersonalWorkOutId");

                    b.HasIndex("WorkOutAttributeValueId");

                    b.HasIndex("WorkOutId");

                    b.ToTable("UserTrackWorkOuts");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.WorkOut", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("BurnedCalories")
                        .HasColumnType("double precision");

                    b.Property<int>("Classification")
                        .HasColumnType("integer");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<int>("HowToDoId")
                        .HasColumnType("integer");

                    b.Property<string>("IconUri")
                        .HasColumnType("text");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.Property<int>("RecommandationId")
                        .HasColumnType("integer");

                    b.Property<int>("TargetMuscle")
                        .HasColumnType("integer");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("WorkOuts");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.WorkOutAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkOutId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WorkOutId");

                    b.ToTable("WorkOutAttributes");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.WorkOutAttributeValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("BurnedCalories")
                        .HasColumnType("double precision");

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkOutAttributeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WorkOutAttributeId");

                    b.ToTable("WorkOutAttributeValues");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.WorkoutBodyMuscles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BodyMuscleId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BodyMuscleId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutBodyMuscles");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.UserFavoriteWorkOut", b =>
                {
                    b.HasOne("WorkoutTracker.Domain.Entities.WorkOut.WorkOut", "WorkOut")
                        .WithMany()
                        .HasForeignKey("WorkOutId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.UserTrackWorkOut", b =>
                {
                    b.HasOne("WorkoutTracker.Domain.Entities.WorkOut.PersonalWorkOut", "PersonalWorkOut")
                        .WithMany()
                        .HasForeignKey("PersonalWorkOutId");

                    b.HasOne("WorkoutTracker.Domain.Entities.WorkOut.WorkOutAttributeValue", "WorkOutAttributeValue")
                        .WithMany()
                        .HasForeignKey("WorkOutAttributeValueId");

                    b.HasOne("WorkoutTracker.Domain.Entities.WorkOut.WorkOut", "WorkOut")
                        .WithMany()
                        .HasForeignKey("WorkOutId");
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.WorkOut", b =>
                {
                    b.HasOne("WorkoutTracker.Domain.Entities.Translation.Translation", "Translation")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.WorkOutAttribute", b =>
                {
                    b.HasOne("WorkoutTracker.Domain.Entities.WorkOut.WorkOut", "WorkOut")
                        .WithMany("WorkOutAttribute")
                        .HasForeignKey("WorkOutId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.WorkOutAttributeValue", b =>
                {
                    b.HasOne("WorkoutTracker.Domain.Entities.WorkOut.WorkOutAttribute", "WorkOutAttribute")
                        .WithMany("WorkOutAttributeValue")
                        .HasForeignKey("WorkOutAttributeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutTracker.Domain.Entities.WorkOut.WorkoutBodyMuscles", b =>
                {
                    b.HasOne("WorkoutTracker.Domain.Entities.WorkOut.BodyMuscle", "BodyMuscle")
                        .WithMany("WorkoutBodyMuscles")
                        .HasForeignKey("BodyMuscleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WorkoutTracker.Domain.Entities.WorkOut.WorkOut", "WorkOut")
                        .WithMany("WorkoutBodyMuscles")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
