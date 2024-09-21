using Ticket.Application.Dtos;

namespace Ticket.Application.UserContacts.V1.Queries.GetAllContact;

public record GetAllContactQuery(int Page,int PageSize) : IRequest<PaginationResult<ContactDto>>;