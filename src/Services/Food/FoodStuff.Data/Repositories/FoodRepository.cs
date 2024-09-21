using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Filter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Repositories
{
    public class FoodRepository : Repository<Food>, IFoodRepository, IScopedDependency
    {
        public FoodRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public virtual async Task<int> AddAsync(Food food, CancellationToken cancellationToken, bool saveNow = true)
        {
            try
            {
                Assert.NotNull(food, nameof(food));
                await Entities.AddAsync(food, cancellationToken).ConfigureAwait(false);
                if (saveNow)
                    await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return food.Id;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IQueryable<Food> FindByCondition(Expression<Func<Food, bool>> expression, CancellationToken cancellationToken)
        {
            return TableNoTracking.Where(expression);
        }

        public IQueryable<Food> FindByConditionTracking(Expression<Func<Food, bool>> expression, CancellationToken cancellationToken)
        {
            return Table.Include(a => a.TranslationName).Include(a => a.TranslationRecipe).Where(expression);
        }

        public IQueryable<Food> FindByParameters(FoodInputParameters Parameter, string LanguageName, CancellationToken cancellationToken)
        {
            IQueryable<Food> _food;
            _food = Table.Include(a => a.TranslationName);
            _food = FoodQueryParameters.GetQueryable(_food, LanguageName, Parameter);
            return _food;
        }

        public virtual async Task<Food> GetByIdAsync(CancellationToken cancellationToken, int id)
        {

            var food = await Table
                 .Include(ft => ft.TranslationName)
                 .Include(fr => fr.TranslationRecipe)
                 .Include(fi => fi.FoodIngredients)
                 .ThenInclude(i => i.Ingredient)
                 .ThenInclude(t => t.Translation)
                 .Include(f => f.FoodIngredients)
                 .ThenInclude(fi => fi.Ingredient)
                 .ThenInclude(im => im.IngredientMeasureUnits)
                 .ThenInclude(m => m.MeasureUnit)
                 .ThenInclude(mt => mt.Translation)
                 .Include(f => f.FoodIngredients)
                 .ThenInclude(m => m.MeasureUnit)
                 .ThenInclude(t => t.Translation)
                 .Include(fm => fm.FoodMeasureUnits)
                 .ThenInclude(m => m.MeasureUnit)
                 .ThenInclude(mt => mt.Translation)
                 .Include(fb => fb.Brand)
                 .ThenInclude(b => b.Translation)
                 .Include(fn => fn.FoodNationalities)
                 .ThenInclude(n => n.Nationality)
                 .ThenInclude(t => t.NameTranslation)
                 .Include(fc => fc.FoodCategories)
                 .ThenInclude(c => c.Category)
                 .ThenInclude(tca => tca.NameTranslation)
                 .Include(d => d.FoodDietCategories)
                 .ThenInclude(dc => dc.DietCategory)
                 .ThenInclude(trdc => trdc.NameTranslation)
                 .Include(fsd => fsd.FoodSpecialDiseases)
                 .Include(fh => fh.FoodHabits)
                 .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
            return food;
        }

        public virtual async Task<Food> GetByFoodCodeAsync(CancellationToken cancellationToken, long foodCode)
        {
            var food = await Table
                //.Include(ft => ft.TranslationName)
                .Include(fr => fr.TranslationRecipe)
                .Include(fi => fi.FoodIngredients)
                .ThenInclude(i => i.Ingredient)/*.ThenInclude(t => t.Translation)*/
                .Include(f => f.FoodIngredients)
                .ThenInclude(fi => fi.Ingredient)
                .ThenInclude(im => im.IngredientMeasureUnits)
                .Include(fm => fm.FoodMeasureUnits)
                .ThenInclude(m => m.MeasureUnit)/*.ThenInclude(mt => mt.Translation)*/
                .Include(fb => fb.Brand).ThenInclude(b => b.Translation)
                .Where(f => f.FoodCode == foodCode).FirstOrDefaultAsync(cancellationToken);
            return food;
        }

    }
}
