using Market.Domain.Common;
using Market.Domain.Enums;

namespace Market.Domain.Aggregates.QuestionAnswerAggregate;

public class QuestionAnswer : AggregateRoot
{
    public QuestionCategory Category { get; set; }
    public string Question { get; set; } = string.Empty;
    public List<NutritionistAnswer> Answers { get; set; } = new();
}  