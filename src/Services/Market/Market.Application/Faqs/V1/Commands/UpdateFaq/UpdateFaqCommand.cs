using Market.Application.Dtos;

namespace Market.Application.Faqs.V1.Commands.UpdateFaq;

public class UpdateFaqCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public string CategoryId{ get; set; }  = string.Empty;
    public TranslationDto Question { get; set; } = new();
    public TranslationDto Answer { get; set; } = new();
}