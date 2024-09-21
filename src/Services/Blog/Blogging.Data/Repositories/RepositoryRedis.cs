using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Common;
using Data.Contracts;
using Data.Database;
using Newtonsoft.Json;
using System.Linq;
using FluentValidation.Results;

namespace Data.Repositories
{
    public class RepositoryRedis<TData> : IRepositoryRedis<TData>, IScopedDependency
    {
        private readonly IRedisContext _context;

        public RepositoryRedis(IRedisContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TData> GetAsync(string key)
        {
            var _values = await _context.Redis.StringGetAsync(key);

            if (_values.IsNullOrEmpty)
            {
                return default(TData);
            }

            return JsonConvert.DeserializeObject<TData>(_values);
        }

        public async Task<List<TData>> GetAllAsync(List<string> keys)
        {
            RedisKey[] allKeys = new RedisKey[keys.Count];

            for (int i = 0; i < keys.Count; i++)
            {
                allKeys[i] = keys[i];
            }

            RedisValue[] _values = await _context.Redis.StringGetAsync(allKeys);

            string[] _data = _values.ToStringArray();

            List<TData> _json = new List<TData>();

            foreach (var item in _data)
            {
                if (item != null)
                {
                    var _get = JsonConvert.DeserializeObject<TData>(item);
                    _json.Add(_get);
                }
            }

            return _json;
        }

        public async Task<TData> UpdateAsync(string key, TData entity,TimeSpan? expierTime=null)
        {
            var _values = await _context.Redis.StringSetAsync(key, JsonConvert.SerializeObject(entity), expierTime);

            if (!_values)
            {
                return default(TData);
            }

            return await GetAsync(key);
        }

        public async Task UpdateAllAsync(KeyValuePair<RedisKey, RedisValue>[] keyValuePairs)
        {
            await _context.Redis.StringSetAsync(keyValuePairs, When.Always, CommandFlags.None);
        }

        public async Task DeleteAsync(string key)
        {
            await _context.Redis.KeyDeleteAsync(key);
        }

        public async Task DeleteAllAsync(List<string> keys)
        {
            RedisKey[] allKeys = new RedisKey[keys.Count];

            for (int i = 0; i < keys.Count; i++)
            {
                allKeys[i] = keys[i];
            }

            await _context.Redis.KeyDeleteAsync(allKeys);
        }
    }
}
