namespace Food.V2.Domain.Enums
{
    public enum BakingType
    {
        [Display(Name = "شعله خیلی کم")]
        [Description("0.002")]
        VeryLowFlame = 1,

        [Display(Name = "شعله کم")]
        [Description("0.0062")]
        LowFlame=2,

        [Display(Name = "شعله متوسط")]
        [Description("0.0072")]
        MediumFlame=3,

        [Display(Name = "شعله زیاد")]
        [Description("0.009")]
        HighFlame=4,

        [Display(Name = "سرخ کردنی")]
        [Description("0.01")]
        PanFrying=5,

        [Display(Name = "آتشی یا کبابی")]
        [Description("0.035")]
        OnFire=6,

        [Display(Name = "تنوری - فر")]
        [Description("0.015")]
        ElectricOven=7,

        [Display(Name = "بخارپز")]
        [Description("0.002")]
        SteamerModel=8,

        [Display(Name = "بدون نوع پخت")]
        [Description("0")]
        NoTypeOfBaking=9,

    }
}
