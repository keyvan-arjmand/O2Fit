using Ticket.Application.Dtos;

namespace Ticket.Application.Messages.V1.Queries.GetAllUserMessage;

public record GetAllUserMessageQuery(string UserId) : IRequest<List<MessageDto>>;