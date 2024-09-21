﻿// <auto-generated />
using System;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FoodStuff.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201223111640_EditPersonalFoodAddIngRelation")]
    partial class EditPersonalFoodAddIngRelation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("LogoUri")
                        .HasColumnType("text");

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.DailyTargetNutrient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FromAge")
                        .HasColumnType("integer");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<int>("Nutrient")
                        .HasColumnType("integer");

                    b.Property<string>("NutrientValue")
                        .HasColumnType("text");

                    b.Property<int>("ToAge")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("DailyTargetNutrients");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.ExcelTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ArabicName")
                        .HasColumnType("text");

                    b.Property<string>("Brand")
                        .HasColumnType("text");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<string>("EnglishName")
                        .HasColumnType("text");

                    b.Property<string>("GS1")
                        .HasColumnType("text");

                    b.Property<string>("IRCode")
                        .HasColumnType("text");

                    b.Property<string>("PersianName")
                        .HasColumnType("text");

                    b.Property<double>("V1")
                        .HasColumnType("double precision");

                    b.Property<double>("V10")
                        .HasColumnType("double precision");

                    b.Property<double>("V11")
                        .HasColumnType("double precision");

                    b.Property<double>("V12")
                        .HasColumnType("double precision");

                    b.Property<double>("V13")
                        .HasColumnType("double precision");

                    b.Property<double>("V14")
                        .HasColumnType("double precision");

                    b.Property<double>("V15")
                        .HasColumnType("double precision");

                    b.Property<double>("V16")
                        .HasColumnType("double precision");

                    b.Property<double>("V17")
                        .HasColumnType("double precision");

                    b.Property<double>("V18")
                        .HasColumnType("double precision");

                    b.Property<double>("V19")
                        .HasColumnType("double precision");

                    b.Property<double>("V2")
                        .HasColumnType("double precision");

                    b.Property<double>("V20")
                        .HasColumnType("double precision");

                    b.Property<double>("V21")
                        .HasColumnType("double precision");

                    b.Property<double>("V22")
                        .HasColumnType("double precision");

                    b.Property<double>("V23")
                        .HasColumnType("double precision");

                    b.Property<double>("V24")
                        .HasColumnType("double precision");

                    b.Property<double>("V25")
                        .HasColumnType("double precision");

                    b.Property<double>("V26")
                        .HasColumnType("double precision");

                    b.Property<double>("V27")
                        .HasColumnType("double precision");

                    b.Property<double>("V28")
                        .HasColumnType("double precision");

                    b.Property<double>("V29")
                        .HasColumnType("double precision");

                    b.Property<double>("V3")
                        .HasColumnType("double precision");

                    b.Property<double>("V30")
                        .HasColumnType("double precision");

                    b.Property<double>("V31")
                        .HasColumnType("double precision");

                    b.Property<double>("V32")
                        .HasColumnType("double precision");

                    b.Property<double>("V33")
                        .HasColumnType("double precision");

                    b.Property<double>("V34")
                        .HasColumnType("double precision");

                    b.Property<double>("V4")
                        .HasColumnType("double precision");

                    b.Property<double>("V5")
                        .HasColumnType("double precision");

                    b.Property<double>("V6")
                        .HasColumnType("double precision");

                    b.Property<double>("V7")
                        .HasColumnType("double precision");

                    b.Property<double>("V8")
                        .HasColumnType("double precision");

                    b.Property<double>("V9")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("ExcelTables");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<TimeSpan>("BakingTime")
                        .HasColumnType("interval");

                    b.Property<int>("BakingType")
                        .HasColumnType("integer");

                    b.Property<string>("BarcodeGs1")
                        .HasColumnType("text");

                    b.Property<string>("BarcodeNational")
                        .HasColumnType("text");

                    b.Property<int?>("BrandId")
                        .HasColumnType("integer");

                    b.Property<double>("DryIngredient")
                        .HasColumnType("double precision");

                    b.Property<double>("EvaporatedWater")
                        .HasColumnType("double precision");

                    b.Property<int>("FoodCode")
                        .HasColumnType("integer");

                    b.Property<int>("FoodHabit")
                        .HasColumnType("integer");

                    b.Property<int>("FoodType")
                        .HasColumnType("integer");

                    b.Property<string>("ImageThumb")
                        .HasColumnType("text");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUpdate")
                        .HasColumnType("boolean");

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.Property<string>("NutrientValue")
                        .HasColumnType("text");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("integer");

                    b.Property<double>("Version")
                        .HasColumnType("double precision");

                    b.Property<double>("WeightAfterBaking")
                        .HasColumnType("double precision");

                    b.Property<double>("WeightBeforBaking")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("NameId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.FoodIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FoodId")
                        .HasColumnType("integer");

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<double>("IngredientValue")
                        .HasColumnType("double precision");

                    b.Property<int>("MeasureUnitId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("MeasureUnitId");

                    b.ToTable("FoodIngredients");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.FoodMeasureUnit", b =>
                {
                    b.Property<int>("FoodId")
                        .HasColumnType("integer");

                    b.Property<int>("MeasureUnitId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("FoodId", "MeasureUnitId");

                    b.HasIndex("MeasureUnitId");

                    b.ToTable("FoodMeasureUnits");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<int?>("IngredientCategory")
                        .HasColumnType("integer");

                    b.Property<bool>("IsFood")
                        .HasColumnType("boolean");

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.Property<string>("NutrientValue")
                        .HasColumnType("text");

                    b.Property<string>("ThumbUri")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.IngredientMeasureUnit", b =>
                {
                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<int>("MeasureUnitId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("IngredientId", "MeasureUnitId");

                    b.HasIndex("MeasureUnitId");

                    b.ToTable("IngredientMeasureUnits");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.PersonalFood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<TimeSpan>("BakingTime")
                        .HasColumnType("interval");

                    b.Property<int>("BakingType")
                        .HasColumnType("integer");

                    b.Property<double>("DryIngredient")
                        .HasColumnType("double precision");

                    b.Property<double>("EvaporatedWater")
                        .HasColumnType("double precision");

                    b.Property<string>("ImageUri")
                        .HasColumnType("text");

                    b.Property<DateTime>("Insertdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsCopyable")
                        .HasColumnType("boolean");

                    b.Property<bool>("Isdelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NutrientValue")
                        .HasColumnType("text");

                    b.Property<int?>("ParentFoodId")
                        .HasColumnType("integer");

                    b.Property<string>("Recipe")
                        .HasColumnType("text");

                    b.Property<string>("ThumbUri")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<double>("Water")
                        .HasColumnType("double precision");

                    b.Property<double>("WeightAfterBaking")
                        .HasColumnType("double precision");

                    b.Property<double>("WeightBeforBaking")
                        .HasColumnType("double precision");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PersonalFoods");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.PersonalFoodIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<double>("IngredientValue")
                        .HasColumnType("double precision");

                    b.Property<int>("MeasureUnitId")
                        .HasColumnType("integer");

                    b.Property<int>("PersonalFoodId")
                        .HasColumnType("integer");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("MeasureUnitId");

                    b.HasIndex("PersonalFoodId");

                    b.ToTable("PersonalFoodIngredients");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.UserFoodAlergy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.ToTable("UserFoodAlergies");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.UserFoodFavorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FoodId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.ToTable("UserFoodFavorites");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.UserTrackFood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("FoodId")
                        .HasColumnType("integer");

                    b.Property<int>("FoodMeal")
                        .HasColumnType("integer");

                    b.Property<string>("FoodNutrientValue")
                        .HasColumnType("text");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MeasureUnitId")
                        .HasColumnType("integer");

                    b.Property<int?>("PersonalFoodId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("MeasureUnitId");

                    b.HasIndex("PersonalFoodId");

                    b.ToTable("UserTrackFoods");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.UserTrackNutrient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.Property<string>("_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserTrackNutrients");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.UserTrackWater", b =>
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

                    b.ToTable("UserTrackWaters");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.MeasureUnit.MeasureUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("MeasureUnitCategory")
                        .HasColumnType("integer");

                    b.Property<int>("NameId")
                        .HasColumnType("integer");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("NameId");

                    b.ToTable("MeasureUnits");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.MeasureUnit.NutrientMeasureUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("MeasureUnitId")
                        .HasColumnType("integer");

                    b.Property<int>("Nutrient")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MeasureUnitId");

                    b.ToTable("NutrientMeasureUnits");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Translation.Translation", b =>
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

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.Brand", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.Translation.Translation", "Translation")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.Food", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.Food.Brand", "Brand")
                        .WithMany("Foods")
                        .HasForeignKey("BrandId");

                    b.HasOne("FoodStuff.Domain.Entities.Translation.Translation", "TranslationName")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FoodStuff.Domain.Entities.Translation.Translation", "TranslationRecipe")
                        .WithMany()
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.FoodIngredient", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.Food.Food", "Food")
                        .WithMany("FoodIngredients")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FoodStuff.Domain.Entities.Food.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FoodStuff.Domain.Entities.MeasureUnit.MeasureUnit", "MeasureUnit")
                        .WithMany()
                        .HasForeignKey("MeasureUnitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.FoodMeasureUnit", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.Food.Food", "Food")
                        .WithMany("FoodMeasureUnits")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FoodStuff.Domain.Entities.MeasureUnit.MeasureUnit", "MeasureUnit")
                        .WithMany("FoodMeasureUnits")
                        .HasForeignKey("MeasureUnitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.Ingredient", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.Translation.Translation", "Translation")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.IngredientMeasureUnit", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.Food.Ingredient", "Ingredient")
                        .WithMany("IngredientMeasureUnits")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FoodStuff.Domain.Entities.MeasureUnit.MeasureUnit", "MeasureUnit")
                        .WithMany("IngredientMeasureUnits")
                        .HasForeignKey("MeasureUnitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.PersonalFoodIngredient", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.Food.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FoodStuff.Domain.Entities.MeasureUnit.MeasureUnit", "MeasureUnit")
                        .WithMany()
                        .HasForeignKey("MeasureUnitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FoodStuff.Domain.Entities.Food.PersonalFood", "PersonalFood")
                        .WithMany("FoodIngredients")
                        .HasForeignKey("PersonalFoodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.UserFoodAlergy", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.Food.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.UserFoodFavorite", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.Food.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.Food.UserTrackFood", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.Food.Food", "Food")
                        .WithMany("UserTrackFoods")
                        .HasForeignKey("FoodId");

                    b.HasOne("FoodStuff.Domain.Entities.MeasureUnit.MeasureUnit", "MeasureUnit")
                        .WithMany()
                        .HasForeignKey("MeasureUnitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FoodStuff.Domain.Entities.Food.PersonalFood", "PersonalFood")
                        .WithMany("UserTrackFoods")
                        .HasForeignKey("PersonalFoodId");
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.MeasureUnit.MeasureUnit", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.Translation.Translation", "Translation")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodStuff.Domain.Entities.MeasureUnit.NutrientMeasureUnit", b =>
                {
                    b.HasOne("FoodStuff.Domain.Entities.MeasureUnit.MeasureUnit", "MeasureUnit")
                        .WithMany("NutrientMeasureUnits")
                        .HasForeignKey("MeasureUnitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
