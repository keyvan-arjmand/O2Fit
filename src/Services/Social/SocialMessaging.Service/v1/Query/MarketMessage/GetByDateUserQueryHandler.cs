using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMessaging.Service.ViewModels.MarketMessage;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMessaging.Service.v1.Query.MarketMessage
{
    public class GetByDateUserQueryHandler : IRequestHandler<GetByDateUserQuery, GetByDateUserViewModel>,
        IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Market.MarketMessage> _marketMessageRepository;

        public GetByDateUserQueryHandler(IRepository<Domain.Entities.Market.MarketMessage> marketMessageRepository)
        {
            _marketMessageRepository = marketMessageRepository;
        }

        public async Task<GetByDateUserViewModel> Handle(GetByDateUserQuery request,
            CancellationToken cancellationToken)
        {
            var today = DateTime.Now;
            return await _marketMessageRepository.TableNoTracking.Include(b => b.ButtonName)
                .Include(t => t.Title).Include(d => d.Description)
                .Where(m => m.StartDate.Date <= today.Date && m.EndDate.Date >= today.Date)
                .Select(s => new GetByDateUserViewModel
                {
                    Id = s.Id,
                    ButtonName = s.ButtonName,
                    Description = s.Description,
                    Image = s.Image,
                    Link = s.Link,
                    Postpone = s.Postpone,
                    Title = s.Title,
                }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}