using Advertising.Domain.Entities.Advertise;
using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advertising.Data.Contracts
{
   public interface IAdvertiseRepository : IRepository<Advertise>
    {
        Task<Advertise> AddAsync(Advertise entity, CancellationToken cancellationToken, bool saveNow = true);
    }
}
