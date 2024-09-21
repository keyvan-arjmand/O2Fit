namespace Advertise.Application.AdminAdvertises.V1.Commands.ChangeAdminAdvertiseStatus;

public record ChangeAdminAdvertiseStatusCommand(string Id, AdvertiseStatus Status) : IRequest;