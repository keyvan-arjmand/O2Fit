namespace Food.V2.Application.DietCategories.V1.Queries.IsDietCategoryExits;

public record IsDietCategoryExitsQuery(string Id) : IRequest<bool>;