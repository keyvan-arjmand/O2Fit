using Market.Application.Dtos;

namespace Market.Application.FaqCategories.V1.Queries.GetFaqById;

public record GetFaqByIdQuery(string Id):IRequest<FaqDto>;