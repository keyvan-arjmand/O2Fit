namespace Identity.V2.Application.Countries.V1.Commands.SoftDeleteByOldSystemId;

public record SoftDeleteByOldSystemIdCommand(int Id): IRequest;