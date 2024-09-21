using Domain;
using System;

namespace SocialMessaging.Domain.Entities.Market
{
    public class InternalLink : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public int AdminId { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
