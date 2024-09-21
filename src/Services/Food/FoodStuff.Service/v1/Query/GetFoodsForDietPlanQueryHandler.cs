using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace FoodStuff.Service.v1.Query
{
    public class GetFoodsForDietPlanQueryHandler :
        IRequestHandler<GetFoodsForDietPlanQuery, PageResult<GetFoodsForDietPlanViewModel>>,
        IScopedDependency
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IRepository<Nationality> _nationalityRepository;
        private readonly IRedisCacheClient _redisCacheClient;

        public GetFoodsForDietPlanQueryHandler(
            IFoodRepository foodRepository,
            IRedisCacheClient redisCacheClient,
            IRepository<Nationality> nationalityRepository)
        {
            _foodRepository = foodRepository;
            _redisCacheClient = redisCacheClient;
            _nationalityRepository = nationalityRepository;
        }

        public async Task<PageResult<GetFoodsForDietPlanViewModel>> Handle(
            GetFoodsForDietPlanQuery request, CancellationToken cancellationToken)
        {
            if (request.CategoryId == 0 || string.IsNullOrEmpty(request.CategoryId.ToString())
                || request.NationalityIds.Count == 0
                || request.DietCategoryIds.Count == 0)
                throw new AppException(ApiResultStatusCode.BadRequest, "BadRequest");


            PageResult<GetFoodsForDietPlanViewModel> pageResult = new PageResult<GetFoodsForDietPlanViewModel>();
            List<GetFoodsForDietPlanViewModel> foods = new List<GetFoodsForDietPlanViewModel>();
            var foodList = new List<Food>();

            //foreach (var DietCategoryId in request.DietCategoryIds)
            //{
            foreach (var nationalityId in request.NationalityIds)
            {
                var parentFoods = await _foodRepository.TableNoTracking
                             .Include(n => n.TranslationName)
                             .Include(c => c.FoodDietCategories)
                             .Include(n => n.FoodNationalities)
                             .Include(c => c.FoodCategories)
                             .Include(m => m.FoodMeasureUnits)
                             .Where(f => f.IsActive &&
                                         f.UseInDiet &&
                                         f.FoodCategories.Any(x => x.CategoryId == request.CategoryId) &&
                                         // f.FoodDietCategories.Any(fdc => fdc.DietCategoryId == DietCategoryId)
                                         //&&
                                         f.FoodNationalities.Any(
                                             fn => fn.NationalityId == nationalityId))
                             //.Select(s => new GetFoodsForDietPlanViewModel
                             //{
                             //    FoodId = s.Id,
                             //    FoodName = s.TranslationName.Persian,
                             //    NutrientValue = s.NutrientValue,
                             //    MeasureUnitIds = s.FoodMeasureUnits.Select(m => m.MeasureUnitId).ToList()
                             //})
                             .ToListAsync(cancellationToken).ConfigureAwait(false);

                foodList.AddRange(parentFoods.Where(x => foodList.All(y => y.Id != x.Id)));

                Nationality nationality = await _nationalityRepository.Entities.FindAsync(nationalityId);

                if (nationality.ParentId == null)
                {
                    var childIds = await _nationalityRepository.TableNoTracking
                   .Where(n => n.ParentId == nationalityId).Select(s => s.Id)
                   .ToListAsync(cancellationToken);

                    if (childIds.Any())
                    {
                        List<GetFoodsForDietPlanViewModel> childFoods = new List<GetFoodsForDietPlanViewModel>();
                        var childFoodList = new List<Food>();
                        foreach (var childId in childIds)
                        {
                            childFoodList = await _foodRepository.TableNoTracking
                                .Include(n => n.TranslationName)
                                .Include(c => c.FoodDietCategories)
                                .Include(n => n.FoodNationalities)
                                .Include(c => c.FoodCategories)
                                .Include(m => m.FoodMeasureUnits)
                                .Where(f => f.IsActive &&
                                            f.UseInDiet &&
                                            f.FoodCategories.Any(x => x.CategoryId == request.CategoryId) &&
                                            //f.FoodDietCategories.Any(fdc => fdc.DietCategoryId == DietCategoryId)
                                            //&&
                                            f.FoodNationalities.Any(
                                                fn => fn.NationalityId == childId))
                                //.Select(s => new GetFoodsForDietPlanViewModel
                                //{
                                //    FoodId = s.Id,
                                //    FoodName = s.TranslationName.Persian,
                                //    NutrientValue = s.NutrientValue,
                                //    MeasureUnitIds = s.FoodMeasureUnits.Select(m => m.MeasureUnitId).ToList()
                                //})
                                .ToListAsync(cancellationToken).ConfigureAwait(false);

                            // foods.AddRange(childFoods);
                            foodList.AddRange(childFoodList.Where(x => foodList.All(y => y.Id != x.Id)));


                        }

                    }
                }
            }

            // var  foodListIncludeCompleteFilter = foodList.Where(x =>
            //       x.FoodDietCategories.Select(s=>s.DietCategoryId).OrderBy(o=>o).SequenceEqual(request.DietCategoryIds.OrderBy(o=>o))).ToList();

            // var foodListIncludeContainFilter = foodList.Where(x =>
            //     x.FoodDietCategories.Any(a => request.DietCategoryIds.Contains(a.DietCategoryId))).ToList();

            var finalFoodIds = new List<int>();
            var firstLoopFoodIds = new List<int>();
            int index = 0;
            foreach (var dietCategoryId in request.DietCategoryIds)
            {
                var tempFoodIds = foodList.Where(x => x.FoodDietCategories.Any(q => q.DietCategoryId == dietCategoryId))
                    .Select(s => s.Id).ToList();
                if (index == 0)
                {
                    firstLoopFoodIds.AddRange(tempFoodIds);
                    index++;
                    continue;
                }
                else if(index == 1)
                {
                    finalFoodIds.AddRange(firstLoopFoodIds.Intersect(tempFoodIds));
                }
                else
                {
                    var temp = finalFoodIds.Intersect(tempFoodIds).ToList();
                    finalFoodIds.AddRange(temp);
                }
                index++;

            }

            if(finalFoodIds.Count == 0 && firstLoopFoodIds.Count  > 0)
                finalFoodIds.AddRange(firstLoopFoodIds);


            pageResult = new PageResult<GetFoodsForDietPlanViewModel>
            {
                Items = foodList.Where(x=> finalFoodIds.Contains(x.Id)).Select(s => new GetFoodsForDietPlanViewModel
                {
                    FoodId = s.Id,
                    FoodName = s.TranslationName.Persian,
                    NutrientValue = s.NutrientValue,
                    MeasureUnitIds = s.FoodMeasureUnits.Select(m => m.MeasureUnitId).ToList()
                }).ToList(),
                Count = foodList.Where(x => finalFoodIds.Contains(x.Id)).ToList().Count
            };

            return pageResult;
        }
    }
}
