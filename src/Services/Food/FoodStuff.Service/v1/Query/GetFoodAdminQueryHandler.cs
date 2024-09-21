using Common;
using Common.Utilities;
using Dapper;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query
{
    public class GetFoodAdminQueryHandler : IRequestHandler<GetFoodAdminQuery, PageResult<FoodResultAdmin>>, IScopedDependency
    {

        private readonly IDatabaseConnectionFactory _connectionFactory;

        public GetFoodAdminQueryHandler(IDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<PageResult<FoodResultAdmin>> Handle(GetFoodAdminQuery request, CancellationToken cancellationToken)
        {
            List<FoodVMQ> foodVmq;
            List<FoodResultAdmin> paging = new List<FoodResultAdmin>();

            string searchParameter = null;
            request.foodInputParameters.Name = request.foodInputParameters.Name.ToLower();
            if (!string.IsNullOrEmpty(request.foodInputParameters.Name))
            {
                var parameter = request.foodInputParameters.Name.ToLower();


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
                                foodVmq = conn.Query<FoodVMQ>($"SELECT \"Foods\".\"Id\", \"Foods\".\"FoodType\", \"Foods\".\"FoodCode\",\"Foods\".\"NutrientValue\", \"Foods\".\"BrandId\", \"Foods\".\"BarcodeGs1\", \"Foods\".\"BarcodeNational\", \"Foods\".\"IsActive\",TransBrand.\"Persian\" NameBrand, TransFood.\"Persian\" NameTranslate FROM public.\"Foods\" INNER JOIN public.\"Translations\" TransFood ON TransFood.\"Id\" = \"Foods\".\"NameId\" LEFT JOIN public.\"Brands\" OwnBrand ON \"Foods\".\"BrandId\" = OwnBrand.\"Id\" LEFT JOIN public.\"Translations\" TransBrand ON TransBrand.\"Id\" = OwnBrand.\"NameId\"  WHERE  (TransFood.\"Persian\" LIKE ALL (ARRAY[{searchParameter}]) OR \"Foods\".\"Tag\" LIKE ALL (ARRAY[{searchParameter}])) ORDER BY \"Foods\".\"FoodType\" ASC, length(TransFood.\"Persian\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}").ToList();
                                break;
                            }
                        case "English":
                            {
                                foodVmq = conn.Query<FoodVMQ>($"SELECT \"Foods\".\"Id\", \"Foods\".\"FoodType\", \"Foods\".\"FoodCode\",\"Foods\".\"NutrientValue\", \"Foods\".\"BrandId\", \"Foods\".\"BarcodeGs1\", \"Foods\".\"BarcodeNational\", \"Foods\".\"IsActive\",TransBrand.\"English\" NameBrand, TransFood.\"English\" NameTranslate FROM public.\"Foods\" INNER JOIN public.\"Translations\" TransFood ON TransFood.\"Id\" = \"Foods\".\"NameId\" LEFT JOIN public.\"Brands\" OwnBrand ON \"Foods\".\"BrandId\" = OwnBrand.\"Id\" LEFT JOIN public.\"Translations\" TransBrand ON TransBrand.\"Id\" = OwnBrand.\"NameId\"  WHERE  (TransFood.\"English\" LIKE ALL (ARRAY[{searchParameter}]) OR \"Foods\".\"TagArEn\" LIKE ALL (ARRAY[{searchParameter}])) ORDER BY \"Foods\".\"FoodType\" ASC, length(TransFood.\"English\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}").ToList();
                                break;
                            }
                        case "Arabic":
                            {
                                foodVmq = conn.Query<FoodVMQ>($"SELECT \"Foods\".\"Id\", \"Foods\".\"FoodType\", \"Foods\".\"FoodCode\",\"Foods\".\"NutrientValue\", \"Foods\".\"BrandId\", \"Foods\".\"BarcodeGs1\", \"Foods\".\"BarcodeNational\", \"Foods\".\"IsActive\",TransBrand.\"Arabic\" NameBrand, TransFood.\"Arabic\" NameTranslate FROM public.\"Foods\" INNER JOIN public.\"Translations\" TransFood ON TransFood.\"Id\" = \"Foods\".\"NameId\" LEFT JOIN public.\"Brands\" OwnBrand ON \"Foods\".\"BrandId\" = OwnBrand.\"Id\" LEFT JOIN public.\"Translations\" TransBrand ON TransBrand.\"Id\" = OwnBrand.\"NameId\"  WHERE  (TransFood.\"Arabic\" LIKE ALL (ARRAY[{searchParameter}]) OR \"Foods\".\"TagArEn\" LIKE ALL (ARRAY[{searchParameter}])) ORDER BY \"Foods\".\"FoodType\" ASC, length(TransFood.\"Arabic\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}").ToList();
                                break;
                            }
                        default:
                            {
                                searchParameter = searchParameter.FixPersianChars();
                                foodVmq = conn.Query<FoodVMQ>($"SELECT \"Foods\".\"Id\", \"Foods\".\"FoodType\", \"Foods\".\"FoodCode\",\"Foods\".\"NutrientValue\", \"Foods\".\"BrandId\", \"Foods\".\"BarcodeGs1\", \"Foods\".\"BarcodeNational\", \"Foods\".\"IsActive\",TransBrand.\"Persian\" NameBrand, TransFood.\"Persian\" NameTranslate FROM public.\"Foods\" INNER JOIN public.\"Translations\" TransFood ON TransFood.\"Id\" = \"Foods\".\"NameId\" LEFT JOIN public.\"Brands\" OwnBrand ON \"Foods\".\"BrandId\" = OwnBrand.\"Id\" LEFT JOIN public.\"Translations\" TransBrand ON TransBrand.\"Id\" = OwnBrand.\"NameId\"  WHERE  (TransFood.\"Persian\" LIKE ALL (ARRAY[{searchParameter}]) OR \"Foods\".\"Tag\" LIKE ALL (ARRAY[{searchParameter}])) ORDER BY \"Foods\".\"FoodType\" ASC, length(TransFood.\"Persian\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}").ToList();
                                break;
                            }
                    }

                    if (request.LanguageName == "English")
                    {
                        foreach (var item in foodVmq)
                        {
                            FoodResultAdmin foodResult = new FoodResultAdmin();

                            foodResult.Name = item.NameTranslate;
                            foodResult.FoodCode = item.FoodCode;
                            foodResult.FoodId = item.Id;
                            foodResult.FoodType = item.FoodType;
                            foodResult.NutrientValue = item.NutrientValue;
                            foodResult.BrandName = new TranslationName { English = item.NameBrand };
                            foodResult.IsActive = item.IsActive;
                            paging.Add(foodResult);
                        }
                    }
                    else
                    {
                        if (request.LanguageName == "Arabic")
                        {
                            foreach (var item in foodVmq)
                            {
                                FoodResultAdmin foodResult = new FoodResultAdmin();

                                foodResult.Name = item.NameTranslate;
                                foodResult.FoodCode = item.FoodCode;
                                foodResult.FoodId = item.Id;
                                foodResult.FoodType = item.FoodType;
                                foodResult.NutrientValue = item.NutrientValue;
                                foodResult.BrandName = new TranslationName { Arabic = item.NameBrand };
                                foodResult.IsActive = item.IsActive;
                                paging.Add(foodResult);
                            }
                        }
                        else
                        {
                            foreach (var item in foodVmq)
                            {
                                FoodResultAdmin foodResult = new FoodResultAdmin();

                                foodResult.Name = item.NameTranslate;
                                foodResult.FoodCode = item.FoodCode;
                                foodResult.FoodId = item.Id;
                                foodResult.FoodType = item.FoodType;
                                foodResult.NutrientValue = item.NutrientValue;
                                foodResult.BrandName = new TranslationName { Persian = item.NameBrand };
                                foodResult.IsActive = item.IsActive;
                                paging.Add(foodResult);
                            }
                        }
                    }


                }
            }



            var result = new PageResult<FoodResultAdmin>
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
