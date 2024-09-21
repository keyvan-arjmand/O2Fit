using MongoDB.Bson;
using Ticket.Application.Common.Interfaces.Persistence.UoW;
using Ticket.Application.Common.Mapping;
using Ticket.Application.Dtos;
using Ticket.Domain.Aggregates.ContactUsAggregate;

namespace Ticket.Application.UserContacts.V1.Queries.GetByIdContact;

public class GetByIdContactQueryHandler : IRequestHandler<GetByIdContactQuery, ContactDto>
{
    private readonly IUnitOfWork _work;

    public GetByIdContactQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<ContactDto> Handle(GetByIdContactQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<ContactUs>.Filter.Eq(x => x.Id, request.Id);
        var result = await _work.GenericRepository<ContactUs>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        return result.ToDto<ContactDto>();
    }
}