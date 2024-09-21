using Market.Application.Dtos;
using Market.Domain.Enums;

namespace Market.Application.AppVersions.V1.Commands.UpdateAppVersion;

public class UpdateAppVersionCommand:IRequest
{
    public string Id { get; set; } = string.Empty;
    public List<string> Version { get; set; } = new();
    public TranslationDto Description { get; set; } = new();
    public bool IsForced { get; set; }
    public List<MarketType> MarketTypes { get; set; } = new();
    public string Link { get; set; } = string.Empty;
}