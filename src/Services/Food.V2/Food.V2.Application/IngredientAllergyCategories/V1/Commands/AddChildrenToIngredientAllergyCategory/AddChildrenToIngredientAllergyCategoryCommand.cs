﻿namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.AddChildrenToIngredientAllergyCategory;

public record AddChildrenToIngredientAllergyCategoryCommand(string RootId, string ChildId): IRequest;