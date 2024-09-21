using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.RecipeTips.V1.Queries.GetRecipeTipsByFoodId;

public record GetRecipeTipsByFoodIdQuery(string Id):IRequest<List<TranslationDto>>;