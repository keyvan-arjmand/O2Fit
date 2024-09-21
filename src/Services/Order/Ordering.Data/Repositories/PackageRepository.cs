using Common;
using Data.Database;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Ordering.Data.Contracts;
using Ordering.Domain.Entities.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Data.Repositories
{
   public class PackageRepository:Repository<Package>,IPackageRepository, IScopedDependency
    {
        public PackageRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }

        public async Task<Package> GetById(int id, CancellationToken cancellationToken)
        {
            Package package = await Table.Include(n => n.TranslationName).Include(d => d.TranslationDescription).Where(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
            return package;
        }
    }
}
