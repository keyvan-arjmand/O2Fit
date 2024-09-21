using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Service.v2.Command
{
    public class AddConfirmationCodeCommandHandler : IRequestHandler<AddConfirmationCodeCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<User> _userRepository;

        public AddConfirmationCodeCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(AddConfirmationCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Table.FirstOrDefaultAsync(x =>
                x.NormalizedUserName == request.Username.ToUpper(), cancellationToken);

            if (user == null)
                return Unit.Value;
            
            user.ConfirmCode = request.ConfirmCode;
            user.ConfirmCodeExpireTime = request.ConfirmCodeExpireTime;

            await _userRepository.UpdateAsync(user,cancellationToken);

            return Unit.Value;
        }
    }
}