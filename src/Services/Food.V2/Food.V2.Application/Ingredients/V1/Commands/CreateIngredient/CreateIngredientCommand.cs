namespace Food.V2.Application.Ingredients.V1.Commands.CreateIngredient;

public record CreateIngredientCommand(CreateUpdateIngredientTranslationDto Name, string Code, string ThumbUri, string Tag, string TagEn,string TagAr, bool IsAllergy,
    List<decimal> NutrientValue, List<string> MeasureUnitIds, string DefaultMeasureUnitId): IRequest<string>;