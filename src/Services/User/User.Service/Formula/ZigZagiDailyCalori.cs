using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using User.API.Models;
using User.Domain.Enum;

namespace User.Service.Formula
{
    public static class ZigZagiDailyCalori
    {
        /// <summary>
        /// محاسبه کالری زیگزاگی کاربر بر اساس تقویم و پارامترهای بدنی کاربر
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="gender"></param>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <param name="lcId"></param>
        /// <returns></returns>
        public static ZigzagiDailyCalories CalcZigzagDailyCalories(
         DailyActivityRate rate, Gender gender, double weight, double height, int age, int lcId)
        {
            var daily = DailyRequiredCalories.CalcDailyRequiredCalories(rate, gender, weight, height, age);
            return CalcZigzagDailyCalories(daily, lcId);
        }

        /// <summary>
        /// محاسبه کالری زیگزاگی کاربر بر اساس تقویم و کالری مورد نیاز روزانه
        /// </summary>
        /// <param name="dailyRequiredCalories"></param>
        /// <param name="lcId"></param>
        /// <returns></returns>
        public static ZigzagiDailyCalories CalcZigzagDailyCalories(double dailyRequiredCalories, int lcId)
        {
            var cals = new[]
            {
               Math.Round(dailyRequiredCalories * 87  / 100,2),
               Math.Round(dailyRequiredCalories * 87  / 100,2),
               Math.Round(dailyRequiredCalories * 87  / 100,2),
               Math.Round(dailyRequiredCalories * 87  / 100,2),
               Math.Round(dailyRequiredCalories * 87  / 100,2),
               Math.Round(dailyRequiredCalories * 133 / 100,2),
               Math.Round(dailyRequiredCalories * 132 / 100,2)
            };
            var days = GetDaysOfWeek(lcId).ToArray();
            var result = new List<ZigzagiDailyCalories>();
            for (var index = 0; index < days.Length; index++)
                result.Add(new ZigzagiDailyCalories
                {
                    Calories = cals[index],
                    DayName = days[index].Day.ToString()
                });
            var res = result.Find(d => d.DayName == DateTime.Now.DayOfWeek.ToString());
            return res;
        }

        /// <summary>
        /// محاسبه کالری زیگزاگی با توجه به کاهش ,افزایش وزن هدف
        /// </summary>
        /// <param name="targetWeight"></param>
        /// <param name="weightPerWeek"></param>
        /// <param name="rate"></param>
        /// <param name="gender"></param>
        /// <param name="nowWeight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <param name="lcId"></param>
        /// <returns></returns>
        public static ZigzagiDailyCalories CalcAchieveToTargetWeight(double targetWeight, double weightPerWeek, DailyActivityRate rate, Gender gender, double nowWeight, double height, int age, int lcId)
        {
            var targetCaloriPerDay = (7700 * weightPerWeek) / 7;
            double dailyTarget = 0;
            if (targetWeight < nowWeight)
            {
                dailyTarget = DailyRequiredCalories.CalcDailyRequiredCalories(rate, gender, nowWeight, height, age) - targetCaloriPerDay;
            }
            else if (targetWeight > nowWeight)
            {
                dailyTarget = DailyRequiredCalories.CalcDailyRequiredCalories(rate, gender, nowWeight, height, age) + targetCaloriPerDay;
            }
            else if (targetWeight == nowWeight)
            {
                dailyTarget = DailyRequiredCalories.CalcDailyRequiredCalories(rate, gender, nowWeight, height, age);
            };

            return CalcZigzagDailyCalories(dailyTarget, lcId);
        }

        /// <summary>
        /// روزهای هفته بر اساس تقوم کاربر
        /// </summary>
        /// <param name="lcId"></param>
        /// <returns></returns>
        public static IEnumerable<(DayOfWeek Day, string DayName)> GetDaysOfWeek(int lcId)
        {
            var daysOfWeek = System.Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToArray();
            var cul = CultureInfo.GetCultureInfo(lcId);
            var firstDayIndex = cul.DateTimeFormat.FirstDayOfWeek;
            if (firstDayIndex.Equals(DayOfWeek.Sunday))
            {

            }
            else
            {
                var dayName = cul.DateTimeFormat.DayNames[6];
                var dayOfWeek = daysOfWeek[6];
                yield return (dayOfWeek, dayName);
                for (int i = 0; i < 6; i++)
                {
                    dayName = cul.DateTimeFormat.DayNames[i];
                    dayOfWeek = daysOfWeek[i];
                    yield return (dayOfWeek, dayName);
                }
            }
        }
    }
}
