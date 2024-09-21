using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
    public interface IFastFoodService
    {
        Task<IEnumerable<FastFoods>> SearchAsync(FastFoodsInputCommand command);
        bool CreateIndex();
        bool DropIndex();
        void PushSampleData();
        Task<FastFoods> GetAsync(string key);
        Task<bool> AddAsync(string docId, FastFoods fastFoods);
        Task<bool> DeleteAsync(string docId);
        Task<bool> UpdateAsync(string docId, FastFoods fastFoods);
    }
}
