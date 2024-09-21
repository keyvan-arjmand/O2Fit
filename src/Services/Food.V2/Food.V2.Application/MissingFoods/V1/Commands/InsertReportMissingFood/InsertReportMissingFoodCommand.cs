namespace Food.V2.Application.MissingFoods.V1.Commands.InsertReportMissingFood;

public class InsertReportMissingFoodCommand:IRequest
{
    public string Name { get; set; } = string.Empty;
    public string Barcode { get; set; }= string.Empty;
    public string FirstImageUri { get; set; }= string.Empty;
    public string SecondImageUri { get; set; }= string.Empty;
    public string ThirdImageUri{ get; set; }= string.Empty;
}