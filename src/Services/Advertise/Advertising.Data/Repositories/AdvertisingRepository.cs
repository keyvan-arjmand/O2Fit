using Advertising.Data.Contracts;
using Advertising.Domain.Entities.Advertise;
using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advertising.Data.Repositories
{
    public class AdvertisingRepository : Repository<Advertise>, IAdvertiseRepository, IScopedDependency
    {
        public AdvertisingRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }
        public async Task<Advertise> AddAsync(Advertise entity, CancellationToken cancellationToken, bool saveNow)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;
        }
    }
}
