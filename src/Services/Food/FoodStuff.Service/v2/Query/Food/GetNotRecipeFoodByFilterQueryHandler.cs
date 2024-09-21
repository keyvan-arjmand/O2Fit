using AutoMapper;
using FoodStuff.Service.Models;
using FoodStuff.Service.v2.Query.Recipe;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Query.Food
{
    public class
        GetNotRecipeFoodByFilterQueryHandler :
            IRequestHandler<GetNotRecipeFoodByFilterQuery, List<GetFoodNotRecipeDto>>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Food> _repository;
        private readonly IMapper _mapper;

        public GetNotRecipeFoodByFilterQueryHandler(IRepository<Domain.Entities.Food.Food> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetFoodNotRecipeDto>> Handle(GetNotRecipeFoodByFilterQuery request,
            CancellationToken cancellationToken)
        {
            var foodResult = await FilterFood(request, cancellationToken);
            if (foodResult.Count > 0)
            {
                return foodResult;
            }
            throw new AppException(ApiResultStatusCode.BadRequest);
        }

        private async Task<List<GetFoodNotRecipeDto>> FilterFood(GetNotRecipeFoodByFilterQuery request, CancellationToken cancellationToken)
        {
            var food = new List<Domain.Entities.Food.Food>();

            switch (request.FoodCode > 0, request.Id > 0, !string.IsNullOrWhiteSpace(request.PersianName))
            {
                case (true, true, true):
                    food = await _repository.TableNoTracking
                      .Include(x => x.TranslationName)
                      .Where(x => x.Id == request.Id
                       && x.FoodCode == request.FoodCode
                       && x.TranslationName.Persian.Contains(request.PersianName) && x.Recipe == null)
                    .OrderByDescending(x => x.Id)
                    .Skip((request.Page - 1 ?? 0) * request.PageSize)
                    .Take(request.PageSize)
                       .ToListAsync(cancellationToken);
                    return _mapper.Map<List<Domain.Entities.Food.Food>, List<GetFoodNotRecipeDto>>(food);
                    break;
                case (false, false, false):
                    food = await _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                        .Where(x => x.Recipe == null)
                        .OrderByDescending(x => x.Id)
                    .Skip((request.Page - 1 ?? 0) * request.PageSize)
                    .Take(request.PageSize)
                        .ToListAsync(cancellationToken);
                    return _mapper.Map<List<Domain.Entities.Food.Food>, List<GetFoodNotRecipeDto>>(food);
                    break;
                case (true, false, false):
                    food = await _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                         .Where(x => x.FoodCode == request.FoodCode && x.Recipe == null)
                        .OrderByDescending(x => x.Id)
                    .Skip((request.Page - 1 ?? 0) * request.PageSize)
                    .Take(request.PageSize)
                        .ToListAsync(cancellationToken);
                    return _mapper.Map<List<Domain.Entities.Food.Food>, List<GetFoodNotRecipeDto>>(food);
                    break;
                case (true, true, false):
                    food = await _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                         .Where(x => x.Id == request.Id
                                    && x.FoodCode == request.FoodCode && x.Recipe == null)
                        .OrderByDescending(x => x.Id)
                    .Skip((request.Page - 1 ?? 0) * request.PageSize)
                    .Take(request.PageSize)
                        .ToListAsync(cancellationToken);
                    return _mapper.Map<List<Domain.Entities.Food.Food>, List<GetFoodNotRecipeDto>>(food);
                    break;
                case (false, true, true):
                    food = await _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                        .Where(x => x.Id == request.Id
                    && x.TranslationName.Persian.Contains(request.PersianName) && x.Recipe == null)
                        .OrderByDescending(x => x.Id)
                        .Skip((request.Page - 1 ?? 0) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync(cancellationToken);
                    return _mapper.Map<List<Domain.Entities.Food.Food>, List<GetFoodNotRecipeDto>>(food);
                    break;
                case (false, true, false):
                    food = await _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                        .Where(x => x.Id == request.Id && x.Recipe == null)
                        .OrderByDescending(x => x.Id)
                        .Skip((request.Page - 1 ?? 0) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync(cancellationToken);
                    return _mapper.Map<List<Domain.Entities.Food.Food>, List<GetFoodNotRecipeDto>>(food);
                    break;
                case (false, false, true):
                    food = await _repository.TableNoTracking
                        .Include(x => x.TranslationName)
                        .Where(x => x.TranslationName.Persian.Contains(request.PersianName) && x.Recipe == null)
                        .OrderByDescending(x => x.Id)
                        .ToListAsync(cancellationToken);
                    return _mapper.Map<List<Domain.Entities.Food.Food>, List<GetFoodNotRecipeDto>>(food);
                    break;
                default:
                    return new List<GetFoodNotRecipeDto>();
                    break;
            }


        }

    }
}