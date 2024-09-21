using Ordering.Domain.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Data.Contracts
{
    public interface ITakhfifanService
    {
        [Post("/track/purchase")]
        [Headers("Authorization: Basic")]
        Task purchase(TakhfifanDto takhfifanDto);
    }
}
