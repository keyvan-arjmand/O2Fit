using Common;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetFoodByIdQueryHandler : IRequestHandler<GetFoodByIdQuery, GetFoodByIdViewModel>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Food> _foodRepository;

        public GetFoodByIdQueryHandler(IRepository<Domain.Entities.Food.Food> foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public Task<GetFoodByIdViewModel> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
        {
            return _foodRepository.TableNoTracking
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
                   .Include(fh => fh.FoodHabits)
                   .Where(f => f.Id == request.Id)
                   .Select(s => new GetFoodByIdViewModel
                   {
                       BakingTime = s.BakingTime,
                       BakingType = s.BakingType,
                       BarcodeNational = s.BarcodeNational,
                       BarcodeGs1 = s.BarcodeGs1,
                       DefaultMeasureUnitId = s.DefaultMeasureUnitId,
                       FoodId = s.Id,
                       FoodMeals = s.FoodMeals,
                       FoodType = s.FoodType,
                       GI = s.GI,
                       _id = s.Id.ToString(),
                       PersonCount = s.PersonCount,
                       Brand = s.Brand == null ? null : new TranslationViewModel
                       {
                           Arabic = s.Brand.Translation.Arabic,
                           English = s.Brand.Translation.English,
                           Persian = s.Brand.Translation.Persian,
                       },
                       ImageThumb = s.ImageThumb,
                       ImageUri = s.ImageUri,
                       Name = new TranslationViewModel
                       {
                           Persian = s.TranslationName.Persian,
                           English = s.TranslationName.English,
                           Arabic = s.TranslationName.Arabic,
                       },
                       FoodHabitIds = s.FoodHabits.Where(fh => fh.FoodId == s.Id).Select(h => h.FoodHabit).ToList(),
                       Recipe = s.TranslationRecipe == null ? null : new TranslationViewModel
                       {
                           Persian = s.TranslationRecipe.Persian,
                           English = s.TranslationRecipe.English,
                           Arabic = s.TranslationRecipe.Arabic,
                       },
                       NutrientValue = StringConvertor.ToNumber(s.NutrientValue),
                       MeasureUnits = s.FoodMeasureUnits.Select(fmu => new MeasureUnitViewModel
                       {
                           Id = fmu.MeasureUnit.Id,
                           MeasureUnitName = new TranslationViewModel
                           {
                               Arabic = fmu.MeasureUnit.Translation.Arabic,
                               English = fmu.MeasureUnit.Translation.English,
                               Persian = fmu.MeasureUnit.Translation.Persian
                           },
                           Value = fmu.MeasureUnit.Value
                       }).ToList(),
                       Ingredients = s.FoodIngredients.Select(fi => new IngredientViewModel
                       {
                           Id = fi.Ingredient.Id,
                           Name = new TranslationViewModel
                           {
                               Arabic = fi.Ingredient.Translation.Arabic,
                               English = fi.Ingredient.Translation.English,
                               Persian = fi.Ingredient.Translation.Persian
                           },
                           Value = fi.IngredientValue,
                           MeasureUnitId = fi.MeasureUnit.Id,
                           MeasureUnitList = fi.Ingredient.IngredientMeasureUnits
                           .Select(imu => new MeasureUnitViewModel
                           {
                               Id = imu.MeasureUnit.Id,
                               MeasureUnitName = new TranslationViewModel
                               {
                                   Arabic = imu.MeasureUnit.Translation.Arabic,
                                   English = imu.MeasureUnit.Translation.English,
                                   Persian = imu.MeasureUnit.Translation.Persian
                               },
                               Value = imu.MeasureUnit.Value,
                           }).ToList(),

                       }).ToList(),


                   }).FirstOrDefaultAsync();
        }
    }
}
