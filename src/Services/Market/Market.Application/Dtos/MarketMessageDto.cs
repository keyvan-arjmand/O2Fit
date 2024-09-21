using Market.Domain.Enums;

namespace Market.Application.Dtos;

public class MarketMessageDto
{
    public List<string> Version { get; set; } = new();
    public TranslationDto Title { get; set; } = new();
    public TranslationDto Description { get; set; } = new();
    public TranslationDto ButtonName { get; set; } = new();
    public string Link { get; set; } = string.Empty;
    public TargetLink Target { get; set; }
    public string ImageName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Postpone { get; set; }
}