using Common;
using Data.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NRediSearch;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStuff.Data.Repositories
{
    public abstract class BaseCacheRepository
    {
        protected ConnectionMultiplexer _redisConnection;
        protected Client _client;
        public IDatabase Database { get; }

        public BaseCacheRepository(ConnectionMultiplexer redisConnection ,string indexName)
        {
            _redisConnection = redisConnection;
            Database = _redisConnection.GetDatabase();
            _client = new Client(indexName, Database);
        }

        public async Task<bool> AddAsync(string docId, Dictionary<string, dynamic> docDic)
        {
            var score = docDic.FirstOrDefault(x => x.Key == "Score").Value ?? 1;

            Dictionary<string, RedisValue> dictDocRedis = new Dictionary<string, RedisValue>();

            foreach (var item in docDic)
            {
                dictDocRedis.Add(item.Key, item.Value ?? "");
            }

            return await _client.AddDocumentAsync(docId, dictDocRedis, score);
        }

        public T Get<T>(string docId) where T : class
        {
            var retObj = _client.GetDocument(docId);

            var jsonRedisItem = JsonConvert.SerializeObject(retObj.GetProperties());
            var jsonResObj = JsonConvert.DeserializeObject<T>(jsonRedisItem);

            return jsonResObj;
        }

        public async Task<T> GetAsync<T>(string docId) where T : class
        {
            var retObj = await _client.GetDocumentAsync(docId);

            var jsonRedisItem = JsonConvert.SerializeObject(retObj.GetProperties());
            var jsonResObj = JsonConvert.DeserializeObject<T>(jsonRedisItem);

            return jsonResObj;
        }

        public bool Add(string docId, Dictionary<string, dynamic> docDic)
        {
            var score = docDic.FirstOrDefault(x => x.Key == "Score").Value ?? 1;

            Dictionary<string, RedisValue> dictDocRedis = new Dictionary<string, RedisValue>();

            foreach (var item in docDic)
            {
                dictDocRedis.Add(item.Key, item.Value ?? "");
            }

            return _client.AddDocument(docId, dictDocRedis, score);
        }

        public bool Delete(string docId)
        {
            return _client.DeleteDocument(docId);
        }

        public async Task<bool> DeleteAsync(string docId)
        {
            return await _client.DeleteDocumentAsync(docId);
        }

        public bool Update(string docId, Dictionary<string, dynamic> docDic)
        {
            var score = docDic.FirstOrDefault(x => x.Key == "Score").Value ?? 1;

            Dictionary<string, RedisValue> dictDocRedis = new Dictionary<string, RedisValue>();

            foreach (var item in docDic)
            {
                dictDocRedis.Add(item.Key, item.Value ?? "");
            }

            return _client.UpdateDocument(docId, dictDocRedis, score);
        }

        public async Task<bool> UpdateAsync(string docId, Dictionary<string, dynamic> docDic)
        {
            var score = docDic.FirstOrDefault(x => x.Key == "Score").Value ?? 1;

            Dictionary<string, RedisValue> dictDocRedis = new Dictionary<string, RedisValue>();

            foreach (var item in docDic)
            {
                dictDocRedis.Add(item.Key, item.Value ?? "");
            }

            return await _client.UpdateDocumentAsync(docId, dictDocRedis, score);
        }

        protected List<T> CastRedisValues<T>(List<Document> docList) where T : class
        {
            List<T> newDoc = new List<T>();

            foreach (var item in docList)
            {
                var jsonRedisItem = JsonConvert.SerializeObject(item.GetProperties());
                JObject jItemObj = JObject.Parse(jsonRedisItem);
                jItemObj["Id"] = item.Id;
                T castJsonObj = jItemObj.ToObject<T>();

                newDoc.Add(castJsonObj);
            }

            return newDoc;
        }

        public abstract bool CreateIndex();
        public bool DropIndex()
        {
            return _client.DropIndex();
        }
    }

}
