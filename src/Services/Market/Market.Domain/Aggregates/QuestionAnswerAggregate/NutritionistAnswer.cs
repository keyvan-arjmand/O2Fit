using Market.Domain.Common;
using Market.Domain.Enums;

namespace Market.Domain.Aggregates.QuestionAnswerAggregate;

public class NutritionistAnswer : BaseAuditableEntity
{
    public string Answer { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ScientificDegree ScientificDegree { get; set; } 
}