using Common.Utilities;
using Data.Contracts;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.Food;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
    public interface IPersonalFoodIngredientRepository : IRepository<PersonalFoodIngredient>
    {
        Task<List<PersonalFoodIngredient>> GetIngsByFoodIdAsync(int foodId, CancellationToken cancellationToken);
    }
}
