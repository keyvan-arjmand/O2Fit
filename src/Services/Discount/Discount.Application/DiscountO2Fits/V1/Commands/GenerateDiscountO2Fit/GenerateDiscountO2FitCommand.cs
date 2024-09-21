﻿using Common.Enums.TypeEnums;
using Discount.Application.Dtos;

namespace Discount.Application.DiscountO2Fits.V1.Commands.GenerateDiscountO2Fit;



public class GenerateDiscountO2FitCommand:IRequest
{
    public TranslationDto Description { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDateTime { get; set; }
    public string? UserId { get; set; }
    public List<int>? CountryIds { get; set; }
    public PackageType PackageType { get; set; }
    public int UsableCount { get; set; }
    public bool IsActive { get; set; }
    public int Percent { get; set; }
}