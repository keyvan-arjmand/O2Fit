using Food.V2.Application.Dtos.Recipe;

namespace Food.V2.Application.RecipeTips.V1.Queries.GetAllRecipeTips;

public record GetAllRecipeTipsQuery():IRequest<List<RecipeTipDto>>;