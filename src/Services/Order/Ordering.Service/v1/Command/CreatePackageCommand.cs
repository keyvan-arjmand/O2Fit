using MediatR;
using Ordering.Domain.Entities.Package;
using Ordering.Domain.Models;

namespace Ordering.Service.v1.Command
{
    public class CreatePackageCommand : IRequest<Package>
    {
        public CreatePackageDTO createPackageDTO { get; set; }
    }
}
