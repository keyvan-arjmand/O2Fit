using MediatR;
using Ordering.Domain.Entities.Package;
using System.Collections.Generic;

namespace Ordering.Service.v1.Query.GetAllPackageAdmin
{
    public class GetAllPackageAdminQuery : IRequest<List<Package>>
    {
        public string LanguageName { get; set; }
    }
}
