using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using User.Domain.Models;

namespace User.Service.Formula
{
  public static class SleepTimeCalori
    {
        /// <summary>
        ///محاسبه کالری سوزانده شده در خواب
        /// </summary>
        /// <param name="model"></param>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <param name="sleepInfo"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static SleepTimeCalories CalcSleepTimeCalories(ActivityTime model, double weight,
         double height, int age, IEnumerable<(TimeSpan Duration, short Rate)> sleepInfo, bool? gender)
        {
            var bmr = gender switch
            {
                true => Bmr.CalBmrForMale(weight, height, age),
                false => Bmr.CalBmrForFemale(weight, height, age),
                _ => throw new NotFoundException("اطلاعات کاربری شما کامل نیست.")
            };
            var range = model.EndDate.Subtract(model.StartDate);
            var sleepInfoList = sleepInfo.ToList();
            var result = (bmr / 1440) * sleepInfoList.Sum(d => d.Duration.TotalMinutes) * range.TotalDays;
            return new SleepTimeCalories
            {
                TotalCalories = result,
                DurationAverage = sleepInfoList.Average(d => d.Duration.TotalMinutes),
                RateAverage = sleepInfoList.Average(d => d.Rate)
            };
        }

    }
}
