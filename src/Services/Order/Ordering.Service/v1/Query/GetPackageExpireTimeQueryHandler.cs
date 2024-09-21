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
    public class GetPackageExpireTimeQueryHandler : IRequestHandler<GetPackageExpireTimeQuery, string>, IScopedDependency
    {
        private readonly IRepository<Order> _repository;
        private readonly IRepositoryRedis<Order> _repositoryRedis;

        public GetPackageExpireTimeQueryHandler(IRepository<Order> repository, IRepositoryRedis<Order> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<string> Handle(GetPackageExpireTimeQuery request, CancellationToken cancellationToken)
        {
            Order order = await _repositoryRedis.GetAsync($"Order_User_{request.UserId}");

            string _expire = null;

            if (order != null)
            {
                _expire = order.ExpireTime.ToString();
            }
            else
            {
                order = await _repository.Table.Include(a => a.BankTransaction).Where(a => a.BankTransaction != null && a.UserId == request.UserId).OrderByDescending(a => a.CreateDate).SingleOrDefaultAsync(cancellationToken);
                
                if (order != null)
                {
                    _repository.Detach(order);
                    _expire = order.ExpireTime.ToString();
                    await _repositoryRedis.UpdateDisableLoopAsync($"Order_User_{request.UserId}", order);
                }
            }

            return _expire;
        }
    }
}
