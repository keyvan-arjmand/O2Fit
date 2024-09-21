namespace Food.V2.Application.DietPacks.V1.Commands.SoftDeleteDietPack;

public record SoftDeleteDietPackCommand(string Id): IRequest;