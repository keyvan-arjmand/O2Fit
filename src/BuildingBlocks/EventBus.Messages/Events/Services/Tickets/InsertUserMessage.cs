namespace EventBus.Messages.Events.Services.Tickets;

public record InsertUserMessage(string Title, string Description, string? ImageUri, string UserId,
    int Classification) : BaseEvent;