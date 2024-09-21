using Market.Application.Dtos;

namespace Market.Application.Faqs.V1.Queries.GetByIdFaq;

public record GetByIdFaqQuery(string Id):IRequest<FaqDto>;