namespace Notification.Application.PhoneBooks.V1.Queries.GetUserDataFromPhoneBook;

public record GetUserDataFromPhoneBookQuery(string UserId) : IRequest<PhoneBookDto>;