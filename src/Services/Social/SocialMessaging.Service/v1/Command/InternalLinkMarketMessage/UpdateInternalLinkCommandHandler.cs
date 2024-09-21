using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMessaging.Domain.Entities.Market;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMessaging.Service.v1.Command.InternalLinkMarketMessage
{
    public class UpdateInternalLinkCommandHandler : IRequestHandler<UpdateInternalLinkCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<InternalLink> _internalLinkRepository;

        public UpdateInternalLinkCommandHandler(IRepository<InternalLink> internalLinkRepository)
        {
            _internalLinkRepository = internalLinkRepository;
        }

        public async Task<Unit> Handle(UpdateInternalLinkCommand request, CancellationToken cancellationToken)
        {
            var pastInternalLink = await _internalLinkRepository.TableNoTracking.Where(i => i.Id == request.Id).FirstAsync();


            pastInternalLink = new InternalLink
            {
                AdminId = request.AdminId,
                Link = request.Link,
                Name = request.Name,
                Id = pastInternalLink.Id,
                DateCreate = pastInternalLink.DateCreate
            };
            _internalLinkRepository.Detach(pastInternalLink);

            await _internalLinkRepository.UpdateAsync(pastInternalLink, cancellationToken);

            return Unit.Value;
        }
    }
}
