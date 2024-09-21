using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Advertising.Domain.Entities.Advertise;

namespace Advertising.Domain.Entities.Advertise
{
    public class AdvertiseCountry : BaseEntity<int>
    {
        public int AdvertiseId { get; set; }
        [ForeignKey(nameof(AdvertiseId))]
        public Advertise Advertise { get; set; }
        public int CountryId { get; set; }
    }
}
