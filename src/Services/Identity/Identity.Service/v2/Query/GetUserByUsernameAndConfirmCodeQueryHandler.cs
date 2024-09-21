using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Data.Contracts;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Identity.Service.v2.Query
{
    public class GetUserByUsernameAndConfirmCodeQueryHandler : IRequestHandler<GetUserByUsernameAndConfirmCodeQuery , UserConfirmDto>, IScopedDependency
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public GetUserByUsernameAndConfirmCodeQueryHandler(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserConfirmDto> Handle(GetUserByUsernameAndConfirmCodeQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.TableNoTracking.FirstOrDefaultAsync(
                x => x.NormalizedUserName == request.Username.ToUpper() && x.ConfirmCode == request.Code, cancellationToken);
            if (user == null)
                return null;

            return _mapper.Map<User, UserConfirmDto>(user);
        }
    }
}