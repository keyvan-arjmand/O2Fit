using Payment.Domain.Aggregates.SequenceAggregate;

namespace Payment.Application.Common.Interfaces.Persistence.Repositories;

public interface ISequenceRepository: IGenericRepository<Sequence>
{
    public Task<long> BankOrderGeneration(CancellationToken cancellationToken = default);
}