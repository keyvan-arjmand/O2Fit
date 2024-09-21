using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Domain;
using Identity.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Service.v1.Command
{
    public class UpdateReferreralCountCommandHandler : IRequestHandler<UpdateReferreralCountCommand>, IScopedDependency
    {
        private readonly IRepository<User> _repository;
        private readonly IUserProfileService _userProfileService;

        public UpdateReferreralCountCommandHandler(IRepository<User> repository, IUserProfileService userProfileService)
        {
            _repository = repository;
            _userProfileService = userProfileService;
        }

        public async Task<Unit> Handle(UpdateReferreralCountCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.TableNoTracking.FirstOrDefaultAsync(
                a => a.Id == request.UserId, cancellationToken);
            _repository.Detach(user);

            if (user != null && user.ReferreralInviter != null)
            {
                var userInviter = await _repository.TableNoTracking.FirstOrDefaultAsync(a =>
                        a.ReferreralCode.ToLower() == user.ReferreralInviter.ToLower(), cancellationToken);

                if (userInviter != null)
                {
                    _repository.Detach(userInviter);
                    userInviter.ReferreralCountBuy = userInviter.ReferreralCountBuy + 1;

                    if (userInviter.ReferreralCountBuy >= 3)
                    {
                        userInviter.ReferreralCountBuy = userInviter.ReferreralCountBuy <= 3 ? 0 : userInviter.ReferreralCountBuy - 3;
                        
                       await _userProfileService.UpdateExpireTimeUser(userInviter.Id, "FCBA1B18-588E-11EB-A22F-0D43FC2CA371");
                    }

                    await _repository.UpdateAsync(userInviter, cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
