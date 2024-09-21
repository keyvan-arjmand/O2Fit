using Market.Application.Dtos;

namespace Market.Application.AppLearnCategories.V1.Queries.GetAllAppLearn;

public record GetAllAppLearnQuery(string Id):IRequest<List<AppLearnCategoryDto>>;