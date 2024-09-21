using Domain;
using SocialMessaging.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMessaging.Domain.Entities.Market
{
    public class MarketMessage : BaseEntity<int>
    {
        public int TitleId { get; set; }
        [ForeignKey(nameof(TitleId))]
        public Translation.TranslationDto Title { get; set; }

        public int DescriptionId { get; set; }
        [ForeignKey(nameof(DescriptionId))]
        public Translation.TranslationDto Description { get; set; }

        public int ButtonNameId { get; set; }
        [ForeignKey(nameof(ButtonNameId))]
        public Translation.TranslationDto ButtonName { get; set; }

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
