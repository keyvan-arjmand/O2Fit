using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Domain.Aggregates.MarketMessageAggregate;

namespace Market.Application.MarketMessages.V1.Commands.SoftDeleteMarketMessage;

public class SoftDeleteMarketMessageCommandHandler:IRequestHandler<SoftDeleteMarketMessageCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteMarketMessageCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteMarketMessageCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<MarketMessage>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"MarketMessage not found {request.Id}");
        result.IsDelete = true;
        await _work.GenericRepository<MarketMessage>()
            .SoftDeleteByIdAsync(request.Id, result, null, cancellationToken);
    }
}