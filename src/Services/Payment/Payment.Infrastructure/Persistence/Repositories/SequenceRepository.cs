using MassTransit;
using Microsoft.Extensions.Options;
using Payment.Domain.Aggregates.SequenceAggregate;
using System.Threading;

namespace Payment.Infrastructure.Persistence.Repositories;

public class SequenceRepository : GenericRepository<Sequence>,ISequenceRepository
{
    public SequenceRepository(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService) : base(mediator, configuration, currentUserService)
    {
    }
    public async Task<long> BankOrderGeneration(CancellationToken cancellationToken = default)
    {
        var filter =
            Builders<Sequence>.Filter.Eq(x => x.Name, "orderSequence");
        var seq = await Collection.Find(filter).Limit(1).FirstOrDefaultAsync(cancellationToken);
        var update = Builders<Sequence>.Update.Inc(a => a.Value, 1);
        // await UpdateOneAsync(filter, seq, update, null, cancellationToken);
        await Collection.UpdateOneAsync(filter, update, null, cancellationToken).ConfigureAwait(false);
        return seq.Value;
    }

    // public async Task<UpdateResult> UpdateOneAsync(FilterDefinition<Sequence> filter, Sequence aggregateRoot,
    //     UpdateDefinition<Sequence> update, UpdateOptions? options = null,
    //     CancellationToken cancellationToken = default)
    // {
    //     return await Collection.UpdateOneAsync(filter, update, options, cancellationToken).ConfigureAwait(false);
    // }

    private string ToLowerFirsWord(string word)
    {
        word = word.Replace(word.Substring(0, 1), word.Substring(0, 1).ToLower());
        return word;
    }

  
}