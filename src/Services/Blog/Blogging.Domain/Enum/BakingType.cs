using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blogging.Domain.Enum
{
    public enum BakingType
    {

        [Display(Name = "شعله زیاد")]
        [Description("0.009")]
        HighFlame=1,

        [Display(Name = "شعله کم")]
        [Description("0.0062")]
        LowFlame=2,

        [Display(Name = "شعله متوسط")]
        [Description("0.0072")]
        MediumFlame=3,

        [Display(Name = "بدون نوع پخت")]
        [Description("0")]
        NoTypeOfBaking=4,

        [Display(Name = "تنوری - فر")]
        [Description("0.015")]
        ElectricOven=5,

        [Display(Name = "آتشی یا کبابی")]
        [Description("0.035")]
        OnFire=6,

        [Display(Name = "بخارپز")]
        [Description("0.002")]
        SteamerModel=7,

        [Display(Name = "سرخ کردنی")]
        [Description("0.01")]
        PanFrying=8,
    }
}
