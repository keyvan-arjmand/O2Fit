using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Query
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>, IScopedDependency
    {
        private readonly IRepository<Order> _repositoryOrder;

        public GetOrderQueryHandler(IRepository<Order> repositoryOrder)
        {
            _repositoryOrder = repositoryOrder;
        }

        public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            Order order = await _repositoryOrder.Table.Include(a => a.BankTransaction).Include(a => a.Package).SingleOrDefaultAsync(a => a.Id == request.Id);

            if (order != null)
            {
                _repositoryOrder.Detach(order);
            }

            return order;
        }
    }
}
