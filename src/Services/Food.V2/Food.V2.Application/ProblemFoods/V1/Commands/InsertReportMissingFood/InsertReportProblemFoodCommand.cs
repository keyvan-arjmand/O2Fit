namespace Food.V2.Application.ProblemFoods.V1.Commands.InsertReportMissingFood;

public class InsertReportProblemFoodCommand : IRequest
{
    public string FoodId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ReasonsProblem ProblemType { get; set; } = new();
}