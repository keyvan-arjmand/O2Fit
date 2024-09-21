using Advertising.Domain.Entities.Advertise;
using Common;
using Data.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advertising.Service.v1.Command
{
    public class DeleteAdvertiseCommandHandler : IRequestHandler<DeleteAdvertiseCommand>, IScopedDependency
    {
        private readonly IRepositoryRedis<Advertise> _repositoryRedis;

        public DeleteAdvertiseCommandHandler(IRepositoryRedis<Advertise> repositoryRedis)
        {
            _repositoryRedis = repositoryRedis;
        }

        public async Task<Unit> Handle(DeleteAdvertiseCommand request, CancellationToken cancellationToken)
        {
            await _repositoryRedis.DeleteAsync($"Ads_{request.Id}");
            return Unit.Value;
        }
    }
}
