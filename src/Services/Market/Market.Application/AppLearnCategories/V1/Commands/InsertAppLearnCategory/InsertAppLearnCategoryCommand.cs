using Market.Application.Dtos;

namespace Market.Application.AppLearnCategories.V1.Commands.InsertAppLearnCategory;

public class InsertAppLearnCategoryCommand : IRequest
{
    public TranslationDto Title { get; set; } = new();
}