using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Ordering.Data.Contracts;
using Ordering.Domain.Entities.Order;
using Ordering.Domain.Entities.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Data.Repositories
{
   public class OrderRepository:Repository<Order>,IOrderRepository, IScopedDependency
    {
        public OrderRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }

        public virtual async Task<int> AddCafeBazarAsync(Order order, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(order, nameof(order));
            await Entities.AddAsync(order, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return order.Id;
        }
    }
}
