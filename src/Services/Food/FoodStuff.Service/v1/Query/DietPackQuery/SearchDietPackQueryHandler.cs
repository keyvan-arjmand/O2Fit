using Common;
using Common.Utilities;
using Dapper;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query.DietPackQuery
{
    public class SearchDietPackQueryHandler : IRequestHandler<SearchDietPackQuery, PageResult<DietPackSearchResultViewModel>>, IScopedDependency
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IRedisCacheClient _redisCacheClient;

        public SearchDietPackQueryHandler(IDatabaseConnectionFactory connectionFactory,
            IRedisCacheClient redisCacheClient)
        {
            _connectionFactory = connectionFactory;
            _redisCacheClient = redisCacheClient;

        }

        public async Task<PageResult<DietPackSearchResultViewModel>> Handle(SearchDietPackQuery request,
            CancellationToken cancellationToken)
        {
            List<DietPackSearchResultViewModel> dietPacks = new List<DietPackSearchResultViewModel>();
            if (await _redisCacheClient.Db9.ExistsAsync($"DietPackSearch_{request.Name}_" +
                $"{StringConvertor.IntToString(request.NationalityIds)}_{request.CalorieValue}" +
                $"_{request.FoodMeal}_{request.DailyCalorie}_{StringConvertor.IntToString(request.DietCategoryIds)}" +
                $"_{StringConvertor.IntToString(request.IngredientAllergyIds)}_" +
                $"{StringConvertor.IntToString(request.SpecialDiseases)}_{request.Language}_{request.Page}_{request.PageSize}"))
            {
                dietPacks = await _redisCacheClient.Db9.GetAsync<List<DietPackSearchResultViewModel>>(
                    $"DietPackSearch_{request.Name}_" +
                $"{StringConvertor.IntToString(request.NationalityIds)}_{request.CalorieValue}" +
                $"_{request.FoodMeal}_{request.DailyCalorie}_{StringConvertor.IntToString(request.DietCategoryIds)}" +
                $"_{StringConvertor.IntToString(request.IngredientAllergyIds)}_" +
                $"{StringConvertor.IntToString(request.SpecialDiseases)}_{request.Language}_{request.Page}_{request.PageSize}");
            }
            else
            {
                var minCalorie = request.CalorieValue - (request.CalorieValue * 3 / 100);
                var maxCalorie = request.CalorieValue + (request.CalorieValue * 3 / 100);

                var minDailyCalorie = request.DailyCalorie - (request.DailyCalorie * 3 / 100);
                var maxDailyCalorie = request.DailyCalorie + (request.DailyCalorie * 3 / 100);



                if (string.IsNullOrEmpty(request.Name) || string.IsNullOrWhiteSpace(request.Name))
                {
                    string nationalityParameter = null;
                    if (request.NationalityIds != null)
                    {
                        int number = 0;

                        foreach (var nationality in request.NationalityIds)
                        {
                            if (number == 0)
                            {
                                nationalityParameter += $"{nationality}";
                            }
                            else
                            {
                                nationalityParameter += $",{nationality}";
                            }
                            number++;
                        }
                    }

                    string dietCategoryParameter = null;
                    if (request.DietCategoryIds != null)
                    {
                        int number = 0;

                        foreach (var dietCategory in request.DietCategoryIds)
                        {
                            if (number == 0)
                            {
                                dietCategoryParameter += $"{dietCategory}";
                            }
                            else
                            {
                                dietCategoryParameter += $",{dietCategory}";
                            }
                            number++;
                        }
                    }

                    string specialDiseaseParameter = null;
                    if (request.SpecialDiseases != null)
                    {
                        int number = 0;

                        foreach (var specialDisease in request.SpecialDiseases)
                        {
                            if (number == 0)
                            {
                                specialDiseaseParameter += $"{specialDisease}";
                            }
                            else
                            {
                                specialDiseaseParameter += $",{specialDisease}";
                            }
                            number++;
                        }
                    }

                    string IngredientAllergyIdParameter = null;
                    if (request.IngredientAllergyIds != null)
                    {
                        int number = 0;

                        foreach (var IngredientAllergyId in request.IngredientAllergyIds)
                        {
                            if (number == 0)
                            {
                                IngredientAllergyIdParameter += $"{IngredientAllergyId}";
                            }
                            else
                            {
                                IngredientAllergyIdParameter += $",{IngredientAllergyId}";
                            }
                            number++;
                        }
                    }


                    var conn = await _connectionFactory.CreateConnectionAsync();
                    if (request.DietCategoryIds != null && request.NationalityIds != null &&
                        request.SpecialDiseases != null && request.IngredientAllergyIds != null)
                    {



                        string query = $"SELECT DISTINCT \"DietPacks\".\"Id\" PackId,\"DietPacks\".\"CaloriValue\" CalorieValue,tr.\"Persian\" PackName FROM public.\"DietPacks\" JOIN public.\"DietPackSpecialDiseases\" ON \"DietPackSpecialDiseases\".\"DietPackId\" = \"DietPacks\".\"Id\" join public.\"DietPackAlerges\" ON \"DietPackAlerges\".\"DietPackId\" = \"DietPacks\".\"Id\" join public.\"DietPackNationalities\" ON \"DietPackNationalities\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"DietPackDietCategories\" ON \"DietPackDietCategories\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"Translations\" tr ON tr.\"Id\" = \"DietPacks\".\"NameId\" WHERE  \"DietPackNationalities\".\"NationalityId\" in ({nationalityParameter}) AND \"DietPackAlerges\".\"IngredientId\" NOT IN ({IngredientAllergyIdParameter}) and \"DietPacks\".\"FoodMeal\"={request.FoodMeal} AND \"DietPackSpecialDiseases\".\"SpecialDisease\" in ({specialDiseaseParameter}) AND  \"DietPackDietCategories\".\"DietCategoryId\" in ({dietCategoryParameter}) AND \"DietPacks\".\"CaloriValue\">{minCalorie} AND \"DietPacks\".\"CaloriValue\"<{maxCalorie} AND \"DietPacks\".\"DailyCalorie\">{minDailyCalorie} AND \"DietPacks\".\"DailyCalorie\"<{maxDailyCalorie} ORDER BY \"DietPacks\".\"Id\" DESC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}";

                        dietPacks = (List<DietPackSearchResultViewModel>)await conn.QueryAsync<DietPackSearchResultViewModel>(query);



                    }
                    else
                    {
                        if (request.DietCategoryIds != null && request.NationalityIds != null &&
                                          request.SpecialDiseases != null)
                        {


                            string query = $"SELECT DISTINCT \"DietPacks\".\"Id\" PackId, \"DietPacks\".\"CaloriValue\" CalorieValue,tr.\"Persian\" PackName FROM public.\"DietPacks\" JOIN public.\"DietPackSpecialDiseases\" ON \"DietPackSpecialDiseases\".\"DietPackId\" = \"DietPacks\".\"Id\" join public.\"DietPackNationalities\" ON \"DietPackNationalities\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"DietPackDietCategories\" ON \"DietPackDietCategories\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"Translations\" tr ON tr.\"Id\" = \"DietPacks\".\"NameId\" WHERE  \"DietPackNationalities\".\"NationalityId\" in ({nationalityParameter}) AND \"DietPacks\".\"FoodMeal\"={request.FoodMeal} AND \"DietPackSpecialDiseases\".\"SpecialDisease\" in ({specialDiseaseParameter}) AND  \"DietPackDietCategories\".\"DietCategoryId\" in ({dietCategoryParameter}) AND \"DietPacks\".\"CaloriValue\">{minCalorie} AND \"DietPacks\".\"CaloriValue\"<{maxCalorie} AND \"DietPacks\".\"DailyCalorie\">{minDailyCalorie} AND \"DietPacks\".\"DailyCalorie\"<{maxDailyCalorie} ORDER BY \"DietPacks\".\"Id\" DESC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}";

                            dietPacks = (List<DietPackSearchResultViewModel>)await conn.QueryAsync<DietPackSearchResultViewModel>(query);

                        }
                        else
                        {
                            if (request.DietCategoryIds != null && request.NationalityIds != null)
                            {

                                string query = $"SELECT DISTINCT \"DietPacks\".\"Id\" PackId,\"DietPacks\".\"CaloriValue\" CalorieValue,tr.\"Persian\" PackName FROM public.\"DietPacks\" join public.\"DietPackNationalities\" ON \"DietPackNationalities\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"DietPackDietCategories\" ON \"DietPackDietCategories\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"Translations\" tr ON tr.\"Id\" = \"DietPacks\".\"NameId\" WHERE  \"DietPackNationalities\".\"NationalityId\" in ({nationalityParameter}) AND \"DietPacks\".\"FoodMeal\"={request.FoodMeal} AND \"DietPackDietCategories\".\"DietCategoryId\" in ({dietCategoryParameter}) AND \"DietPacks\".\"CaloriValue\">{minCalorie} AND \"DietPacks\".\"CaloriValue\"<{maxCalorie} AND \"DietPacks\".\"DailyCalorie\">{minDailyCalorie} AND \"DietPacks\".\"DailyCalorie\"<{maxDailyCalorie} ORDER BY \"DietPacks\".\"Id\" DESC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}";

                                dietPacks = (List<DietPackSearchResultViewModel>)await conn.QueryAsync<DietPackSearchResultViewModel>(query);

                            }
                            else
                            {
                                if (request.DietCategoryIds != null)
                                {

                                    string query = $"SELECT DISTINCT \"DietPacks\".\"Id\" PackId,\"DietPacks\".\"CaloriValue\" CalorieValue,tr.\"Persian\" PackName FROM public.\"DietPacks\" JOIN public.\"DietPackDietCategories\" ON \"DietPackDietCategories\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"Translations\" tr ON tr.\"Id\" = \"DietPacks\".\"NameId\" WHERE  \"DietPacks\".\"FoodMeal\"={request.FoodMeal} AND \"DietPackDietCategories\".\"DietCategoryId\" in ({dietCategoryParameter}) AND \"DietPacks\".\"CaloriValue\">{minCalorie} AND \"DietPacks\".\"CaloriValue\"<{maxCalorie} AND \"DietPacks\".\"DailyCalorie\">{minDailyCalorie} AND \"DietPacks\".\"DailyCalorie\"<{maxDailyCalorie} ORDER BY \"DietPacks\".\"Id\" DESC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}";

                                    dietPacks = (List<DietPackSearchResultViewModel>)await conn.QueryAsync<DietPackSearchResultViewModel>(query);
                                }

                            }

                        }


                    }
                }
                else
                {

                    string searchParameter = null;
                    if (request.Language == "Persian")
                    {
                        request.Name = request.Name.FixPersianChars();
                    }
                    var searchStrings = request.Name.Split(' ').ToList();
                    int n = 0;
                    foreach (var item in searchStrings)
                    {
                        if (n == 0)
                        {
                            searchParameter += $"{item}";
                        }
                        else
                        {
                            searchParameter += $"|{item}";
                        }
                        n++;
                    }


                    string nationalityParameter = null;
                    if (request.NationalityIds != null)
                    {
                        int number = 0;

                        foreach (var nationality in request.NationalityIds)
                        {
                            if (number == 0)
                            {
                                nationalityParameter += $"{nationality}";
                            }
                            else
                            {
                                nationalityParameter += $",{nationality}";
                            }
                            number++;
                        }
                    }

                    string dietCategoryParameter = null;
                    if (request.DietCategoryIds != null)
                    {
                        int number = 0;

                        foreach (var dietCategory in request.DietCategoryIds)
                        {
                            if (number == 0)
                            {
                                dietCategoryParameter += $"{dietCategory}";
                            }
                            else
                            {
                                dietCategoryParameter += $",{dietCategory}";
                            }
                            number++;
                        }
                    }

                    string specialDiseaseParameter = null;
                    if (request.SpecialDiseases != null)
                    {
                        int number = 0;

                        foreach (var specialDisease in request.SpecialDiseases)
                        {
                            if (number == 0)
                            {
                                specialDiseaseParameter += $"{specialDisease}";
                            }
                            else
                            {
                                specialDiseaseParameter += $",{specialDisease}";
                            }
                            number++;
                        }
                    }

                    string IngredientAllergyIdParameter = null;
                    if (request.IngredientAllergyIds != null)
                    {
                        int number = 0;

                        foreach (var IngredientAllergyId in request.IngredientAllergyIds)
                        {
                            if (number == 0)
                            {
                                IngredientAllergyIdParameter += $"{IngredientAllergyId}";
                            }
                            else
                            {
                                IngredientAllergyIdParameter += $",{IngredientAllergyId}";
                            }
                            number++;
                        }
                    }


                    var conn = await _connectionFactory.CreateConnectionAsync();
                    if (request.DietCategoryIds != null && request.NationalityIds != null &&
                        request.SpecialDiseases != null && request.IngredientAllergyIds != null)
                    {



                        string query = $"SELECT DISTINCT \"DietPacks\".\"Id\" PackId,\"DietPacks\".\"CaloriValue\" CalorieValue,tr.\"Persian\" PackName FROM public.\"DietPacks\" JOIN public.\"DietPackSpecialDiseases\" ON \"DietPackSpecialDiseases\".\"DietPackId\" = \"DietPacks\".\"Id\" join public.\"DietPackAlerges\" ON \"DietPackAlerges\".\"DietPackId\" = \"DietPacks\".\"Id\" join public.\"DietPackNationalities\" ON \"DietPackNationalities\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"DietPackDietCategories\" ON \"DietPackDietCategories\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"Translations\" tr ON tr.\"Id\" = \"DietPacks\".\"NameId\" WHERE lower(tr.\"{request.Language}\") similar to '%({searchParameter})%' AND \"DietPackNationalities\".\"NationalityId\" in ({nationalityParameter}) AND \"DietPackAlerges\".\"IngredientId\" NOT IN ({IngredientAllergyIdParameter}) and \"DietPacks\".\"FoodMeal\"={request.FoodMeal} AND \"DietPackSpecialDiseases\".\"SpecialDisease\" in ({specialDiseaseParameter}) AND  \"DietPackDietCategories\".\"DietCategoryId\" in ({dietCategoryParameter}) AND \"DietPacks\".\"CaloriValue\">{minCalorie} AND \"DietPacks\".\"CaloriValue\"<{maxCalorie} AND \"DietPacks\".\"DailyCalorie\">{minDailyCalorie} AND \"DietPacks\".\"DailyCalorie\"<{maxDailyCalorie} ORDER BY \"DietPacks\".\"Id\" DESC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}";

                        dietPacks = (List<DietPackSearchResultViewModel>)await conn.QueryAsync<DietPackSearchResultViewModel>(query);



                    }
                    else
                    {
                        if (request.DietCategoryIds != null && request.NationalityIds != null &&
                                          request.SpecialDiseases != null)
                        {

                            string query = $"SELECT DISTINCT \"DietPacks\".\"Id\" PackId,\"DietPacks\".\"CaloriValue\" CalorieValue,tr.\"Persian\" PackName FROM public.\"DietPacks\" JOIN public.\"DietPackSpecialDiseases\" ON \"DietPackSpecialDiseases\".\"DietPackId\" = \"DietPacks\".\"Id\" join public.\"DietPackNationalities\" ON \"DietPackNationalities\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"DietPackDietCategories\" ON \"DietPackDietCategories\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"Translations\" tr ON tr.\"Id\" = \"DietPacks\".\"NameId\" WHERE lower(tr.\"{request.Language}\") similar to '%({searchParameter})%' AND \"DietPackNationalities\".\"NationalityId\" in ({nationalityParameter}) AND \"DietPacks\".\"FoodMeal\"={request.FoodMeal} AND \"DietPackSpecialDiseases\".\"SpecialDisease\" in ({specialDiseaseParameter}) AND  \"DietPackDietCategories\".\"DietCategoryId\" in ({dietCategoryParameter}) AND \"DietPacks\".\"CaloriValue\">{minCalorie} AND \"DietPacks\".\"CaloriValue\"<{maxCalorie} AND \"DietPacks\".\"DailyCalorie\">{minDailyCalorie} AND \"DietPacks\".\"DailyCalorie\"<{maxDailyCalorie} ORDER BY \"DietPacks\".\"Id\" DESC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}";

                            dietPacks = (List<DietPackSearchResultViewModel>)await conn.QueryAsync<DietPackSearchResultViewModel>(query);

                        }
                        else
                        {
                            if (request.DietCategoryIds != null && request.NationalityIds != null)
                            {
                                string query = $"SELECT DISTINCT \"DietPacks\".\"Id\" PackId,\"DietPacks\".\"CaloriValue\" CalorieValue,tr.\"Persian\" PackName FROM public.\"DietPacks\" join public.\"DietPackNationalities\" ON \"DietPackNationalities\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"DietPackDietCategories\" ON \"DietPackDietCategories\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"Translations\" tr ON tr.\"Id\" = \"DietPacks\".\"NameId\" WHERE lower(tr.\"{request.Language}\") similar to '%({searchParameter})%' AND \"DietPackNationalities\".\"NationalityId\" in ({nationalityParameter}) AND \"DietPacks\".\"FoodMeal\"={request.FoodMeal} AND \"DietPackDietCategories\".\"DietCategoryId\" in ({dietCategoryParameter}) AND \"DietPacks\".\"CaloriValue\">{minCalorie} AND \"DietPacks\".\"CaloriValue\"<{maxCalorie} AND \"DietPacks\".\"DailyCalorie\">{minDailyCalorie} AND \"DietPacks\".\"DailyCalorie\"<{maxDailyCalorie} ORDER BY \"DietPacks\".\"Id\" DESC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}";

                                dietPacks = (List<DietPackSearchResultViewModel>)await conn.QueryAsync<DietPackSearchResultViewModel>(query);

                            }
                            else
                            {
                                if (request.DietCategoryIds != null)
                                {
                                    string query = $"SELECT DISTINCT \"DietPacks\".\"Id\" PackId,\"DietPacks\".\"CaloriValue\" CalorieValue,tr.\"Persian\" PackName FROM public.\"DietPacks\" JOIN public.\"DietPackDietCategories\" ON \"DietPackDietCategories\".\"DietPackId\" = \"DietPacks\".\"Id\" JOIN public.\"Translations\" tr ON tr.\"Id\" = \"DietPacks\".\"NameId\" WHERE lower(tr.\"{request.Language}\") similar to '%({searchParameter})%' AND \"DietPacks\".\"FoodMeal\"={request.FoodMeal} AND \"DietPackDietCategories\".\"DietCategoryId\" in ({dietCategoryParameter}) AND \"DietPacks\".\"CaloriValue\">{minCalorie} AND \"DietPacks\".\"CaloriValue\"<{maxCalorie} AND \"DietPacks\".\"DailyCalorie\">{minDailyCalorie} AND \"DietPacks\".\"DailyCalorie\"<{maxDailyCalorie} ORDER BY \"DietPacks\".\"Id\" DESC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}";

                                    dietPacks = (List<DietPackSearchResultViewModel>)await conn.QueryAsync<DietPackSearchResultViewModel>(query);
                                }

                            }

                        }


                    }
                }

                await _redisCacheClient.Db9.AddAsync($"DietPackSearch_{request.Name}_" +
                    $"{StringConvertor.IntToString(request.NationalityIds)}_{request.CalorieValue}" +
                    $"_{request.FoodMeal}_{request.DailyCalorie}_{StringConvertor.IntToString(request.DietCategoryIds)}" +
                    $"_{StringConvertor.IntToString(request.IngredientAllergyIds)}_" +
                    $"{StringConvertor.IntToString(request.SpecialDiseases)}_{request.Language}_{request.Page}_{request.PageSize}"
                    , dietPacks);

            }




            PageResult<DietPackSearchResultViewModel> result = new PageResult<DietPackSearchResultViewModel>
            {
                Items = dietPacks,
                Count = dietPacks.Count,
                PageIndex = (int)request.Page,
                PageSize = request.PageSize,
            };

            return result;
        }

    }
}

