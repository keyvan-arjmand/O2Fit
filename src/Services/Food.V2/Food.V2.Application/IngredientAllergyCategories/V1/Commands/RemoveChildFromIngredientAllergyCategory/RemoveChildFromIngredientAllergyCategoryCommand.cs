﻿namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.RemoveChildFromIngredientAllergyCategory;

public record RemoveChildFromIngredientAllergyCategoryCommand(string Id, string ChildId): IRequest;