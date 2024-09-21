using MediatR;
using SocialMessaging.Domain.Enum;
using SocialMessaging.Service.ViewModels;

namespace SocialMessaging.Service.v1.Query
{
    public class GetAppVersionQuery : IRequest<AppVersionViewModel>
    {
        public MarketType MarketType { get; set; }
        public string AppVersion { get; set; }
        public TranslationDto Description { get; set; }

    }
}
