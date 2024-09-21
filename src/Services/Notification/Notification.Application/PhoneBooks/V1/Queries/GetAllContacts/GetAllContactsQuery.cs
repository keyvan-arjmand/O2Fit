namespace Notification.Application.PhoneBooks.V1.Queries.GetAllContacts;

public record GetAllContactsQuery() : IRequest<List<PhoneBookDto>>;
