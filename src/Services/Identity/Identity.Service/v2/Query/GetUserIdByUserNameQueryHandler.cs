using Common;
using Data.Contracts;
using Domain;
using Identity.Service.v1.Query.GetUserInformation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Service.v2.Query
{
    public class GetUserIdByUserNameQueryHandler : IRequestHandler<GetUserIdByUserNameQuery, GetUserIdByUserNameViewModel>, IScopedDependency
    {
        private readonly IRepository<User> _userRepository;

        public GetUserIdByUserNameQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserIdByUserNameViewModel> Handle(GetUserIdByUserNameQuery request, CancellationToken cancellationToken)
        {
            //var result = await _userRepository.TableNoTracking.Where(u => u.NormalizedUserName== request.UserName.ToUpper())
            //    .Select(s => new GetUserIdByUserNameViewModel
            //    {
            //        UserId = s.Id,
            //    }).FirstOrDefaultAsync(cancellationToken);
            var result =
                await _userRepository.TableNoTracking.FirstOrDefaultAsync(x =>
                    x.NormalizedUserName == request.UserName.ToUpper(),cancellationToken);

            if (result !=  null) 
                return new GetUserIdByUserNameViewModel
                {
                    UserId = result.Id
                };

            return null;

        }
    }
}
