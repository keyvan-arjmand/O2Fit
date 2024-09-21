using Common;
using MediatR;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query.RedisSpeed
{
    public class GetRedisSpeedQueryHandler : IRequestHandler<GetRedisSpeedQuery, string>, IScopedDependency
    {
        private readonly IRedisCacheClient _redisCacheClient;

        public GetRedisSpeedQueryHandler(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        public async Task<string> Handle(GetRedisSpeedQuery request, CancellationToken cancellationToken)
        {
            string myName = null;

            if (await _redisCacheClient.Db1.ExistsAsync("Hosi"))
            {
                myName = await _redisCacheClient.Db1.GetAsync<string>("Hosi");
            }
            else
            {
                myName = "Hossein Farhoudi";
                await _redisCacheClient.Db1.AddAsync<string>("Hosi", myName);
            }

            return myName;
        }
    }
}
