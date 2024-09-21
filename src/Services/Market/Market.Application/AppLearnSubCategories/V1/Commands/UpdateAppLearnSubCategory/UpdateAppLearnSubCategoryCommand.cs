using Market.Application.Dtos;

namespace Market.Application.AppLearnSubCategories.V1.Commands.UpdateAppLearnSubCategory;

public record UpdateAppLearnSubCategoryCommand(string CategoryId,string Id, TranslationDto Title) : IRequest;