using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Advertising.Domain.Entities.Advertise
{
    public class Advertise : BaseEntity<int>
    {
        public int TitleId { get; set; }
        [ForeignKey(nameof(TitleId))]
        public Translation.Translation TranslationTitle { get; set; }
        public int DescriptionId { get; set; }
        [ForeignKey(nameof(DescriptionId))]
        public Translation.Translation TranslationDescription { get; set; }
        public int ShortDescriptionId { get; set; }
        [ForeignKey(nameof(ShortDescriptionId))]
        public Translation.Translation TranslationShortDescription { get; set; }
        public string Url { get; set; }
        public string BannerUri { get; set; }
        public string ImageUri { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClickCount { get; set; }
        public double ClickPrice { get; set; }
        public int ViewCount { get; set; }
        public double ViewPrice { get; set; }
        public int HintCount { get; set; }
        public double ChargeAmount { get; set; }
        public bool IsActive { get; set; }
        public ICollection<AdvertiseCountry> AdvertiseCountries { get; set; }
    }
}
