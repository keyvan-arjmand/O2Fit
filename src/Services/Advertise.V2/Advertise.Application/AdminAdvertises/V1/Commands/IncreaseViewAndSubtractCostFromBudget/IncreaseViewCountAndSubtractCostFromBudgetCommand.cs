﻿namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseViewAndSubtractCostFromBudget;

public record IncreaseViewCountAndSubtractCostFromBudgetCommand(string Id) : IRequest;