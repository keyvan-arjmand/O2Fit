using Common.Enums.TypeEnums;

namespace EventBus.Messages.Events.Services.Identity.Package;

public record UserPackageRegistered:BaseEvent
{
    public string UserId { get; init; }
    public DateTime ExpireDate { get; init; }
    public PackageType PackageType { get; init; }
}