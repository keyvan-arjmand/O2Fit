using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Interfaces.Services;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.MarketMessageAggregate;

namespace Market.Application.MarketMessages.V1.Commands.InsertMarketMessage;

public class InsertMarketMessageCommandHandler : IRequestHandler<InsertMarketMessageCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IFileService _fileService;

    public InsertMarketMessageCommandHandler(IUnitOfWork work, IFileService fileService)
    {
        _work = work;
        _fileService = fileService;
    }

    public async Task Handle(InsertMarketMessageCommand request, CancellationToken cancellationToken)
    {
        var marketMessage = new MarketMessage
        {
            Target = request.Target,
            Title = request.Title.MapTo<TranslationMarketMessage, TranslationDto>(),
            Description = request.Title.MapTo<TranslationMarketMessage, TranslationDto>(),
            ButtonName = request.Title.MapTo<TranslationMarketMessage, TranslationDto>(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Link = request.Link,
            Version = request.Version,
            Postpone = request.Postpone,
        };
        if (!string.IsNullOrWhiteSpace(request.ImageUri))
        {
            marketMessage.ImageName = _fileService.AddImage(request.ImageUri, "MarketMessage",
                Guid.NewGuid().ToString().Substring(0, 6));
        }

        await _work.GenericRepository<MarketMessage>().InsertOneAsync(marketMessage, null, cancellationToken);
    }
}