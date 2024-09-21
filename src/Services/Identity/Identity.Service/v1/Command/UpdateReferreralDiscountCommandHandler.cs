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

namespace Identity.Service.v1.Command
{
    public class UpdateReferreralDiscountCommandHandler : IRequestHandler<UpdateReferreralDiscountCommand>, IScopedDependency
    {
        private readonly IRepository<User> _repository;

        public UpdateReferreralDiscountCommandHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateReferreralDiscountCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.TableNoTracking.FirstOrDefaultAsync(a => a.Id == request.userId, cancellationToken);

            if (user != null)
            {
                user.ReferreralCountBuy = user.ReferreralCountBuy <= 3 ? 0 : user.ReferreralCountBuy - 3;
                await _repository.UpdateAsync(user, cancellationToken);
                _repository.Detach(user);
            }

            return Unit.Value;
        }
    }
}
