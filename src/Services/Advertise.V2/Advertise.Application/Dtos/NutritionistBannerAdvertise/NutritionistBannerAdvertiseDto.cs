namespace Advertise.Application.Dtos.NutritionistBannerAdvertise;

public class NutritionistBannerAdvertiseDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}