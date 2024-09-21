﻿// <auto-generated />
using System;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace User.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230223094255_AddIsPurchase")]
    partial class AddIsPurchase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Domain.UserDisability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Disability")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserDisabilities");
                });

            modelBuilder.Entity("Domain.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("BounsCount")
                        .HasColumnType("text");

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DailyActivityRate")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DietPkExpireDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("FirstPay")
                        .HasColumnType("boolean");

                    b.Property<int>("FoodHabit")
                        .HasColumnType("integer");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<double>("HeightSize")
                        .HasColumnType("double precision");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<DateTime?>("PkExpireDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("PkExpireReferreralCountBuy")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ReferreralCount")
                        .HasColumnType("text");

                    b.Property<double?>("TargetArm")
                        .HasColumnType("double precision");

                    b.Property<double?>("TargetBust")
                        .HasColumnType("double precision");

                    b.Property<double?>("TargetHighHip")
                        .HasColumnType("double precision");

                    b.Property<double?>("TargetHip")
                        .HasColumnType("double precision");

                    b.Property<double?>("TargetNeckSize")
                        .HasColumnType("double precision");

                    b.Property<string>("TargetNutrient")
                        .HasColumnType("text");

                    b.Property<double?>("TargetShoulder")
                        .HasColumnType("double precision");

                    b.Property<int?>("TargetStep")
                        .HasColumnType("integer");

                    b.Property<double?>("TargetThighSize")
                        .HasColumnType("double precision");

                    b.Property<double?>("TargetWaist")
                        .HasColumnType("double precision");

                    b.Property<double?>("TargetWater")
                        .HasColumnType("double precision");

                    b.Property<double>("TargetWeight")
                        .HasColumnType("double precision");

                    b.Property<double?>("TargetWrist")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<double>("Wallet")
                        .HasColumnType("double precision");

                    b.Property<double?>("WeightChangeRate")
                        .HasColumnType("double precision");

                    b.Property<int?>("WeightTimeRange")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Domain.UserTrackSpecification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double?>("ArmSize")
                        .HasColumnType("double precision");

                    b.Property<double?>("BustSize")
                        .HasColumnType("double precision");

                    b.Property<double?>("HighHipSize")
                        .HasColumnType("double precision");

                    b.Property<double?>("HipSize")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double?>("NeckSize")
                        .HasColumnType("double precision");

                    b.Property<double?>("ShoulderSize")
                        .HasColumnType("double precision");

                    b.Property<double?>("ThighSize")
                        .HasColumnType("double precision");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("integer");

                    b.Property<double?>("WaistSize")
                        .HasColumnType("double precision");

                    b.Property<double?>("WeightSize")
                        .HasColumnType("double precision");

                    b.Property<double?>("WristSize")
                        .HasColumnType("double precision");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserTrackSpecifications");
                });

            modelBuilder.Entity("User.Domain.Entities.FireBase.UsersFirebaseToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AppVersion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeviceModel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeviceOS")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirebaseToken")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UsersFirebaseTokens");
                });

            modelBuilder.Entity("User.Domain.Entities.Translation.Translation", b =>
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

            modelBuilder.Entity("User.Domain.Entities.User.DeviceInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AndroidId")
                        .HasColumnType("text");

                    b.Property<string>("ApiLevelSdk")
                        .HasColumnType("text");

                    b.Property<string>("AppVersion")
                        .HasColumnType("text");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Brightness")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Display")
                        .HasColumnType("text");

                    b.Property<string>("FontScale")
                        .HasColumnType("text");

                    b.Property<string>("IPAddress")
                        .HasColumnType("text");

                    b.Property<bool?>("IsEmulator")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsProfileComplete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPurchase")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsTablet")
                        .HasColumnType("boolean");

                    b.Property<string>("Market")
                        .HasColumnType("text");

                    b.Property<int>("OS")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneModel")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("DeviceInformations");
                });

            modelBuilder.Entity("User.Domain.Entities.User.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Text")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("User.Domain.Entities.User.SpecialDisease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SpecialDiseases");
                });

            modelBuilder.Entity("User.Domain.Entities.User.UserDietPackNutritionist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DietPkExpireDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DietType")
                        .HasColumnType("integer");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<int>("NutritionistId")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserDietPackNutritionists");
                });

            modelBuilder.Entity("User.Domain.Entities.User.UserProfileSpecialDisease", b =>
                {
                    b.Property<int>("UserProfileId")
                        .HasColumnType("integer");

                    b.Property<int>("SpecialDiseaseId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("UserProfileId", "SpecialDiseaseId");

                    b.HasIndex("SpecialDiseaseId");

                    b.ToTable("UserProfileSpecialDiseases");
                });

            modelBuilder.Entity("Domain.UserTrackSpecification", b =>
                {
                    b.HasOne("Domain.UserProfile", "UserProfiles")
                        .WithMany("UserTrackSpecifications")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("User.Domain.Entities.User.UserProfileSpecialDisease", b =>
                {
                    b.HasOne("User.Domain.Entities.User.SpecialDisease", "SpecialDiseases")
                        .WithMany("UserProfileSpecialDiseases")
                        .HasForeignKey("SpecialDiseaseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.UserProfile", "UserProfiles")
                        .WithMany("UserProfileSpecialDiseases")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
