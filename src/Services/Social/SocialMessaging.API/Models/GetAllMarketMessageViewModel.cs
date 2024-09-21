using SocialMessaging.Domain.Enum;
using System;

namespace SocialMessaging.API.Models
{
    public class GetAllMarketMessageViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ButtonName { get; set; }

        public string Link { get; set; }
        public string Image { get; set; }
        public TargetMarketMessage Target { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Postpone { get; set; }
        public int AdminId { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
