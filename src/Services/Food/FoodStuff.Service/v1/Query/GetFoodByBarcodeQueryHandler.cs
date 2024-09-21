using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using FoodStuff.Common.Utilities;
using Dapper;
using FoodStuff.Data.Contracts;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace FoodStuff.Service.v1.Query
{
    public class GetFoodByBarcodeQueryHandler : IRequestHandler<GetFoodByBarcodeQuery, PageResult<FoodResult>>
        , IScopedDependency
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IRedisCacheClient _redisCacheClient;

        public GetFoodByBarcodeQueryHandler(IDatabaseConnectionFactory connectionFactory, IRedisCacheClient redisCacheClient)
        {
            _connectionFactory = connectionFactory;
            _redisCacheClient = redisCacheClient;
        }

        public async Task<PageResult<FoodResult>> Handle(GetFoodByBarcodeQuery request, CancellationToken cancellationToken)
        {

            List<FoodResult> paging = new List<FoodResult>();

            if (!string.IsNullOrEmpty(request.foodInputParameters.Barcode))
            {

                if (await _redisCacheClient.Db4.ExistsAsync(
                        $"FoodSearchBarcode_{request.foodInputParameters.Barcode}"))
                {
                    paging = await _redisCacheClient.Db4.GetAsync<List<FoodResult>>
                        ($"FoodSearchBarcode_{request.foodInputParameters.Barcode}");
                }
                else
                {
                    using var conn = await _connectionFactory.CreateConnectionAsync();

                    string foodQuery = "SELECT f.\"Id\", f.\"FoodType\", f.\"FoodCode\", " +
                                       "f.\"NutrientValue\", f.\"BrandId\" ,tb.\"Persian\" NameBrand," +
                                       " tr.\"Persian\" NameTranslate, f.\"BarcodeGs1\"," +
                                       " f.\"BarcodeNational\" " +
                                       "from \"Foods\" f " +
                                       "FULL join \"Brands\" b ON b.\"Id\" = f.\"BrandId\" " +
                                       "FULL join \"Translations\" tb ON tb.\"Id\" = b.\"NameId\" " +
                                       "FULL JOIN \"Translations\" tr ON tr.\"Id\" = f.\"NameId\" " +
                                       "WHERE f.\"IsActive\"=true AND " +
                                       $"(f.\"BarcodeGs1\"= '{request.foodInputParameters.Barcode}' OR f.\"BarcodeNational\"='{request.foodInputParameters.Barcode}')";


                    FoodVMQ foodVmq = await conn.QuerySingleOrDefaultAsync<FoodVMQ>(foodQuery);


                    if (foodVmq!=null)
                    {
                        FoodResult foodResult = new FoodResult();

                        foodResult.Name = foodVmq.NameTranslate;
                        foodResult.FoodCode = foodVmq.FoodCode;
                        foodResult.FoodId = foodVmq.Id;
                        foodResult.FoodType = foodVmq.FoodType;
                        foodResult.NutrientValue = foodVmq.NutrientValue;
                        foodResult.BrandName = new TranslationName { Persian = foodVmq.NameBrand };

                        paging.Add(foodResult);
                    }

                   


                    if (paging.Any())
                    {
                        await _redisCacheClient.Db4.AddAsync(
                            $"FoodSearchBarcode_{request.foodInputParameters.Barcode}",
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
