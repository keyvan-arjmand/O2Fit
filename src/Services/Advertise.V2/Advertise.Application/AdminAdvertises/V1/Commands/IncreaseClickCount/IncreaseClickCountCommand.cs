namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseClickCount;

public record IncreaseClickCountCommand(string Id) : IRequest;