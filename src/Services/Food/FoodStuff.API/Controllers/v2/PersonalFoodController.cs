using AutoMapper;
using Data.Contracts;
using FoodStuff.API.Models;
using FoodStuff.Data.Contracts;
using FoodStuff.Data.Repositories;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Service.v2.Query.GetPersonalFoods;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v2
{
    [ApiVersion("2")]
    public class PersonalFoodController : v1.PersonalFoodController
    {
        private readonly IFoodMeasureUnitRepository _foodMeasurUnitrepository;
        private readonly IPersonalFoodRepository _personalFoodRepository;
        private readonly IPersonalFoodIngredientRepository _personalFoodIngredientRepository;
        private readonly IRepositoryRedis<List<PersonalFoodSelectDTO>> _personalFoodListRepositoryRedis;
        private readonly IRepositoryRedis<PersonalFoodSelectDTO> _personalFoodRepositoryRedis;
        private readonly IWebHostEnvironment _environment;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PersonalFoodController(IMediator mediator, IMapper mapper,
            IPersonalFoodRepository personalFoodRepository,
            IPersonalFoodIngredientRepository personalFoodIngredientRepository,
            IFoodMeasureUnitRepository foodMeasurUnitrepository,
            IRepositoryRedis<List<PersonalFoodSelectDTO>> personalFoodListRepositoryRedis,
            IRepositoryRedis<PersonalFoodSelectDTO> personalFoodRepositoryRedis,
            IWebHostEnvironment environment)
            : base(mediator, mapper, personalFoodRepository, personalFoodIngredientRepository,
                  foodMeasurUnitrepository, personalFoodListRepositoryRedis,
                  personalFoodRepositoryRedis, environment)
        {
            _mapper = mapper;
            _mediator = mediator;
            _environment = environment;
            _personalFoodRepository = personalFoodRepository;
            _foodMeasurUnitrepository = foodMeasurUnitrepository;
            _personalFoodRepositoryRedis = personalFoodRepositoryRedis;
            _personalFoodListRepositoryRedis = personalFoodListRepositoryRedis;
            _personalFoodIngredientRepository = personalFoodIngredientRepository;
        }

        public override async Task<ApiResult<PersonalFoodSelectDTO>> Get(int foodId, CancellationToken cancellationToken)
        {
            var result = await base.Get(foodId, cancellationToken);
            if (result.Data != null)
            {
                if (result.Data.ParentFoodId == 0 || result.Data.ParentFoodId ==null)
                {
                    List<int> newMeasureUnits = new List<int>
                    {
                        1125 ,
                        1124 ,
                        1123 ,
                        1122 ,
                        1121 ,
                        1120 ,
                        1119 ,
                        1118 ,
                        1117 ,
                        1116 ,
                        1115 ,
                        1114 ,
                        1113 ,
                        1112 ,
                        1111 ,
                        1110 ,
                        1109 ,
                        1108 ,
                        1107 ,
                        1106 ,
                        1105 ,
                        1104 ,
                        1103 ,
                        1102 ,
                        1101 ,
                        1100 
                    };
                    result.Data.MeasureUnits.AddRange(newMeasureUnits);
                    result.Data.MeasureUnits = result.Data.MeasureUnits.Distinct<int>().ToList();
                }
            }
            return result;
        }
        public override async Task<ApiResult<PersonalFoodSelectDTO>> Post(PersonalFoodDTO PersonalFoodDTO, CancellationToken cancellationToken)
        {
            var personalFood = base.Post(PersonalFoodDTO, cancellationToken).Result.Data;
            if (personalFood != null)
            {
                if (personalFood.ParentFoodId == 0 || personalFood.ParentFoodId == null)
                {
                    List<int> newMeasureUnits = new List<int>
                    {
                        1125 ,
                        1124 ,
                        1123 ,
                        1122 ,
                        1121 ,
                        1120 ,
                        1119 ,
                        1118 ,
                        1117 ,
                        1116 ,
                        1115 ,
                        1114 ,
                        1113 ,
                        1112 ,
                        1111 ,
                        1110 ,
                        1109 ,
                        1108 ,
                        1107 ,
                        1106 ,
                        1105 ,
                        1104 ,
                        1103 ,
                        1102 ,
                        1101 ,
                        1100
                    };
                    personalFood.MeasureUnits.AddRange(newMeasureUnits);
                    personalFood.MeasureUnits = personalFood.MeasureUnits.Distinct<int>().ToList();
                }

                List<PersonalFoodSelectDTO> userPersonalFoods = new List<PersonalFoodSelectDTO>();
                List<PersonalFoodSelectDTO> userPersonalFoodsRedis = await _personalFoodListRepositoryRedis.GetAsync($"UserPersonalFoods_{PersonalFoodDTO.UserId}");
                if (userPersonalFoodsRedis != null)
                {
                    userPersonalFoods.AddRange(userPersonalFoodsRedis);
                }
                userPersonalFoods.Add(personalFood);
                await _personalFoodListRepositoryRedis.UpdateAsync($"UserPersonalFoods_{PersonalFoodDTO.UserId}", userPersonalFoods);
                await _personalFoodRepositoryRedis.UpdateAsync($"PersonalFood_{personalFood.PersonalFoodId}", personalFood);
            }
            return personalFood;
        }
        public override async Task<ApiResult<List<PersonalFoodSelectDTO>>> GetByUserId(CancellationToken cancellationToken, int userId)
        {
            return await _mediator.Send(new GetPersonalFoodsQuery() { userId = userId });
        }
    }
}