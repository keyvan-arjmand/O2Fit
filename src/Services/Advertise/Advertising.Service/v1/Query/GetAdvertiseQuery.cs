using Advertising.Domain.Entities.Advertise;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Advertising.Service.v1.Query
{
    public class GetAdvertiseQuery : IRequest<AdSelect>
    {
        public int CountryId { get; set; }
        public string Language { get; set; }
    }

    public class AdSelect
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public string BannerUri { get; set; }
        public string ImageUri { get; set; }
    }
}
