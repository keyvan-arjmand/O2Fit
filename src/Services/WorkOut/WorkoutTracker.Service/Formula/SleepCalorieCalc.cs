using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkoutTracker.Domain.Enum;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.Service.Formula
{
  public static class SleepCalorieCalc
    {
        /// <summary>
        /// محاسبه کالری سوزانده شده در خواب
        /// </summary>
        /// <param name="dateTimes"></param>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <param name="gender"></param>
        /// <param name="disabilities"></param>
        /// <returns></returns>
        public static SleepCalorieModel CalcSleepTimeCalories(ActivityTime dateTimes, double weight,
         double height, int age, Gender gender, IEnumerable<Disability> disabilities)
        {
            SleepCalorieModel sleepCalorie = new SleepCalorieModel();
            var bmr = Formula.Bmr.CalBmr(weight, height, age, gender, disabilities);
            sleepCalorie.Duration = dateTimes.EndDate.Subtract(dateTimes.StartDate);
            sleepCalorie.Calories = (bmr / 1440) * sleepCalorie.Duration.TotalMinutes;
            return sleepCalorie;
        }

    }
}
