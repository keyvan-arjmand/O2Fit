﻿using Common.Enums.TypeEnums;

namespace Discount.Application.Dtos;

public class ApplyDiscountCodeNutritionistResult
{
    public string Id { get; set; }=string.Empty;
    public string Code { get; set; } = string.Empty;
    public double PricePackage { get; set; }//قیمت پکیج
    public double DiscountedPrice { get; set; }//قیمت پکیج با تخفیف
    public double DiscountAmount { get; set; }//مبلغ تخفیف کد
    public double PackageDiscount { get; set; }//مبلغ تخفیف پکیج
    public DiscountType DiscountType { get; set; } 
}