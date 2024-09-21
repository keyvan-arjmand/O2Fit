using Common;
using Data.Contracts;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.Entities.User;

namespace User.Service.v1.Command
{
    public class AdminExpireTimeUserCommandHandler : IRequestHandler<AdminExpireTimeUserCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<UserProfile> _repository;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedis;
        private readonly IRepository<DeviceInformation> _deviceInformationRepository;

        public AdminExpireTimeUserCommandHandler(IRepository<UserProfile> repository, IRepositoryRedis<UserProfile> repositoryRedis, IRepository<DeviceInformation> deviceInformationRepository)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
            _deviceInformationRepository = deviceInformationRepository;
        }

        public async Task<Unit> Handle(AdminExpireTimeUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _repository.Table.Where(a => a.UserId == request.UserId).FirstOrDefaultAsync();

            if (user != null)
            {

                user.PkExpireDate = request.ExpireDate;
                user.DietPkExpireDate = request.ExpireDate;


                if (user.PkExpireReferreralCountBuy != null)
                {
                    var timeRefrral = Convert.ToDateTime(user.PkExpireReferreralCountBuy);
                    var days = timeRefrral.Date - DateTime.Now.Date;

                    if (days.Days > 0)
                    {
                        user.PkExpireDate = request.ExpireDate.AddDays(days.Days);
                    }
                }
                await _repository.UpdateAsync(user, cancellationToken);
                _repository.Detach(user);

                await _repositoryRedis.UpdateAsync($"UserProfile_{user.UserId}", user);


                var device = await _deviceInformationRepository.Table.Where(d => d.UserId == request.UserId)
                    .OrderByDescending(d => d.Id).FirstOrDefaultAsync(cancellationToken);

                if (device != null)
                {
                    device.IsPurchase = true;
                    await _deviceInformationRepository.UpdateAsync(device, cancellationToken);
                }
            }


            return Unit.Value;
        }
    }
}
