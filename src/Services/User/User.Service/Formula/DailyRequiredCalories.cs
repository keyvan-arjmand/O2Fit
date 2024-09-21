using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Enum;

namespace User.Service.Formula
{
   public static class DailyRequiredCalories
    {
        /// <summary>
        /// کالری روزانه مورد نیاز کاربر
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="gender"></param>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        public static double CalcDailyRequiredCalories(DailyActivityRate rate, Gender gender, double weight, double height, int age)
        {
            var bmr = gender switch
            {
                Gender.Male => Bmr.CalBmrForMale(weight, height, age),
                Gender.Female => Bmr.CalBmrForFemale(weight, height, age),
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };
            return rate switch
            {
                DailyActivityRate.Sedentary => (bmr * 1.200),
                DailyActivityRate.Light => (bmr * 1.375),
                DailyActivityRate.Moderate => (bmr * 1.465),
                DailyActivityRate.Active => (bmr * 1.550),
                DailyActivityRate.VeryActive => (bmr * 1.725),
                DailyActivityRate.ExtraActive => (bmr * 1.900),
                _ => throw new ArgumentOutOfRangeException(nameof(rate), rate, null)
            };
        }


        /// <summary>
        /// کالری روزانه مورد نیاز کاربر با نقص عضو
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="gender"></param>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <param name="disabilities"></param>
        /// <returns></returns>
        public static double CalcDailyRequiredCaloriesForDisable(DailyActivityRate rate, Gender gender, double weight, double height,
                                                                 int age,
                                                                 IEnumerable<Disability> disabilities)
        {
            var bmr = gender switch
            {
                Gender.Male => Bmr.CalBmrForDisableMale(weight, height, age, disabilities),
                Gender.Female => Bmr.CalBmrForDisableFemale(weight, height, age, disabilities),
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };
            return rate switch
            {
                DailyActivityRate.Sedentary => (bmr * 1.200),
                DailyActivityRate.Light => (bmr * 1.375),
                DailyActivityRate.Moderate => (bmr * 1.465),
                DailyActivityRate.Active => (bmr * 1.550),
                DailyActivityRate.VeryActive => (bmr * 1.725),
                DailyActivityRate.ExtraActive => (bmr * 1.900),
                _ => throw new ArgumentOutOfRangeException(nameof(rate), rate, null)
            };
        }

    }
}
