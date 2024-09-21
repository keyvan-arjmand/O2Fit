using Market.Application.Dtos;

namespace Market.Application.Faqs.V1.Commands.InsertFaq;

public class InsertFaqCommand:IRequest
{
    public string CategoryId { get; set; } = string.Empty;

    public TranslationDto Category { get; set; } = new();
    public TranslationDto Question { get; set; } = new();
    public TranslationDto Answer { get; set; } = new();
}