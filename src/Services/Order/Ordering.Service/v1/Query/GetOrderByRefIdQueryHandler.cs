using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Query
{
    public class GetOrderByRefIdQueryHandler : IRequestHandler<GetOrderByRefIdQuery, Order>, ITransientDependency
    {
        private readonly IRepository<Order> _repository;

        public GetOrderByRefIdQueryHandler(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<Order> Handle(GetOrderByRefIdQuery request, CancellationToken cancellationToken)
        {
            Order order = await _repository.Table.Include(a => a.BankTransaction).Include(a => a.Package).SingleOrDefaultAsync(a => a.RefId == request.RefId);

            if (order != null)
            {
                _repository.Detach(order);

                if (order.BankTransaction == null)
                {
                    return order;
                }
            }

            return null;
        }
    }
}
