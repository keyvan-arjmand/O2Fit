namespace Food.V2.Application.Ingredients.V1.Commands.UpdateIngredient;

public record UpdateIngredientCommand(string Id, CreateUpdateIngredientTranslationDto Name, string Code, string ThumbUri, string Tag , string TagEn,
                                      string TagAr, List<decimal> NutrientValue, List<string> MeasureUnitIds, string DefaultMeasureUnitId, bool IsAllergy): IRequest;