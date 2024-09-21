using Common;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FoodStuff.Service.Services
{
    public class FastFoodService : IFastFoodService , IScopedDependency
    {
        private readonly IFastFoodsRepository _fastFoodsRepository;

        public FastFoodService(IFastFoodsRepository fastFoodsRepository)
        {
            _fastFoodsRepository = fastFoodsRepository;
        }

        public bool CreateIndex()
        {
            return _fastFoodsRepository.CreateIndex();
        }

        public bool DropIndex()
        {
            return _fastFoodsRepository.DropIndex();
        }

        public void PushSampleData()
        {
            _fastFoodsRepository.PushSampleData();
        }

        public Task<IEnumerable<FastFoods>> SearchAsync(FastFoodsInputCommand command)
        {
            command.Name = command.Name.Trim();
            return _fastFoodsRepository.SearchAsync(command);
        }

        public Task<FastFoods> GetAsync(string key)
        {
            return _fastFoodsRepository.GetAsync<FastFoods>(key);
        }

        public Task<bool> AddAsync(string docId, FastFoods fastFoods)
        {

            var docDic = CastEntityToDict(fastFoods);

            return _fastFoodsRepository.AddAsync(docId, docDic);
        }

        public Task<bool> DeleteAsync(string docId)
        {
            return _fastFoodsRepository.DeleteAsync(docId);
        }

        public Task<bool> UpdateAsync(string docId, FastFoods fastFoods)
        {
            var docDic = CastEntityToDict(fastFoods);

            return _fastFoodsRepository.UpdateAsync(docId, docDic);
        }

        private Dictionary<string, object> CastEntityToDict<T>(T data) where T : class
        {
            return data.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .ToDictionary(prop => prop.Name, prop => prop.GetValue(data, null));
        }
    }
}
