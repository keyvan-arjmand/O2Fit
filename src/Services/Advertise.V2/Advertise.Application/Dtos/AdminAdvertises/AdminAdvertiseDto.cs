namespace Advertise.Application.Dtos.AdminAdvertises;

public class AdminAdvertiseDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}