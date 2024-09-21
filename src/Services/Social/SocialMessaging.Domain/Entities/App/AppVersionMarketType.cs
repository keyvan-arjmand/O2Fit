using Domain;
using SocialMessaging.Domain.Enum;

namespace SocialMessaging.Domain.Entities.App
{
    public class AppVersionMarketType : BaseEntity<int>
    {
        public int AppVersionId { get; set; }
        public AppVersion AppVersion { get; set; }

        public MarketType MarketType { get; set; }

        public string Link { get; set; }
    }
}
