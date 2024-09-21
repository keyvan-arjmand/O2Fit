using Common;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query
{
    public class GetFoodByCodeQueryHandler : IRequestHandler<GetFoodByCodeQuery, PageResult<FoodResultFoodCode>>, IScopedDependency
    {
        private readonly IRepository<Food> _foodRepository;

        public GetFoodByCodeQueryHandler(IRepository<Food> foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<PageResult<FoodResultFoodCode>> Handle(GetFoodByCodeQuery request, CancellationToken cancellationToken)
        {
            var food = await _foodRepository.TableNoTracking
                .Include(c => c.Brand)
                .ThenInclude(b => b.Translation)
                .Include(d => d.TranslationName)
                .Where(f => f.FoodCode == request.FoodCode)
                .Select(s => new FoodResultFoodCode
                {
                    BrandName = s.BrandId != null ? new TranslationName
                    {
                        Arabic = s.Brand.Translation.Arabic,
                        English = s.Brand.Translation.English,
                        Persian = s.Brand.Translation.Persian
                    } : null,
                    FoodCode = s.FoodCode,
                    FoodId = s.Id,
                    FoodType = (int)s.FoodType,
                    Name = s.TranslationName.Persian,
                    NutrientValue = s.NutrientValue,
                    IsActive = s.IsActive
                })
                .ToListAsync(cancellationToken);

            return new PageResult<FoodResultFoodCode>
            {
                Count = food.Count,
                Items = food,
            };
        }
    }
}
