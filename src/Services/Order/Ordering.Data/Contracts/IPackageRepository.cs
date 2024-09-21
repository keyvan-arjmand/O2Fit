using Data.Contracts;
using Ordering.Domain.Entities.Package;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Data.Contracts
{
   public interface IPackageRepository:IRepository<Package>
    {
        Task<Package> GetById(int id, CancellationToken cancellationToken);
    }
}
