﻿namespace Food.V2.Application.Dtos.Food;

public class FoodParametersDto
{
    public List<decimal> FoodNutrients { get; set; } = new();
    public decimal FoodWeight { get; set; }
    public decimal BakingRatio { get; set; }
    public decimal BakingTimeInMinute { get; set; }
}