namespace Track.Application.Dtos;

public class UserTrackNutrientDto
{
    public DateTime InsertDate { get; set; }
    public NutrientDto Nutrient { get; set; } = new();
}