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
    public class UpdateExpireTimeCommandHandler : IRequestHandler<UpdateExpireTimeCommand>, IScopedDependency
    {
        private readonly IRepository<UserProfile> _repository;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedis;
        private readonly IRepository<DeviceInformation> _deviceInformationRepository;

        public UpdateExpireTimeCommandHandler(IRepository<UserProfile> repository, IRepositoryRedis<UserProfile> repositoryRedis,
            IRepository<DeviceInformation> deviceInformationRepository)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
            _deviceInformationRepository = deviceInformationRepository;
        }

        public async Task<Unit> Handle(UpdateExpireTimeCommand request, CancellationToken cancellationToken)
        {
            if (request.Tid == "D6862E20-45DB-11EB-BBAC-6133FC2CA371")
            {
                var user = await _repository.Table.Where(a => a.UserId == request.UserId).FirstOrDefaultAsync();

                if (user != null)
                {
                    var time = DateTime.Now;
                    var timeExpire = Convert.ToDateTime(request.Time);
                    user.PkExpireDate = timeExpire;

                    if (!String.IsNullOrEmpty(request.DietTime))
                    {
                        var dietTimeExpier = Convert.ToDateTime(request.DietTime);
                        user.DietPkExpireDate = dietTimeExpier;
                    }

                    if (user.PkExpireReferreralCountBuy != null)
                    {
                        var timeRefrral = Convert.ToDateTime(user.PkExpireReferreralCountBuy);
                        var days = timeRefrral.Date - DateTime.Now.Date;

                        if (days.Days > 0)
                        {
                            user.PkExpireDate = timeExpire.AddDays(days.Days);
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
            }

            return Unit.Value;
        }
    }
}
