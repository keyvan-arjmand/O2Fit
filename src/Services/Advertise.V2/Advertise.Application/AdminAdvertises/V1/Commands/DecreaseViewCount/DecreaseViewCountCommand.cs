namespace Advertise.Application.AdminAdvertises.V1.Commands.DecreaseViewCount;

public record DecreaseViewCountCommand(string Id) : IRequest;