using MediatR;
using Ordering.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v1.Query
{
    public class GetPackageQuery : IRequest<List<PackageCurrency>>
    {
        public string LanguageName { get; set; }
    }

    public class PackageCurrency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? DiscountPercent { get; set; }
        public int Duration { get; set; }
        public Currency Currency { get; set; }
        public PackageType PackageType { get; set; }
        //for promote 
        public bool IsPromote { get; set; }
    }
}
