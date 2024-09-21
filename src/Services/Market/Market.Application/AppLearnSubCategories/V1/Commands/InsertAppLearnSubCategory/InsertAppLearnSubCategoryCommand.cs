using Market.Application.Dtos;

namespace Market.Application.AppLearnSubCategories.V1.Commands.InsertAppLearnSubCategory;

public class InsertAppLearnSubCategoryCommand : IRequest
{
    public string CategoryId { get; set; } = string.Empty;
    public TranslationDto Title { get; set; } = new();
}