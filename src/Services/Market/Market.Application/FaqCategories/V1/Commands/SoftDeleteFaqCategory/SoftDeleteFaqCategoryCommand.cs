namespace Market.Application.FaqCategories.V1.Commands.SoftDeleteFaqCategory;

public record SoftDeleteFaqCategoryCommand(string Id) : IRequest;