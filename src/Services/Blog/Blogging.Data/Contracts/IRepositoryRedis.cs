using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Data.Contracts
{
    public interface IRepositoryRedis<TData>
    {
        Task<TData> GetAsync(string key);
        Task<List<TData>> GetAllAsync(List<string> keys);
        Task<TData> UpdateAsync(string key, TData entity,TimeSpan? expierTime=null);
        Task UpdateAllAsync(KeyValuePair<RedisKey, RedisValue>[] keyValuePairs);
        Task DeleteAsync(string key);
        Task DeleteAllAsync(List<string> keys);
    }
}
