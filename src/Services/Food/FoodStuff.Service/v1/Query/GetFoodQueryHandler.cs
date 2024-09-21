using Common;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Dapper;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace FoodStuff.Service.v1.Query
{
    public class GetFoodQueryHandler : IRequestHandler<GetFoodQuery, PageResult<FoodResult>>, IScopedDependency
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IRedisCacheClient _redisCacheClient;

        public GetFoodQueryHandler(IFoodRepository foodRepository, IDatabaseConnectionFactory connectionFactory, IRedisCacheClient redisCacheClient)
        {
            _foodRepository = foodRepository;
            _connectionFactory = connectionFactory;
            _redisCacheClient = redisCacheClient;
        }

        public async Task<PageResult<FoodResult>> Handle(GetFoodQuery request, CancellationToken cancellationToken)
        {
            List<FoodVMQ> foodVmq;
            List<FoodResult> paging = new List<FoodResult>();

            string searchParameter = null;
            request.foodInputParameters.Name = request.foodInputParameters.Name.ToLower();
            if (!string.IsNullOrEmpty(request.foodInputParameters.Name))
            {
                var parameter = request.foodInputParameters.Name.ToLower();
                if (await _redisCacheClient.Db5.ExistsAsync(
                        $"FoodSearch_{request.LanguageName}_{request.Page}_{request.PageSize}_{parameter}"))
                {
                    paging = await _redisCacheClient.Db5.GetAsync<List<FoodResult>>(
                        $"FoodSearch_{request.LanguageName}_{request.Page}_{request.PageSize}_{parameter}");


                    var redisResult = new PageResult<FoodResult>
                    {
                        //Count = countDetails,
                        PageIndex = request.Page ?? 1,
                        PageSize = request.PageSize,
                        Items = paging
                    };

                    return redisResult;
                }

                if (!paging.Any())
                {
                    var searchStrings = parameter.Split(' ').ToList();
                    int n = 0;
                    foreach (var item in searchStrings)
                    {
                        if (n == 0)
                        {
                            searchParameter += $"\'%{item}%\'";
                        }
                        else
                        {
                            searchParameter += $" , \'%{item}%\'";
                        }
                        n++;
                    }
                    using var conn = await _connectionFactory.CreateConnectionAsync();
                    switch (request.LanguageName)
                    {
                        case "Persian":
                            {
                                searchParameter = searchParameter.FixPersianChars();
                                foodVmq = conn.Query<FoodVMQ>($"SELECT \"Foods\".\"Id\", \"Foods\".\"FoodType\", \"Foods\".\"FoodCode\",\"Foods\".\"NutrientValue\", \"Foods\".\"BrandId\", \"Foods\".\"BarcodeGs1\", \"Foods\".\"BarcodeNational\", \"Foods\".\"IsActive\",TransBrand.\"Persian\" NameBrand, TransFood.\"Persian\" NameTranslate FROM public.\"Foods\" INNER JOIN public.\"Translations\" TransFood ON TransFood.\"Id\" = \"Foods\".\"NameId\" LEFT JOIN public.\"Brands\" OwnBrand ON \"Foods\".\"BrandId\" = OwnBrand.\"Id\" LEFT JOIN public.\"Translations\" TransBrand ON TransBrand.\"Id\" = OwnBrand.\"NameId\"  WHERE \"Foods\".\"IsActive\"=true AND  (TransFood.\"Persian\" LIKE ALL (ARRAY[{searchParameter}]) OR \"Foods\".\"Tag\" LIKE ALL (ARRAY[{searchParameter}])) ORDER BY \"Foods\".\"FoodType\" ASC, length(TransFood.\"Persian\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}").ToList();
                                break;
                            }
                        case "English":
                            {
                                foodVmq = conn.Query<FoodVMQ>($"SELECT \"Foods\".\"Id\", \"Foods\".\"FoodType\", \"Foods\".\"FoodCode\",\"Foods\".\"NutrientValue\", \"Foods\".\"BrandId\", \"Foods\".\"BarcodeGs1\", \"Foods\".\"BarcodeNational\", \"Foods\".\"IsActive\",TransBrand.\"English\" NameBrand, TransFood.\"English\" NameTranslate FROM public.\"Foods\" INNER JOIN public.\"Translations\" TransFood ON TransFood.\"Id\" = \"Foods\".\"NameId\" LEFT JOIN public.\"Brands\" OwnBrand ON \"Foods\".\"BrandId\" = OwnBrand.\"Id\" LEFT JOIN public.\"Translations\" TransBrand ON TransBrand.\"Id\" = OwnBrand.\"NameId\"  WHERE \"Foods\".\"IsActive\"=true AND  (TransFood.\"English\" LIKE ALL (ARRAY[{searchParameter}]) OR \"Foods\".\"TagArEn\" LIKE ALL (ARRAY[{searchParameter}])) ORDER BY \"Foods\".\"FoodType\" ASC, length(TransFood.\"English\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}").ToList();
                                break;
                            }
                        case "Arabic":
                            {
                                foodVmq = conn.Query<FoodVMQ>($"SELECT \"Foods\".\"Id\", \"Foods\".\"FoodType\", \"Foods\".\"FoodCode\",\"Foods\".\"NutrientValue\", \"Foods\".\"BrandId\", \"Foods\".\"BarcodeGs1\", \"Foods\".\"BarcodeNational\", \"Foods\".\"IsActive\",TransBrand.\"Arabic\" NameBrand, TransFood.\"Arabic\" NameTranslate FROM public.\"Foods\" INNER JOIN public.\"Translations\" TransFood ON TransFood.\"Id\" = \"Foods\".\"NameId\" LEFT JOIN public.\"Brands\" OwnBrand ON \"Foods\".\"BrandId\" = OwnBrand.\"Id\" LEFT JOIN public.\"Translations\" TransBrand ON TransBrand.\"Id\" = OwnBrand.\"NameId\"  WHERE \"Foods\".\"IsActive\"=true AND  (TransFood.\"Arabic\" LIKE ALL (ARRAY[{searchParameter}]) OR \"Foods\".\"TagArEn\" LIKE ALL (ARRAY[{searchParameter}])) ORDER BY \"Foods\".\"FoodType\" ASC, length(TransFood.\"Arabic\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}").ToList();
                                break;
                            }
                        default:
                            {
                                searchParameter = searchParameter.FixPersianChars();
                                foodVmq = conn.Query<FoodVMQ>($"SELECT \"Foods\".\"Id\", \"Foods\".\"FoodType\", \"Foods\".\"FoodCode\",\"Foods\".\"NutrientValue\", \"Foods\".\"BrandId\", \"Foods\".\"BarcodeGs1\", \"Foods\".\"BarcodeNational\", \"Foods\".\"IsActive\",TransBrand.\"Persian\" NameBrand, TransFood.\"Persian\" NameTranslate FROM public.\"Foods\" INNER JOIN public.\"Translations\" TransFood ON TransFood.\"Id\" = \"Foods\".\"NameId\" LEFT JOIN public.\"Brands\" OwnBrand ON \"Foods\".\"BrandId\" = OwnBrand.\"Id\" LEFT JOIN public.\"Translations\" TransBrand ON TransBrand.\"Id\" = OwnBrand.\"NameId\"  WHERE \"Foods\".\"IsActive\"=true AND  (TransFood.\"Persian\" LIKE ALL (ARRAY[{searchParameter}]) OR \"Foods\".\"Tag\" LIKE ALL (ARRAY[{searchParameter}])) ORDER BY \"Foods\".\"FoodType\" ASC, length(TransFood.\"Persian\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}").ToList();
                                break;
                            }
                    }

                    if (request.LanguageName == "English")
                    {
                        foreach (var item in foodVmq)
                        {
                            FoodResult foodResult = new FoodResult();

                            foodResult.Name = item.NameTranslate;
                            foodResult.FoodCode = item.FoodCode;
                            foodResult.FoodId = item.Id;
                            foodResult.FoodType = item.FoodType;
                            foodResult.NutrientValue = item.NutrientValue;
                            foodResult.BrandName = new TranslationName { English = item.NameBrand };
                            paging.Add(foodResult);
                        }
                    }
                    else
                    {
                        if (request.LanguageName == "Arabic")
                        {
                            foreach (var item in foodVmq)
                            {
                                FoodResult foodResult = new FoodResult();

                                foodResult.Name = item.NameTranslate;
                                foodResult.FoodCode = item.FoodCode;
                                foodResult.FoodId = item.Id;
                                foodResult.FoodType = item.FoodType;
                                foodResult.NutrientValue = item.NutrientValue;
                                foodResult.BrandName = new TranslationName { Arabic = item.NameBrand };
                                paging.Add(foodResult);
                            }
                        }
                        else
                        {
                            foreach (var item in foodVmq)
                            {
                                FoodResult foodResult = new FoodResult();

                                foodResult.Name = item.NameTranslate;
                                foodResult.FoodCode = item.FoodCode;
                                foodResult.FoodId = item.Id;
                                foodResult.FoodType = item.FoodType;
                                foodResult.NutrientValue = item.NutrientValue;
                                foodResult.BrandName = new TranslationName { Persian = item.NameBrand };
                                paging.Add(foodResult);
                            }
                        }
                    }


                    if (paging.Any())
                    {
                        await _redisCacheClient.Db5.AddAsync(
                            $"FoodSearch_{request.LanguageName}_{request.Page}_{request.PageSize}_{parameter}",
                            paging);
                    }

                }
            }



            var result = new PageResult<FoodResult>
            {
                //Count = countDetails,
                PageIndex = request.Page ?? 1,
                PageSize = request.PageSize,
                Items = paging
            };

            return result;
        }
    }
}
