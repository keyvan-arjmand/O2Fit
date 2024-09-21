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
    public class GetDietPackByIdQueryHandler : IRequestHandler<GetDietPackByIdQuery, GetDietPackViewModel>, ITransientDependency
    {
        private readonly IRepository<DietPack> _repository;
        public GetDietPackByIdQueryHandler(IRepository<DietPack> repository)
        {
            _repository = repository;
        }

        public async Task<GetDietPackViewModel> Handle(GetDietPackByIdQuery request, CancellationToken cancellationToken)
        {

            var dietPack = await _repository.TableNoTracking
                .Include(d => d.DietPackAlerges)
                .ThenInclude(da => da.Ingredient)
                .ThenInclude(it => it.Translation)
                .Include(d => d.DietPackFoods)
                .ThenInclude(f => f.Food)
                .ThenInclude(t => t.TranslationName)
                .Include(dpf => dpf.DietPackFoods)
                .ThenInclude(m => m.MeasureUnit)
                .ThenInclude(m_tr => m_tr.Translation)
                .Include(dpdc => dpdc.DietPackDietCategories)
                .Include(dpn => dpn.DietPackNationalities)
                .Where(m => m.Id == request.Id)
                .Select(s => new GetDietPackViewModel
                {
                    Id = s.Id,
                    CalorieValue = s.CaloriValue,
                    CategoryId = s.CategoryId,
                    DailyCalorie = s.DailyCalorie,
                    FoodMeal = s.FoodMeal,
                    IsActive = s.IsActive,
                    NutrientValue = StringConvertor.ToNumber(s.NutrientValue),
                    DietCategoryIds = s.DietPackDietCategories.Select(dc => dc.DietCategoryId).ToList(),
                    NationalityIds = s.DietPackNationalities.Select(n => n.NationalityId).ToList(),
                    DietPackFoods = s.DietPackFoods.Where(f => f.DietPackId == request.Id)
                    .Select(sf => new DietPackFoodViewModel
                    {
                        Calorie = sf.Calorie,
                        CategoryChildId = sf.CategoryChildId,
                        FoodId = sf.FoodId,
                        MeasureUnitId = sf.MeasureUnitId,
                        MeasureUnitName = sf.MeasureUnit.Translation.Persian,
                        FoodName = sf.Food.TranslationName.Persian,
                        MeasureUnitValue = sf.Value,
                        NutrientValue = StringConvertor.ToNumber(sf.NutrientValue)

                    }).ToList()


                })
                .FirstOrDefaultAsync(cancellationToken);







            return dietPack;
        }
    }
}

