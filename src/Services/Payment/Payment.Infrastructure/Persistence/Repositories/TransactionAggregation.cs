using Payment.Application.Dtos;

namespace Payment.Infrastructure.Persistence.Repositories;

public class TransactionAggregation
    // :GenericRepository<TransactionDietPackage>,ITransactionAggregation
{
    // public TransactionAggregation(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService) : base(mediator, configuration, currentUserService)
    // {
    // }
    //
    // public async Task<TransactionAggregationDto> GetSingleDocumentByFilterAsync(FilterDefinition<TransactionDietPackage> filter, CancellationToken cancellationToken = default)
    // {
    //     var docs = await Collection.Aggregate()
    //         .Match(filter)
    //         .Lookup("packages", "_id", "PackageId", "Packages")
    //         .Lookup("PackageAdvertises", "_id", "PackageAdvertiseId", "PackageAdvertises")
    //         .FirstOrDefaultAsync(cancellationToken);
    //     
    //     return BsonSerializer.Deserialize<TransactionAggregationDto>(docs);
    // }
}