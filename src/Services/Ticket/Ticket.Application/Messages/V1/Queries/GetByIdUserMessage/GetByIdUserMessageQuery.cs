using Ticket.Application.Dtos;

namespace Ticket.Application.Messages.V1.Queries.GetByIdUserMessage;

public record GetByIdUserMessageQuery(string Id):IRequest<MessageDto>;