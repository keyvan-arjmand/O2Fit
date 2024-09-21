using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Data.Contracts;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using NRediSearch;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Common.Utilities;
using Microsoft.AspNetCore.Http.Connections;
using Common;

namespace FoodStuff.Data.Repositories
{
    public sealed class FastFoodsRepository : BaseCacheRepository, IFastFoodsRepository, IScopedDependency
    {
        private static readonly string ixName = "fastfoods";
        public readonly ConnectionMultiplexer _Connection;

        public FastFoodsRepository(ConnectionMultiplexer Connection) : base(Connection,indexName: ixName)
        {
            _Connection = Connection;
        }

        public List<FastFoods> Search(FastFoodsInputCommand command)
        {
            Query q = new Query($"(@persian:{ command.Name }*)|(@arabic:{ command.Name }*)|(@english:{ command.Name }*)|(@barcodeGs1:{ command.BarcodeGs1 }*)|(@barcodeNational:{ command.BarcodeNational }*)|(@foodtype:{ command.FoodType }*)");
            var result = _client.Search(q).Documents;
            return CastRedisValues<FastFoods>(result);
        }

        public async Task<IEnumerable<FastFoods>> SearchAsync(FastFoodsInputCommand command)
        {
            Query q = new Query($"(@persian:{ command.Name }*)|(@arabic:{ command.Name }*)|(@english:{ command.Name }*)|(@barcodeGs1:{ command.BarcodeGs1 }*)|(@barcodeNational:{ command.BarcodeNational }*)|(@foodtype:{ command.FoodType }*)");
            var result = await _client.SearchAsync(q);
            var stringResponse = result.Documents;
            return CastRedisValues<FastFoods>(stringResponse);
        }

        public override bool CreateIndex()
        {
            Schema sch = new Schema();
            sch.AddTextField("Persian");
            sch.AddTextField("Arabic");
            sch.AddTextField("English");
            sch.AddGeoField("BarcodeGs1");
            sch.AddGeoField("BarcodeNational");
            sch.AddGeoField("FoodType");

            //return _client.CreateIndex(sch , NRediSearch.Client.IndexOptions.Default);
            return true;
        }

        public void PushSampleData()
        {

        }
    }
}
