using Data.Contracts;
using Ordering.Domain.Entities.Order;
using Ordering.Domain.Entities.Package;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Data.Contracts
{
   public interface IOrderRepository : IRepository<Order>
    {
        Task<int> AddCafeBazarAsync(Order order, CancellationToken cancellationToken, bool saveNow = true);
    }
}
