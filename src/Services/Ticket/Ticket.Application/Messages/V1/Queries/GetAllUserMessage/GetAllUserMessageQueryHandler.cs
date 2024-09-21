using MongoDB.Bson;
using Ticket.Application.Common.Interfaces.Persistence.UoW;
using Ticket.Application.Common.Mapping;
using Ticket.Application.Dtos;
using Ticket.Domain.Aggregates.MessageAggregate;

namespace Ticket.Application.Messages.V1.Queries.GetAllUserMessage;

public class GetAllUserMessageQueryHandler : IRequestHandler<GetAllUserMessageQuery, List<MessageDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllUserMessageQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<MessageDto>> Handle(GetAllUserMessageQuery request, CancellationToken cancellationToken)
    {
        var userId = ObjectId.Parse(request.UserId);
        var filter = Builders<Message>.Filter.Eq(x => x.UserId, userId);
        filter &= Builders<Message>.Filter.Eq(x => x.IsUserRead, false);
        var result = await _work.GenericRepository<Message>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return result.ToDto<MessageDto>().ToList();
    }
}