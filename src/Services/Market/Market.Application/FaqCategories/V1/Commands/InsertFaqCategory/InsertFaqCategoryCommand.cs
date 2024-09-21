using Market.Application.Dtos;

namespace Market.Application.FaqCategories.V1.Commands.InsertFaqCategory;

public class InsertFaqCategoryCommand:IRequest
{
    public TranslationDto Title { get; set; } = new();
}