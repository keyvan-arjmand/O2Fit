using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Service.Models;
using FoodStuff.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Driver.Linq;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetFoodWithDetailsByIdQueryHandler: IRequestHandler<GetFoodWithDetailsByIdQuery, FoodWithDetailsDto>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Food> _repository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCacheClient;
        public GetFoodWithDetailsByIdQueryHandler(IRepository<Domain.Entities.Food.Food> repository, IMapper mapper, IRedisCacheClient redisCacheClient)
        {
            _repository = repository;
            _mapper = mapper;
            _redisCacheClient = redisCacheClient;
        }

        public async Task<FoodWithDetailsDto> Handle(GetFoodWithDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            if (await _redisCacheClient.Db1.ExistsAsync($"FoodV2:{request.Id}"))
            {
                return await _redisCacheClient.Db1.GetAsync<FoodWithDetailsDto>($"FoodV2:{request.Id}");
            }

            var food = await _repository.TableNoTracking.Include(x => x.FoodIngredients).ThenInclude(x => x.Ingredient)
                .ThenInclude(x => x.Translation).Include(x => x.FoodIngredients).ThenInclude(x=>x.MeasureUnit)
                .Include(x=>x.FoodDietCategories).ThenInclude(x=>x.DietCategory).ThenInclude(x=>x.NameTranslation)
                .Include(x => x.FoodDietCategories).ThenInclude(x => x.DietCategory).ThenInclude(x=>x.DescriptionTranslation)
                .Include(x=>x.FoodCategories).ThenInclude(x=>x.Category).ThenInclude(x=>x.NameTranslation)
                .Include(x => x.TranslationName).Include(x=>x.FoodNationalities).ThenInclude(x=>x.Nationality).ThenInclude(x=>x.NameTranslation)
                .Include(x => x.TranslationRecipe).Include(x => x.Brand).ThenInclude(x => x.Translation)
                //.Include(x => x.RecipeCategory)
                .Include(x => x.Recipe).ThenInclude(x => x.RecipeSteps).ThenInclude(x=>x.Translation)
                .Include(x => x.Recipe).ThenInclude(x => x.Tips).ThenInclude(x=>x.Translation).Include(x=>x.FoodMeasureUnits)
                .Include(x=>x.FoodIngredients).ThenInclude(x=>x.Ingredient).ThenInclude(x=>x.IngredientMeasureUnits)
                .Include(x=>x.FoodHabits).FirstOrDefaultAsync(x=>x.Id == request.Id ,cancellationToken);

            if (food == null)
            {
                return null;
            }

            var result = _mapper.Map<Domain.Entities.Food.Food, FoodWithDetailsDto>(food);


            foreach (var item in food.FoodIngredients)
            {
                var ing = new IngredientDto
                {
                    Id = item.IngredientId,
                    Name = new TranslationResultDto
                    {
                        Id = item.Ingredient.Translation.Id,
                        Persian = item.Ingredient.Translation.Persian,
                        Arabic = item.Ingredient.Translation.Arabic,
                        English = item.Ingredient.Translation.English
                    },
                    MeasureUnitId = item.MeasureUnitId,
                    Value = item.IngredientValue,
                    MeasureUnitList = item.Ingredient.IngredientMeasureUnits.Select(m => m.MeasureUnitId).Distinct().ToList(),
                };
                if (result.Ingredients == null)
                {
                    result.Ingredients = new List<IngredientDto>();
                }
                result.Ingredients.Add(ing);
            }

            List<int> measureUnits = new List<int> { 36, 37, 58 };
            if (food.FoodMeasureUnits.Any())
            {
                List<int> extraMeasureUnits = food.FoodMeasureUnits
                    .Select(m => m.MeasureUnitId).Distinct().ToList();
                measureUnits.AddRange(extraMeasureUnits);
            }
            result.MeasureUnits = measureUnits;
            result.FoodHabitIds = food.FoodHabits.Select(s => s.FoodHabit).ToList();
            result.ImageUri = "FoodImage/" + food.ImageUri;
            result.ImageThumb ="FoodThumb/" + food.ImageThumb;
            //List<int> measureUnits = new List<int> { 36, 37, 58 };

            //if(result.MeasureUnits== null)
            //{
            //    result.MeasureUnits = measureUnits;
            //}
            //else
            //{
            //    result.MeasureUnits.AddRange(measureUnits);
            //}

            await _redisCacheClient.Db1.AddAsync($"FoodV2:{request.Id}", result, expiresIn: TimeSpan.FromDays(7));
            return result;
        }
    }
}