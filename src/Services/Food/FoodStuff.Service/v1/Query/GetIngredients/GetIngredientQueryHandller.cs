using Common;
using Common.Utilities;
using Dapper;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace FoodStuff.Service.v1.Query.GetIngredients
{
    public class GetIngredientQueryHandller : IRequestHandler<GetIngredientQuery, List<IngSearchResultDTO>>, IScopedDependency
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IRedisCacheClient _redisCacheClient;

        public GetIngredientQueryHandller(IDatabaseConnectionFactory connectionFactory,
            IRedisCacheClient redisCacheClient)
        {
            _connectionFactory = connectionFactory;
            _redisCacheClient = redisCacheClient;
        }

        public async Task<List<IngSearchResultDTO>> Handle(GetIngredientQuery request, CancellationToken cancellationToken)
        {
            List<IngSearchResultDTO> ingList = new List<IngSearchResultDTO>();

            if (await _redisCacheClient.Db3.ExistsAsync
                    ($"Ingredient_{request.name}_{request.LanguageName}_" +
                     $"{request.Page}_{request.PageSize}"))
            {
                ingList = await _redisCacheClient.Db3.GetAsync<List<IngSearchResultDTO>>
                ($"Ingredient_{request.name}_{request.LanguageName}_" +
                 $"{request.Page}_{request.PageSize}");

            }
            else
            {


                using var conn = await _connectionFactory.CreateConnectionAsync();

                var searchstrings = request.name.ToLower().Split(' ').ToList();
                string SearchPatameter = null;
                int n = 0;
                foreach (var item in searchstrings)
                {
                    // if (searchstrings.First() == item)
                    if (n == 0)
                    {
                        SearchPatameter = SearchPatameter + $"\'%{item}%\'";
                    }
                    else
                    {
                        SearchPatameter = SearchPatameter + $" , \'%{item}%\'";
                    }

                    n++;
                }



                switch (request.LanguageName)
                {
                    case "English":
                        {
                            ingList = conn.Query<IngSearchResultDTO>(
                                    $"SELECT \"Ingredients\".\"Id\", TransIngr.\"English\" NameTranslate , \"Ingredients\".\"Code\" FROM public.\"Ingredients\" INNER JOIN public.\"Translations\" TransIngr ON TransIngr.\"Id\" = \"Ingredients\".\"NameId\" WHERE lower(\"Ingredients\".\"TagArEn\") LIKE ALL (ARRAY[{SearchPatameter}]) OR \"Ingredients\".\"Code\" LIKE ALL (ARRAY[{SearchPatameter}]) OR lower(TransIngr.\"English\") LIKE ALL (ARRAY[{SearchPatameter}]) ORDER BY length(TransIngr.\"English\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}")
                                .ToList();
                            break;
                        }
                    case "Arabic":
                        {
                            ingList = conn.Query<IngSearchResultDTO>(
                                    $"SELECT \"Ingredients\".\"Id\", TransIngr.\"Arabic\" NameTranslate, \"Ingredients\".\"Code\" FROM public.\"Ingredients\" INNER JOIN public.\"Translations\" TransIngr ON TransIngr.\"Id\" = \"Ingredients\".\"NameId\" WHERE lower(\"Ingredients\".\"TagArEn\") LIKE ALL (ARRAY[{SearchPatameter}]) OR \"Ingredients\".\"Code\" LIKE ALL (ARRAY[{SearchPatameter}]) OR lower(TransIngr.\"Arabic\") LIKE ALL (ARRAY[{SearchPatameter}]) ORDER BY length(TransIngr.\"Arabic\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}")
                                .ToList();
                            break;
                        }
                    case "Persian":
                        {
                            SearchPatameter = SearchPatameter.FixPersianChars();
                            ingList = conn.Query<IngSearchResultDTO>(
                                    $"SELECT \"Ingredients\".\"Id\", TransIngr.\"Persian\" NameTranslate , \"Ingredients\".\"Code\" FROM public.\"Ingredients\" INNER JOIN public.\"Translations\" TransIngr ON TransIngr.\"Id\" = \"Ingredients\".\"NameId\" WHERE lower(\"Ingredients\".\"Tag\") LIKE ALL (ARRAY[{SearchPatameter}]) OR \"Ingredients\".\"Code\" LIKE ALL (ARRAY[{SearchPatameter}]) OR lower(TransIngr.\"Persian\") LIKE ALL (ARRAY[{SearchPatameter}]) ORDER BY length(TransIngr.\"Persian\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}")
                                .ToList();
                            break;
                        }
                    default:
                        {
                            SearchPatameter = SearchPatameter.FixPersianChars();
                            ingList = conn.Query<IngSearchResultDTO>(
                                    $"SELECT \"Ingredients\".\"Id\", TransIngr.\"Persian\" NameTranslate , \"Ingredients\".\"Code\" FROM public.\"Ingredients\" INNER JOIN public.\"Translations\" TransIngr ON TransIngr.\"Id\" = \"Ingredients\".\"NameId\" WHERE lower(\"Ingredients\".\"Tag\") LIKE ALL (ARRAY[{SearchPatameter}]) OR \"Ingredients\".\"Code\" LIKE ALL (ARRAY[{SearchPatameter}]) OR lower(TransIngr.\"Persian\") LIKE ALL (ARRAY[{SearchPatameter}]) ORDER BY length(TransIngr.\"Persian\") ASC OFFSET {(request.Page - 1 ?? 0) * request.PageSize} LIMIT {request.PageSize}")
                                .ToList();
                            break;
                        }
                }

                await _redisCacheClient.Db3.AddAsync($"Ingredient_{request.name}_{request.LanguageName}_" +
                                               $"{request.Page}_{request.PageSize}", ingList);
            }


            return ingList;

        }
    }
}
