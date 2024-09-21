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
    public class DeleteAdvertiseCountryCommandHandler : IRequestHandler<DeleteAdvertiseCountryCommand>, IScopedDependency
    {
        private readonly IRepositoryRedis<List<Advertise>> _repositoryRedis;

        public DeleteAdvertiseCountryCommandHandler(IRepositoryRedis<List<Advertise>> repositoryRedis)
        {
            _repositoryRedis = repositoryRedis;
        }

        public async Task<Unit> Handle(DeleteAdvertiseCountryCommand request, CancellationToken cancellationToken)
        {
            await _repositoryRedis.DeleteAllAsync(request.Keys);
            return Unit.Value;
        }
    }
}
