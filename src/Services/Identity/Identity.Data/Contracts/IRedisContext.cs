using StackExchange.Redis;

namespace Data.Contracts
{
    public interface IRedisContext
    {
        IDatabase Redis{get;}
    }
}
