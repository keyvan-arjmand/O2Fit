using Market.Application.Dtos;

namespace Market.Application.FaqCategories.V1.Queries.GetAllFaqByCategoryId;

public record GetAllFaqByCategoryIdQuery(string Id):IRequest<List<FaqDto>>;