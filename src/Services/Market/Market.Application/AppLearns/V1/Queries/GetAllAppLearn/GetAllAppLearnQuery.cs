using Market.Application.Dtos;

namespace Market.Application.AppLearns.V1.Queries.GetAllAppLearn;

public record GetAllAppLearnQuery():IRequest<List<FaqDto>>;