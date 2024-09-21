using Common;
using Data.Contracts;
using StackExchange.Redis;

namespace Data.Database
{
    public class RedisContext : IRedisContext, IScopedDependency
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public RedisContext(ConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            Redis = redisConnection.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}