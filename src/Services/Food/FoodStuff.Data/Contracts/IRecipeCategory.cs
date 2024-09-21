using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
    public interface IRecipeCategory : IRepository<RecipeCategore>
    {
        void Add(RecipeCategore entity, bool saveNow = true);
        Task AddAsync(RecipeCategore entity, CancellationToken cancellationToken, bool saveNow = true);
        void Delete(RecipeCategore entity, bool saveNow = true);
        Task DeleteAsync(RecipeCategore entity, CancellationToken cancellationToken, bool saveNow = true);
        void Update(RecipeCategore entity, bool saveNow = true);
        Task UpdateAsync(RecipeCategore entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<RecipeCategore> GetByIdAsync(CancellationToken cancellationToken, int ids);
    }
}
