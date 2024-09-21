using Common;
using Data.Contracts;
using MediatR;
using Ordering.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Command
{
    public class UpdateMelatRefIdCommandHandler : IRequestHandler<UpdateMelatRefIdCommand>, IScopedDependency
    {
        private readonly IRepository<Order> _repository;

        public UpdateMelatRefIdCommandHandler(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateMelatRefIdCommand request, CancellationToken cancellationToken)
        {
            request.Order.RefId = request.RefId;
            await _repository.UpdateAsync(request.Order, cancellationToken);
            _repository.Detach(request.Order);
            return Unit.Value;
        }
    }
}
