using Market.Application.Dtos;

namespace Market.Application.AppLearns.V1.Queries.GetByIdAppLearn;

public record GetByIdAppLearnQuery(string Id):IRequest<FaqDto>;