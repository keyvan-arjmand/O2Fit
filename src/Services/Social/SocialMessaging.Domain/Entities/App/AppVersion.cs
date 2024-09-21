using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMessaging.Domain.Entities.App
{
    public class AppVersion : BaseEntity<int>
    {
        public int AdminId { get; set; }
        public DateTime DateCreate { get; set; }

        public string Version { get; set; }

        public int DescId { get; set; }
        [ForeignKey(nameof(DescId))]
        public Translation.TranslationDto Description { get; set; }

        public bool IsForced { get; set; }

        public ICollection<AppVersionMarketType> appVersionMarketTypes { get; set; }
    }
}
