using System.Threading;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Domain.Aggregates.SequenceAggregate;

namespace Payment.Application.Common.Utilities;

public class GenerationBankOrder
{
    public static async Task<long> BankOrderGeneration(IUnitOfWork uow)
    {
        var filter = Builders<Sequence>.Filter.Eq(x => x.Name, "OrderSequence");
        var seq = await uow.GenericRepository<Sequence>()
            .GetSingleDocumentByFilterAsync(filter);
        var update = Builders<Sequence>.Update.Inc(a => a.Value, 1);
        var oid = await uow.GenericRepository<Sequence>()
            .UpdateOneAsync(filter, seq, update, null);
        return seq.Value;
    }
}