using System;
using System.Collections.Generic;
using System.Text;

namespace User.Service.Formula
{
    public static class FoodMealCalories
    {
        /// <summary>
        /// محاسبه میزان کالری هر وعده که باید مصرف شود
        /// </summary>
        /// <param name="dayRequiredCalories"></param>
        /// <param name="foodMealPercentage"></param>
        /// <returns></returns>
        public static double CalcFoodMealCalories(double dayRequiredCalories, int foodMealPercentage) =>
            dayRequiredCalories * foodMealPercentage / 100;
    }
}
