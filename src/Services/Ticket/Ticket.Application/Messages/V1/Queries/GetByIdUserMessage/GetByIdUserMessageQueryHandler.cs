using MongoDB.Bson;
using Ticket.Application.Common.Interfaces.Persistence.UoW;
using Ticket.Application.Common.Mapping;
using Ticket.Application.Dtos;
using Ticket.Domain.Aggregates.MessageAggregate;

namespace Ticket.Application.Messages.V1.Queries.GetByIdUserMessage;

public class GetByIdUserMessageQueryHandler : IRequestHandler<GetByIdUserMessageQuery, MessageDto>
{
    private readonly IUnitOfWork _work;

    public GetByIdUserMessageQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<MessageDto> Handle(GetByIdUserMessageQuery request, CancellationToken cancellationToken)
    {
        var id = ObjectId.Parse(request.Id);
        var filter = Builders<Message>.Filter.Eq(x => x.UserId, id);
        var result = await _work.GenericRepository<Message>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        return result.ToDto<MessageDto>();
    }
}