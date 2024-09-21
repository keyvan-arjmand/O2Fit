using Common.Utilities;
using Domain;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.MeasureUnit;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FoodStuff.Domain.Entities.Diet;
using Microsoft.Extensions.Hosting;

namespace Data.Database
{
    public class ApplicationDbContext : DbContext
    //DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MyApiDb;Integrated Security=true");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;

            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddPluralizingTableNameConvention();

            //modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<FoodMeasureUnit>()
                .HasKey(bc => new { bc.FoodId, bc.MeasureUnitId });

            modelBuilder.Entity<FoodMeasureUnit>()
                .HasOne(bc => bc.Food)
                .WithMany(b => b.FoodMeasureUnits)
                .HasForeignKey(bc => bc.FoodId);

            modelBuilder.Entity<FoodMeasureUnit>()
                .HasOne(bc => bc.MeasureUnit)
                .WithMany(b => b.FoodMeasureUnits)
                .HasForeignKey(bc => bc.MeasureUnitId);

            //////////////////////


            modelBuilder.Entity<FoodNationality>()
                .HasKey(bc => new { bc.FoodId, bc.NationalityId });

            modelBuilder.Entity<FoodNationality>()
                .HasOne(bc => bc.Food)
                .WithMany(b => b.FoodNationalities)
                .HasForeignKey(bc => bc.FoodId);

            modelBuilder.Entity<FoodNationality>()
                .HasOne(bc => bc.Nationality)
                .WithMany(b => b.FoodNationalities)
                .HasForeignKey(bc => bc.NationalityId);

            /////////////////////

            modelBuilder.Entity<FoodDietCategory>()
                .HasKey(bc => new { bc.FoodId, bc.DietCategoryId });

            modelBuilder.Entity<FoodDietCategory>()
                .HasOne(bc => bc.Food)
                .WithMany(b => b.FoodDietCategories)
                .HasForeignKey(bc => bc.FoodId);

            modelBuilder.Entity<FoodDietCategory>()
                .HasOne(bc => bc.DietCategory)
                .WithMany(b => b.FoodDietCategories)
                .HasForeignKey(bc => bc.DietCategoryId);

            //////////////////////



            modelBuilder.Entity<FoodCategory>()
                .HasKey(bc => new { bc.FoodId, bc.CategoryId });


            modelBuilder.Entity<FoodCategory>()
                .HasOne(bc => bc.Food)
                .WithMany(b => b.FoodCategories)
                .HasForeignKey(bc => bc.FoodId);

            modelBuilder.Entity<FoodCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(b => b.FoodCategories)
                .HasForeignKey(bc => bc.CategoryId);


            modelBuilder.Entity<IngredientMeasureUnit>()
                .HasKey(bc => new { bc.IngredientId, bc.MeasureUnitId });

            modelBuilder.Entity<IngredientMeasureUnit>()
                .HasOne(bc => bc.Ingredient)
                .WithMany(b => b.IngredientMeasureUnits)
                .HasForeignKey(bc => bc.IngredientId);

            modelBuilder.Entity<IngredientMeasureUnit>()
                .HasOne(bc => bc.MeasureUnit)
                .WithMany(b => b.IngredientMeasureUnits)
                .HasForeignKey(bc => bc.MeasureUnitId);

            modelBuilder.Entity<MeasureUnit>()
                .HasMany(b => b.NutrientMeasureUnits)
                .WithOne(b => b.MeasureUnit);

            modelBuilder.Entity<DietPack>()
                .HasIndex(d => d.IsActive);

            modelBuilder.Entity<RecipeCategore>()
                .HasIndex(i => new { i.IsActive, i.IsDelete });

            modelBuilder.Entity<RecipeCategore>().HasQueryFilter(p => !p.IsDelete);

            modelBuilder.Entity<Recipe>().HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Recipe>()
                .HasIndex(d => new {d.IsDelete , d.Status});

            modelBuilder.Entity<Tip>().HasQueryFilter(p => !p.IsDelete);

            modelBuilder.Entity<Tip>()
                .HasIndex(d => new { d.IsDelete});

            modelBuilder.Entity<RecipeStep>().HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<RecipeStep>()
                .HasIndex(d => new { d.IsDelete});

            modelBuilder.Entity<Food>().HasQueryFilter(f => !f.IsDelete);
            
            modelBuilder.Entity<Food>().HasIndex(i => new { i.IsActive, i.IsDelete, i.Tag, i.FoodType });

            modelBuilder.Entity<UserTrackWater>().HasIndex(i => new { i.InsertDate, i.UserId });

            modelBuilder.Entity<IngredientAllergy>().HasQueryFilter(f => !f.IsDelete);

            modelBuilder.Entity<IngredientAllergy>().HasIndex(i => i.IsDelete);

            // modelBuilder.Entity<UserTrackFood>().HasIndex(i => new { i.InsertDate, i.UserId });

            //modelBuilder.Entity<Food>()
            //    .HasMany(a => a.UserTrackFoods)
            //    .WithOne(b => b.Food);

            //modelBuilder.Entity<PersonalFood>()
            //    .HasMany(a => a.UserTrackFoods)
            //    .WithOne(b => b.PersonalFood);
        }

        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }
    }
}
