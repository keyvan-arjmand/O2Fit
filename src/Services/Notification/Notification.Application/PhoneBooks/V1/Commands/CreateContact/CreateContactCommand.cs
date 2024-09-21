namespace Notification.Application.PhoneBooks.V1.Commands.CreateContact;

public record CreateContactCommand(string UserId, string Username, string? FullName, bool IsNutritionist, string FcmToken): IRequest;