using Common;
using Data.Contracts;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Service.v1.Query
{
    public class GetReferreralInviterQueryHandler : IRequestHandler<GetReferreralInviterQuery, bool>, IScopedDependency
    {
        private readonly IRepository<User> _repository;

        public GetReferreralInviterQueryHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(GetReferreralInviterQuery request, CancellationToken cancellationToken)
        {
            bool referrerall = false;

            if (request.PreviousPurchase)
            {
                referrerall = await _repository.TableNoTracking.AnyAsync(a => a.Id == request.UserId && a.ReferreralCountBuy >= 3,cancellationToken);
            }
            else
            {
                referrerall = await _repository.TableNoTracking.AnyAsync(a => a.Id == request.UserId && a.ReferreralInviter != null,cancellationToken);
            }

            return referrerall;
        }
    }
}
