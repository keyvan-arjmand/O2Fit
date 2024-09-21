using Common;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query.DietPackQuery
{
    public class GetAllFullDietPackQueryHandler : IRequestHandler<GetAllFullDietPackQuery, PageResult<GetAllFullDietPackViewModel>>, IScopedDependency
    {
        private readonly IRepository<DietPack> _dietPackRepository;

        public GetAllFullDietPackQueryHandler(IRepository<DietPack> dietPackRepository)
        {
            _dietPackRepository = dietPackRepository;
        }

        public async Task<PageResult<GetAllFullDietPackViewModel>> Handle(GetAllFullDietPackQuery request, CancellationToken cancellationToken)
        {
            var result = await _dietPackRepository.TableNoTracking
                 .Include(f => f.DietPackFoods)
                 .ThenInclude(f => f.Food)
                 .ThenInclude(t => t.TranslationName)
                 .Include(m => m.DietPackFoods)
                 .ThenInclude(m => m.MeasureUnit)
                 .ThenInclude(mtr => mtr.Translation)
                 .OrderByDescending(b => b.Id)
                                    .Take(request.PageSize).Skip((request.Page - 1 ?? 0) * request.PageSize)
                                    .Select(s => new GetAllFullDietPackViewModel
                                    {
                                        CaloriValue = s.CaloriValue,
                                        DailyCalorie = s.DailyCalorie,
                                        FoodMeal = s.FoodMeal,
                                        Name = s.Name.Persian,
                                        NutrientValue = s.NutrientValue,
                                        DietCategoryIds = s.DietPackDietCategories.Select(c => c.DietCategoryId).ToList(),
                                        IngredientAllergyIds = s.DietPackAlerges.Select(a => a.IngredientId).ToList(),
                                        NationalityIds = s.DietPackNationalities.Select(n => n.NationalityId).ToList(),
                                        SpecialDiseases = s.DietPackSpecialDiseases.Select(s => s.SpecialDisease).ToList(),
                                        DietPackFoods = s.DietPackFoods.Where(dpf => dpf.DietPackId == s.Id)
                                        .Select(dp => new GetAllFullDietPackFood
                                        {
                                            Calorie = dp.Calorie,
                                            FoodId = dp.FoodId,
                                            FoodName = dp.Food.TranslationName.Persian,
                                            MeasureUnitId = dp.MeasureUnitId,
                                            Value = dp.Value,
                                            MeasureUnitName = dp.MeasureUnit.Translation.Persian
                                        }).ToList()
                                    })
                                    .ToListAsync(cancellationToken);

            return new PageResult<GetAllFullDietPackViewModel>
            {
                Count = result.Count,
                Items = result,
                PageSize = request.PageSize
            };
        }
    }
}
