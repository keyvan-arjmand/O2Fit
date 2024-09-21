using Common;
using MediatR;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Command.DeleteAllFoodRedis
{
    public class DeleteAllFoodRedisCommandHandler : IRequestHandler<DeleteAllFoodRedisCommand>, IScopedDependency
    {
        private readonly IRedisCacheClient _redisCacheClient;

        public DeleteAllFoodRedisCommandHandler(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        public async Task<Unit> Handle(DeleteAllFoodRedisCommand request, CancellationToken cancellationToken)
        {
            await _redisCacheClient.Db5.FlushDbAsync();

            return Unit.Value;
        }
    }
}
