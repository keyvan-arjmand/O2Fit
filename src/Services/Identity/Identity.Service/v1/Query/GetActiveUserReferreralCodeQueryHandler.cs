using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Service.v1.Query
{
    public class GetActiveUserReferreralCodeQueryHandler : IRequestHandler<GetActiveUserReferreralCodeQuery, int>, IScopedDependency
    {
        private readonly IRepository<User> _repository;

        public GetActiveUserReferreralCodeQueryHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetActiveUserReferreralCodeQuery request, CancellationToken cancellationToken)
        {
            var count = await _repository.TableNoTracking.FirstOrDefaultAsync(a => a.Id == request.UserId);
            _repository.Detach(count);
            int value = count.ReferreralCountBuy;
            return value;
        }
    }
}
