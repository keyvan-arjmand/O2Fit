using MediatR;
using Ordering.Domain.Enum;
using Ordering.Service.v1.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.v2.Query.GetPackages
{
    public class GetPackageV2Query : IRequest<List<PackageCurrency>>
    {
        public string LanguageName { get; set; }
    }
}
