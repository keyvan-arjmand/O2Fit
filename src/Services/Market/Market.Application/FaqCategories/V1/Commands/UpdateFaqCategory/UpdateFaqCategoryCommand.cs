using Market.Application.Dtos;

namespace Market.Application.FaqCategories.V1.Commands.UpdateFaqCategory;

public class UpdateFaqCategoryCommand:IRequest
{
    public string Id { get; set; } = string.Empty;
    public TranslationDto Title { get; set; } = new();
}