using Ticket.Application.Dtos;

namespace Ticket.Application.UserContacts.V1.Queries.GetByIdContact;

public record GetByIdContactQuery(string Id):IRequest<ContactDto>;