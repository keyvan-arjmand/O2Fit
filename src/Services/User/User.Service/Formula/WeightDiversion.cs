using System;
using System.Collections.Generic;
using System.Text;

namespace User.Service.Formula
{
  public static class WeightDiversion
    {
        /// <summary>
        /// اختلاف وزن کنونی کاربر نسبت به وزن ایده آل برای مردان
        /// </summary>
        /// <param name="currentWeight"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double WeightDiversionForMale(double currentWeight, double height) => currentWeight - IWC.CalcIwcForMale(height);

        /// <summary>
        /// اختلاف وزن کنونی کاربر نسبت به وزن ایده آل برای خانم ها
        /// </summary>
        /// <param name="currentWeight"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double WeightDiversionForFemale(double currentWeight, double height) => currentWeight - IWC.CalcIwcForFemale(height);
    }
}
