namespace Food.V2.Application.Dtos.MissingFood;

public class MissingFoodDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Barcode { get; set; }= string.Empty;
    public string FirstImageName { get; set; }= string.Empty;
    public string SecondImageName { get; set; }= string.Empty;
    public string ThirdImageName { get; set; }= string.Empty;
    public DateTime Created { get; set; }
}