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
    public interface IBrandRepository:IRepository<Brand>
    {
        Task<Brand> AddAsync(Brand brand, bool saveNow = true);
    }
}
