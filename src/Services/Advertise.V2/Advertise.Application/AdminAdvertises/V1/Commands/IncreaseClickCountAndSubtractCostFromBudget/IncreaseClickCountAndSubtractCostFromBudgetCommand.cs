﻿namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseClickCountAndSubtractCostFromBudget;

public record IncreaseClickCountAndSubtractCostFromBudgetCommand(string Id) : IRequest;