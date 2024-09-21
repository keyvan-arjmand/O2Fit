using Food.V2.Application.Dtos.Recipe;

namespace Food.V2.Application.RecipeTips.V1.Queries.GetByIdRecipeTip;

public record GetByIdRecipeTipQuery(string Id,string FoodId):IRequest<RecipeTipSingleDto>;