using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMessaging.Domain.Entities.App;
using SocialMessaging.Service.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMessaging.Service.v1.Query
{
    public class GetLastAppVersionQueryHandler : IRequestHandler<GetAppVersionQuery, AppVersionViewModel>, IScopedDependency
    {
        private readonly IRepository<AppVersion> _appVersionRepository;

        public GetLastAppVersionQueryHandler(IRepository<AppVersion> appVersionRepository)
        {
            _appVersionRepository = appVersionRepository;
        }

        public async Task<AppVersionViewModel> Handle(GetAppVersionQuery request, CancellationToken cancellationToken)
        {
            var result = await _appVersionRepository.TableNoTracking.Include(m => m.appVersionMarketTypes)
                .Where(a => a.Version == request.AppVersion &&
                a.appVersionMarketTypes.Any(c => c.MarketType == request.MarketType))
                 .Select(s => new AppVersionViewModel
                 {
                     IsForced = s.IsForced,
                     Link = s.appVersionMarketTypes
                     .Where(a => a.AppVersionId == s.Id && a.MarketType == request.MarketType)
                     .Select(l => l.Link).First()
                 }).FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
