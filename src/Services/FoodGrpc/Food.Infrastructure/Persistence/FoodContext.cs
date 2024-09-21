using System;
using System.Collections.Generic;
using Food.Domain.Entities;

namespace Food.Infrastructure.Persistence;

public partial class FoodContext : DbContext
{
    public FoodContext()
    {
    }

    public FoodContext(DbContextOptions<FoodContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DailyTargetNutrient> DailyTargetNutrients { get; set; }

    public virtual DbSet<DietAlergy> DietAlergies { get; set; }

    public virtual DbSet<DietCategory> DietCategories { get; set; }

    public virtual DbSet<DietPack> DietPacks { get; set; }

    public virtual DbSet<DietPackAlerge> DietPackAlerges { get; set; }

    public virtual DbSet<DietPackDietCategory> DietPackDietCategories { get; set; }

    public virtual DbSet<DietPackFood> DietPackFoods { get; set; }

    public virtual DbSet<DietPackNationality> DietPackNationalities { get; set; }

    public virtual DbSet<DietPackSpecialDisease> DietPackSpecialDiseases { get; set; }

    public virtual DbSet<DietPackTemp> DietPackTemps { get; set; }

    public virtual DbSet<ExcelTable> ExcelTables { get; set; }

    public virtual DbSet<Domain.Entities.Food> Foods { get; set; }

    public virtual DbSet<FoodCategory> FoodCategories { get; set; }

    public virtual DbSet<FoodCommentAndLike> FoodCommentAndLikes { get; set; }

    public virtual DbSet<FoodDietCategory> FoodDietCategories { get; set; }

    public virtual DbSet<FoodFoodHabit> FoodFoodHabits { get; set; }

    public virtual DbSet<FoodIngredient> FoodIngredients { get; set; }

    public virtual DbSet<FoodMeasureUnit> FoodMeasureUnits { get; set; }

    public virtual DbSet<FoodNationality> FoodNationalities { get; set; }

    public virtual DbSet<FoodSpecialDisease> FoodSpecialDiseases { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientAllergy> IngredientAllergies { get; set; }

    public virtual DbSet<IngredientMeasureUnit> IngredientMeasureUnits { get; set; }

    public virtual DbSet<MeasureUnit> MeasureUnits { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<NutrientMeasureUnit> NutrientMeasureUnits { get; set; }

    public virtual DbSet<PersonalFood> PersonalFoods { get; set; }

    public virtual DbSet<PersonalFoodIngredient> PersonalFoodIngredients { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeCategore> RecipeCategores { get; set; }

    public virtual DbSet<RecipeStep> RecipeSteps { get; set; }

    public virtual DbSet<TempOldRecipePersian> TempOldRecipePersians { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<Translation> Translations { get; set; }

    public virtual DbSet<UserFoodAlergy> UserFoodAlergies { get; set; }

    public virtual DbSet<UserFoodFavorite> UserFoodFavorites { get; set; }

    public virtual DbSet<UserReportedFood> UserReportedFoods { get; set; }

    public virtual DbSet<UserTrackDietPack> UserTrackDietPacks { get; set; }

    public virtual DbSet<UserTrackDietPackDetail> UserTrackDietPackDetails { get; set; }

    public virtual DbSet<UserTrackFood> UserTrackFoods { get; set; }

    public virtual DbSet<UserTrackNutrient> UserTrackNutrients { get; set; }

    public virtual DbSet<UserTrackWater> UserTrackWaters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:Npgsql");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasIndex(e => e.NameId, "IX_Brands_NameId");

            entity.HasOne(d => d.Name).WithMany(p => p.Brands)
                .HasForeignKey(d => d.NameId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.NameId, "IX_Categories_NameId");

            entity.HasOne(d => d.Name).WithMany(p => p.Categories)
                .HasForeignKey(d => d.NameId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DietAlergy>(entity =>
        {
            entity.HasIndex(e => e.MainAlergyId, "IX_DietAlergies_MainAlergyId");

            entity.HasOne(d => d.MainAlergy).WithMany(p => p.DietAlergies)
                .HasForeignKey(d => d.MainAlergyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DietCategory>(entity =>
        {
            entity.HasIndex(e => e.DescriptionId, "IX_DietCategories_DescriptionId");

            entity.HasIndex(e => e.NameId, "IX_DietCategories_NameId");

            entity.HasOne(d => d.Description).WithMany(p => p.DietCategoryDescriptions)
                .HasForeignKey(d => d.DescriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Name).WithMany(p => p.DietCategoryNames)
                .HasForeignKey(d => d.NameId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DietPack>(entity =>
        {
            entity.HasIndex(e => e.IsActive, "IX_DietPacks_IsActive");

            entity.HasIndex(e => e.NameId, "IX_DietPacks_NameId");

            entity.HasOne(d => d.Name).WithMany(p => p.DietPacks)
                .HasForeignKey(d => d.NameId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DietPackAlerge>(entity =>
        {
            entity.HasIndex(e => e.DietPackId, "IX_DietPackAlerges_DietPackId");

            entity.HasIndex(e => e.IngredientId, "IX_DietPackAlerges_IngredientId");

            entity.HasOne(d => d.DietPack).WithMany(p => p.DietPackAlerges)
                .HasForeignKey(d => d.DietPackId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Ingredient).WithMany(p => p.DietPackAlerges)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DietPackDietCategory>(entity =>
        {
            entity.HasIndex(e => e.DietCategoryId, "IX_DietPackDietCategories_DietCategoryId");

            entity.HasIndex(e => e.DietPackId, "IX_DietPackDietCategories_DietPackId");

            entity.HasOne(d => d.DietCategory).WithMany(p => p.DietPackDietCategories)
                .HasForeignKey(d => d.DietCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.DietPack).WithMany(p => p.DietPackDietCategories)
                .HasForeignKey(d => d.DietPackId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DietPackFood>(entity =>
        {
            entity.HasIndex(e => e.DietPackId, "IX_DietPackFoods_DietPackId");

            entity.HasIndex(e => e.FoodId, "IX_DietPackFoods_FoodId");

            entity.HasIndex(e => e.MeasureUnitId, "IX_DietPackFoods_MeasureUnitId");

            entity.HasOne(d => d.DietPack).WithMany(p => p.DietPackFoods)
                .HasForeignKey(d => d.DietPackId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Food).WithMany(p => p.DietPackFoods)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.MeasureUnit).WithMany(p => p.DietPackFoods)
                .HasForeignKey(d => d.MeasureUnitId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DietPackNationality>(entity =>
        {
            entity.HasIndex(e => e.DietPackId, "IX_DietPackNationalities_DietPackId");

            entity.HasIndex(e => e.NationalityId, "IX_DietPackNationalities_NationalityId");

            entity.HasOne(d => d.DietPack).WithMany(p => p.DietPackNationalities)
                .HasForeignKey(d => d.DietPackId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Nationality).WithMany(p => p.DietPackNationalities)
                .HasForeignKey(d => d.NationalityId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DietPackSpecialDisease>(entity =>
        {
            entity.HasIndex(e => e.DietPackId, "IX_DietPackSpecialDiseases_DietPackId");

            entity.HasOne(d => d.DietPack).WithMany(p => p.DietPackSpecialDiseases)
                .HasForeignKey(d => d.DietPackId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DietPackTemp>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("diet_pack_temp");

            entity.Property(e => e.AllergyId).HasColumnName("Allergy_Id");
        });

        modelBuilder.Entity<ExcelTable>(entity =>
        {
            entity.Property(e => e.Gs1).HasColumnName("GS1");
            entity.Property(e => e.Ircode).HasColumnName("IRCode");
        });

        modelBuilder.Entity<Domain.Entities.Food>(entity =>
        {
            entity.HasIndex(e => e.BrandId, "IX_Foods_BrandId");

            entity.HasIndex(e => new { e.IsActive, e.IsDelete, e.Tag, e.FoodType }, "IX_Foods_IsActive_IsDelete_Tag_FoodType");

            entity.HasIndex(e => e.NameId, "IX_Foods_NameId");

            entity.HasIndex(e => e.RecipeId, "IX_Foods_RecipeId");

            entity.Property(e => e.DefaultMeasureUnitId).HasDefaultValueSql("36");
            entity.Property(e => e.Gi)
                .HasDefaultValueSql("0.0")
                .HasColumnName("GI");

            entity.HasOne(d => d.Brand).WithMany(p => p.Foods)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Name).WithMany(p => p.FoodNames)
                .HasForeignKey(d => d.NameId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Recipe).WithMany(p => p.FoodRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FoodCategory>(entity =>
        {
            entity.HasKey(e => new { e.FoodId, e.CategoryId });

            entity.HasIndex(e => e.CategoryId, "IX_FoodCategories_CategoryId");

            entity.HasOne(d => d.Category).WithMany(p => p.FoodCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Food).WithMany(p => p.FoodCategories)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FoodCommentAndLike>(entity =>
        {
            entity.HasIndex(e => e.FoodId, "IX_FoodCommentAndLikes_FoodId");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodCommentAndLikes)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FoodDietCategory>(entity =>
        {
            entity.HasKey(e => new { e.FoodId, e.DietCategoryId });

            entity.HasIndex(e => e.DietCategoryId, "IX_FoodDietCategories_DietCategoryId");

            entity.HasOne(d => d.DietCategory).WithMany(p => p.FoodDietCategories)
                .HasForeignKey(d => d.DietCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Food).WithMany(p => p.FoodDietCategories)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FoodFoodHabit>(entity =>
        {
            entity.HasIndex(e => e.FoodId, "IX_FoodFoodHabits_FoodId");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodFoodHabits)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FoodIngredient>(entity =>
        {
            entity.HasIndex(e => e.FoodId, "IX_FoodIngredients_FoodId");

            entity.HasIndex(e => e.IngredientId, "IX_FoodIngredients_IngredientId");

            entity.HasIndex(e => e.MeasureUnitId, "IX_FoodIngredients_MeasureUnitId");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodIngredients)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Ingredient).WithMany(p => p.FoodIngredients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.MeasureUnit).WithMany(p => p.FoodIngredients)
                .HasForeignKey(d => d.MeasureUnitId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FoodMeasureUnit>(entity =>
        {
            entity.HasKey(e => new { e.FoodId, e.MeasureUnitId });

            entity.HasIndex(e => e.MeasureUnitId, "IX_FoodMeasureUnits_MeasureUnitId");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodMeasureUnits)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.MeasureUnit).WithMany(p => p.FoodMeasureUnits)
                .HasForeignKey(d => d.MeasureUnitId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FoodNationality>(entity =>
        {
            entity.HasKey(e => new { e.FoodId, e.NationalityId });

            entity.HasIndex(e => e.NationalityId, "IX_FoodNationalities_NationalityId");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodNationalities)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Nationality).WithMany(p => p.FoodNationalities)
                .HasForeignKey(d => d.NationalityId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FoodSpecialDisease>(entity =>
        {
            entity.HasIndex(e => e.FoodId, "IX_FoodSpecialDiseases_FoodId");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodSpecialDiseases)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasIndex(e => e.NameId, "IX_Ingredients_NameId");

            entity.Property(e => e.DefaultMeasureUnitId).HasDefaultValueSql("36");

            entity.HasOne(d => d.Name).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.NameId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<IngredientAllergy>(entity =>
        {
            entity.HasIndex(e => e.IngredientId, "IX_IngredientAllergies_IngredientId");

            entity.HasIndex(e => e.IsDelete, "IX_IngredientAllergies_IsDelete");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientAllergies)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<IngredientMeasureUnit>(entity =>
        {
            entity.HasKey(e => new { e.IngredientId, e.MeasureUnitId });

            entity.HasIndex(e => e.MeasureUnitId, "IX_IngredientMeasureUnits_MeasureUnitId");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientMeasureUnits)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.MeasureUnit).WithMany(p => p.IngredientMeasureUnits)
                .HasForeignKey(d => d.MeasureUnitId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<MeasureUnit>(entity =>
        {
            entity.HasIndex(e => e.NameId, "IX_MeasureUnits_NameId");

            entity.HasOne(d => d.Name).WithMany(p => p.MeasureUnits)
                .HasForeignKey(d => d.NameId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.HasIndex(e => e.NameId, "IX_Nationalities_NameId");

            entity.HasOne(d => d.Name).WithMany(p => p.Nationalities)
                .HasForeignKey(d => d.NameId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<NutrientMeasureUnit>(entity =>
        {
            entity.HasIndex(e => e.MeasureUnitId, "IX_NutrientMeasureUnits_MeasureUnitId");

            entity.HasOne(d => d.MeasureUnit).WithMany(p => p.NutrientMeasureUnits)
                .HasForeignKey(d => d.MeasureUnitId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PersonalFood>(entity =>
        {
            entity.Property(e => e.Id1).HasColumnName("_id");
            entity.Property(e => e.Insertdate)
                .HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<PersonalFoodIngredient>(entity =>
        {
            entity.HasIndex(e => e.IngredientId, "IX_PersonalFoodIngredients_IngredientId");

            entity.HasIndex(e => e.MeasureUnitId, "IX_PersonalFoodIngredients_MeasureUnitId");

            entity.HasIndex(e => e.PersonalFoodId, "IX_PersonalFoodIngredients_PersonalFoodId");

            entity.Property(e => e.Id1).HasColumnName("_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.PersonalFoodIngredients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.MeasureUnit).WithMany(p => p.PersonalFoodIngredients)
                .HasForeignKey(d => d.MeasureUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.PersonalFood).WithMany(p => p.PersonalFoodIngredients)
                .HasForeignKey(d => d.PersonalFoodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasIndex(e => e.FoodId, "IX_Recipes_FoodId").IsUnique();

            entity.HasIndex(e => new { e.IsDelete, e.Status }, "IX_Recipes_IsDelete_Status");

            entity.Property(e => e.DateInsert).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Food).WithOne(p => p.RecipeNavigation)
                .HasForeignKey<Recipe>(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RecipeCategore>(entity =>
        {
            entity.HasIndex(e => new { e.IsActive, e.IsDelete }, "IX_RecipeCategores_IsActive_IsDelete");

            entity.HasIndex(e => e.NameId, "IX_RecipeCategores_NameId");

            entity.HasOne(d => d.Name).WithMany(p => p.RecipeCategores)
                .HasForeignKey(d => d.NameId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RecipeStep>(entity =>
        {
            entity.HasIndex(e => e.DescriptionId, "IX_RecipeSteps_DescriptionId");

            entity.HasIndex(e => e.IsDelete, "IX_RecipeSteps_IsDelete");

            entity.HasIndex(e => e.RecipeId, "IX_RecipeSteps_RecipeId");

            entity.Property(e => e.DateInsert).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Description).WithMany(p => p.RecipeSteps)
                .HasForeignKey(d => d.DescriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeSteps)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TempOldRecipePersian>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_old_recipe_persian");

            entity.Property(e => e.ArabicText).HasColumnName("arabic_text");
            entity.Property(e => e.EnglishText).HasColumnName("english_text");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IsDone)
                .HasDefaultValueSql("false")
                .HasColumnName("is_done");
            entity.Property(e => e.PersianText).HasColumnName("persian_text");
            entity.Property(e => e.TranslationId).HasColumnName("translation_id");
        });

        modelBuilder.Entity<Tip>(entity =>
        {
            entity.HasIndex(e => e.DescriptionId, "IX_Tips_DescriptionId");

            entity.HasIndex(e => e.IsDelete, "IX_Tips_IsDelete");

            entity.HasIndex(e => e.RecipeId, "IX_Tips_RecipeId");

            entity.Property(e => e.DateInsert).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Description).WithMany(p => p.Tips)
                .HasForeignKey(d => d.DescriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Recipe).WithMany(p => p.Tips)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserFoodAlergy>(entity =>
        {
            entity.HasIndex(e => e.IngredientId, "IX_UserFoodAlergies_IngredientId");

            entity.Property(e => e.Id1).HasColumnName("_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.UserFoodAlergies)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserFoodFavorite>(entity =>
        {
            entity.HasIndex(e => e.FoodId, "IX_UserFoodFavorites_FoodId");

            entity.Property(e => e.Id1).HasColumnName("_id");

            entity.HasOne(d => d.Food).WithMany(p => p.UserFoodFavorites)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserReportedFood>(entity =>
        {
            entity.Property(e => e.DateCreate)
                .HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<UserTrackDietPack>(entity =>
        {
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.EndDate)
                .HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<UserTrackDietPackDetail>(entity =>
        {
            entity.HasIndex(e => e.DietPackId, "IX_UserTrackDietPackDetails_DietPackId");

            entity.HasIndex(e => e.UserTrackDietPackId, "IX_UserTrackDietPackDetails_UserTrackDietPackId");

            entity.HasOne(d => d.DietPack).WithMany(p => p.UserTrackDietPackDetails)
                .HasForeignKey(d => d.DietPackId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.UserTrackDietPack).WithMany(p => p.UserTrackDietPackDetails)
                .HasForeignKey(d => d.UserTrackDietPackId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UserTrackDietPackDetails_UserTrackDietPacks_UserTrackDietPa~");
        });

        modelBuilder.Entity<UserTrackFood>(entity =>
        {
            entity.HasIndex(e => e.FoodId, "IX_UserTrackFoods_FoodId");

            entity.HasIndex(e => e.MeasureUnitId, "IX_UserTrackFoods_MeasureUnitId");

            entity.HasIndex(e => e.PersonalFoodId, "IX_UserTrackFoods_PersonalFoodId");

            entity.Property(e => e.Id1).HasColumnName("_id");
            entity.Property(e => e.InsertDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Food).WithMany(p => p.UserTrackFoods)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.MeasureUnit).WithMany(p => p.UserTrackFoods)
                .HasForeignKey(d => d.MeasureUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.PersonalFood).WithMany(p => p.UserTrackFoods)
                .HasForeignKey(d => d.PersonalFoodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserTrackNutrient>(entity =>
        {
            entity.Property(e => e.Id1).HasColumnName("_id");
            entity.Property(e => e.InsertDate).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<UserTrackWater>(entity =>
        {
            entity.HasIndex(e => new { e.InsertDate, e.UserId }, "IX_UserTrackWaters_InsertDate_UserId");

            entity.Property(e => e.Id1).HasColumnName("_id");
            entity.Property(e => e.InsertDate).HasColumnType("timestamp without time zone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
