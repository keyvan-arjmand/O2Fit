using Market.Application.Dtos;

namespace Market.Application.AppLearnCategories.V1.Commands.UpdateAppLearnCategory;

public record UpdateAppLearnCategoryCommand(string Id, TranslationDto Title) : IRequest;