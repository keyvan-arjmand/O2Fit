using System;
using System.Collections.Generic;
using System.Text;

namespace User.Service.Formula
{
    class WaterCalculator
    {
        /// <summary>
        /// آب مورد نیاز کاربر
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="activityMinutes"></param>
        /// <returns></returns>
        public static double RequiredWater(double weight, double activityMinutes)
        {
            var requiredWaterInMl = weight * 41.6150228;
            var consumedWater = 11.336 * activityMinutes;
            return requiredWaterInMl;
        }
    }
}
