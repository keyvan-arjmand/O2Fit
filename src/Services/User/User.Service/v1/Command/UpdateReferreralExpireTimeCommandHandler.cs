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

namespace User.Service.v1.Command
{
    public class UpdateReferreralExpireTimeCommandHandler : IRequestHandler<UpdateReferreralExpireTimeCommand>, IScopedDependency
    {
        private readonly IRepository<UserProfile> _repository;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedis;

        public UpdateReferreralExpireTimeCommandHandler(IRepository<UserProfile> repository, IRepositoryRedis<UserProfile> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<Unit> Handle(UpdateReferreralExpireTimeCommand request, CancellationToken cancellationToken)
        {
            if (request.Tid == "FCBA1B18-588E-11EB-A22F-0D43FC2CA371")
            {
                var user = await _repository.Table.Where(a => a.UserId == request.UserId).FirstOrDefaultAsync();

                if (user != null)
                {
                    if (user.PkExpireDate == null)
                    {
                        user.PkExpireDate = DateTime.Now.AddDays(30);
                    }
                    else
                    {
                        var time = (DateTime)user.PkExpireDate;
                        user.PkExpireDate = time.AddDays(30);
                    }

                    user.PkExpireReferreralCountBuy = user.PkExpireDate;

                    await _repository.UpdateAsync(user, cancellationToken);
                    _repository.Detach(user);

                    await _repositoryRedis.UpdateAsync($"UserProfile_{user.UserId}", user);
                }
            }

            return Unit.Value;
        }
    }
}
