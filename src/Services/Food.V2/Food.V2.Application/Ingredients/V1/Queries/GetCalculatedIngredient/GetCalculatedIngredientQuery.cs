namespace Food.V2.Application.Ingredients.V1.Queries.GetCalculatedIngredient;

public record GetCalculatedIngredientQuery(List<IngredientMeasureUnitDto> IngredientMeasureUnitList): IRequest<CalculatedIngredientDto>;