using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using AutoMapper;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Query.Recipe
{
    public class GetFoodByFilterQueryHandler : IRequestHandler<GetFoodByFilterQuery, List<GetFullRecipeById>>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Food> _repository;
        private readonly IMapper _mapper;

        public GetFoodByFilterQueryHandler(IRepository<Domain.Entities.Food.Food> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetFullRecipeById>> Handle(GetFoodByFilterQuery request, CancellationToken cancellationToken)
        {
            var foodResult = await FilterFood(request, cancellationToken);
            if (foodResult.Count > 0)
            {
                return _mapper.Map<List<Domain.Entities.Food.Food>, List<GetFullRecipeById>>(foodResult);
            }
            throw new AppException(ApiResultStatusCode.BadRequest);
        }

        private async Task<List<Domain.Entities.Food.Food>> FilterFood(GetFoodByFilterQuery request, CancellationToken cancellationToken)
        {
            var food = Enumerable.Empty<Domain.Entities.Food.Food>().AsQueryable();

            switch (request.FoodCode > 0, request.Id > 0, !string.IsNullOrWhiteSpace(request.PersianName))
            {
                case (true, true, true):
                    food = _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                        .Include(x => x.Recipe).ThenInclude(x => x.RecipeSteps).ThenInclude(x => x.Translation)
                        .Include(x => x.Recipe).ThenInclude(x => x.Tips).ThenInclude(x => x.Translation)
                        .Where(x => x.Id == request.Id
                                    && x.FoodCode == request.FoodCode
                                    && x.TranslationName.Persian.Contains(request.PersianName) && x.Recipe != null);
                    break;
                case (false, false, false):
                    food = _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                        .Include(x => x.Recipe).ThenInclude(x => x.RecipeSteps).ThenInclude(x => x.Translation)
                        .Include(x => x.Recipe).ThenInclude(x => x.Tips).ThenInclude(x => x.Translation)
                        .Where(x => x.Recipe != null);
                    break;
                case (true, false, false):
                    food = _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                        .Include(x => x.Recipe).ThenInclude(x => x.RecipeSteps).ThenInclude(x => x.Translation)
                        .Include(x => x.Recipe).ThenInclude(x => x.Tips).ThenInclude(x => x.Translation)
                        .Where(x => x.FoodCode == request.FoodCode && x.Recipe != null);
                    break;

                case (true, true, false):
                    food = _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                        .Include(x => x.Recipe).ThenInclude(x => x.RecipeSteps).ThenInclude(x => x.Translation)
                        .Include(x => x.Recipe).ThenInclude(x => x.Tips).ThenInclude(x => x.Translation)
                        .Where(x => x.Id == request.Id
                                    && x.FoodCode == request.FoodCode && x.Recipe != null);
                    break;

                case (false, true, true):
                    food = _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                        .Include(x => x.Recipe).ThenInclude(x => x.RecipeSteps).ThenInclude(x => x.Translation)
                        .Include(x => x.Recipe).ThenInclude(x => x.Tips).ThenInclude(x => x.Translation)
                        .Where(x => x.Id == request.Id
                                    && x.TranslationName.Persian.Contains(request.PersianName) && x.Recipe != null);
                    break;

                case (false, true, false):
                    food = _repository.TableNoTracking
                            .Include(x => x.TranslationName)
                            .Include(x => x.Recipe).ThenInclude(x => x.RecipeSteps).ThenInclude(x => x.Translation)
                            .Include(x => x.Recipe).ThenInclude(x => x.Tips).ThenInclude(x => x.Translation)
                            .Where(x => x.Id == request.Id && x.Recipe != null);
                    break;

                case (false, false, true):
                    food = _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                        .Include(x => x.Recipe).ThenInclude(x => x.RecipeSteps).ThenInclude(x => x.Translation)
                        .Include(x => x.Recipe).ThenInclude(x => x.Tips).ThenInclude(x => x.Translation)
                        .Where(x => x.TranslationName.Persian.Contains(request.PersianName) && x.Recipe != null);

                    break;

                default:
                    break;
            }

            if (request.RecipeStatus != null)
            {
                return await food.Where(x => x.Recipe.Status == request.RecipeStatus)
                    .OrderByDescending(x => x.Recipe.Id)
                    .Skip((request.Page - 1 ?? 0) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);
            }
            else
            {
                return await food.OrderByDescending(x => x.Recipe.Id)
                    .Skip((request.Page - 1 ?? 0) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);
            }


        }
    }
}