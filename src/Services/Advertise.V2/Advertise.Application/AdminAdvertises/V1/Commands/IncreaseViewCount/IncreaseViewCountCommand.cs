namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseViewCount;

public record IncreaseViewCountCommand(string Id) : IRequest;