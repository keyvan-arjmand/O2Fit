using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Interfaces.Services;
using Market.Domain.Aggregates.MarketMessageAggregate;

namespace Market.Application.MarketMessages.V1.Commands.UpdateMarketMessage;

public class UpdateMarketMessageCommandHandler : IRequestHandler<UpdateMarketMessageCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IFileService _fileService;

    public UpdateMarketMessageCommandHandler(IUnitOfWork work, IFileService fileService)
    {
        _work = work;
        _fileService = fileService;
    }

    public async Task Handle(UpdateMarketMessageCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<MarketMessage>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException("Message not found");
        result.Version = request.Version;
        result.Title.Arabic = request.Title.Arabic;
        result.Title.English = request.Title.English;
        result.Title.Persian = request.Title.Persian;
        result.Description.Arabic = request.Description.Arabic;
        result.Description.English = request.Description.English;
        result.Description.Persian = request.Description.Persian;
        result.ButtonName.Arabic = request.ButtonName.Arabic;
        result.ButtonName.English = request.ButtonName.English;
        result.ButtonName.Persian = request.ButtonName.Persian;
        result.StartDate = request.StartDate;
        result.EndDate = request.EndDate;
        result.Target = request.Target;
        result.Postpone = request.Postpone;
        result.Link = request.Link;
        if (!string.IsNullOrWhiteSpace(request.ImageUri))
        {
            result.ImageName = _fileService.AddImage(request.ImageUri, "MarketMessage",
                Guid.NewGuid().ToString().Substring(0, 6));
        }

        await _work.GenericRepository<MarketMessage>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<MarketMessage, object>>[]
                {
                    x => x.Link,
                    x => x.Description.Persian,
                    x => x.Description.Arabic,
                    x => x.Description.English,
                    x => x.ButtonName.Persian,
                    x => x.ButtonName.Arabic,
                    x => x.ButtonName.English,
                    x => x.Title.Persian,
                    x => x.Title.Arabic,
                    x => x.Title.English,
                    x => x.Link,
                    x => x.Version,
                    x => x.Postpone,
                    x => x.Target,
                    x => x.EndDate,
                    x => x.StartDate,
                }, null, cancellationToken);
        // await _cacheService.CacheResponseAsync(appVersion., appVersion, TimeSpan.FromMinutes(5));
    }
}