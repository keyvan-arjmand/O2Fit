using Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using User.Domain.Entities.FireBase;
using WebFramework.Api;

namespace User.API.Controllers.v1
{
    [ApiVersion("1")]
    public class UsersFirebaseTokenController : BaseController
    {
        private readonly IRepository<UsersFirebaseToken> _repository;
        public UsersFirebaseTokenController(IRepository<UsersFirebaseToken> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ApiResult> Create(UsersFirebaseToken userFirebase, CancellationToken cancellationToken)
        {
            bool AnyusersFirebase = await _repository.TableNoTracking.AnyAsync(u => u.UserId == userFirebase.UserId);
          
           
                UsersFirebaseToken userfirebasetoken = new UsersFirebaseToken()
                {
                    DeviceId = userFirebase.DeviceId,
                    FirebaseToken = userFirebase.FirebaseToken,
                    UserId = userFirebase.UserId,
                     AppVersion=userFirebase.AppVersion,
                      DeviceModel=userFirebase.DeviceModel,
                       DeviceOS=userFirebase.DeviceOS,
                        
                };
            if (!AnyusersFirebase)
            {
                await _repository.AddAsync(userfirebasetoken,cancellationToken);
               
            }
            else
            {
                var currentuser = await _repository.TableNoTracking.Where(u => u.UserId == userFirebase.UserId).FirstOrDefaultAsync();
                userfirebasetoken.Id = currentuser.Id;
                await _repository.UpdateAsync(userfirebasetoken, cancellationToken);
            }
            return Ok();
        }

       
    }
}
