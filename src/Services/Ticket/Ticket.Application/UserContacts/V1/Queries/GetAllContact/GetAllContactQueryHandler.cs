using MongoDB.Bson;
using Ticket.Application.Common.Interfaces.Persistence.UoW;
using Ticket.Application.Common.Mapping;
using Ticket.Application.Dtos;
using Ticket.Domain.Aggregates.ContactUsAggregate;

namespace Ticket.Application.UserContacts.V1.Queries.GetAllContact;

public class GetAllContactQueryHandler : IRequestHandler<GetAllContactQuery, PaginationResult<ContactDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllContactQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<PaginationResult<ContactDto>> Handle(GetAllContactQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<ContactUs>()
            .GetAllPaginationAsync(request.Page, request.PageSize, cancellationToken);
        var contact = result.Data.ToDto<ContactDto>().ToList();
        return PaginationResult<ContactDto>.CreateContactPaginationResult(request.Page, request.PageSize,
            result.Data.Count, contact);
    }
}