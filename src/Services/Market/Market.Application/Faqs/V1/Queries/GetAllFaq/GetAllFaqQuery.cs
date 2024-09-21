using Market.Application.Dtos;

namespace Market.Application.Faqs.V1.Queries.GetAllFaq;

public record GetAllFaqQuery():IRequest<List<FaqDto>>;