using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
    public interface IFoodRepository:IRepository<Food>
    {
        Task<int> AddAsync(Food food, CancellationToken cancellationToken, bool saveNow = true);
        Task<Food> GetByIdAsync(CancellationToken cancellationToken, int ids);
        Task<Food> GetByFoodCodeAsync(CancellationToken cancellationToken, long foodCode);
        IQueryable<Food> FindByParameters(FoodInputParameters Parameter,string LanguageName, CancellationToken cancellationToken);
        IQueryable<Food> FindByCondition(Expression<Func<Food, bool>> expression, CancellationToken cancellationToken);
        IQueryable<Food> FindByConditionTracking(Expression<Func<Food, bool>> expression, CancellationToken cancellationToken);
    }
}
