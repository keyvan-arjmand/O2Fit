﻿using Advertising.API.Models;
using Advertising.Domain.Entities.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advertise.API.Models
{
    public class AdvertiseSelectDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public string BannerUri { get; set; }
        public string ImageUri { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public int ClickCount { get; set; }
        //public double ClickPrice { get; set; }
        //public int ViewCount { get; set; }
        //public double ViewPrice { get; set; }
        //public int HintCount { get; set; }
        //public double ChargeAmount { get; set; }
        //public bool IsActive { get; set; }
        //public List<int> CountrieIds { get; set; }
    }
}
