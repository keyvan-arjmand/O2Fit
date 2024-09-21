using System;
using System.Collections.Generic;
using System.Text;

namespace User.Service.Formula
{
   public static class BFP
    {
        /// <summary>
        /// ‌درصد چربی بدن برای مردان
        /// </summary>
        /// <param name="height"></param>
        /// <param name="neck"></param>
        /// <param name="waist"></param>
        /// <returns></returns>
        public static double CalcBfpForMale(double height, double neck, double waist) =>
        (495 / ((1.0324 - 0.19077 * Math.Log10(waist - neck) + Math.Log10(height) * 0.15456))) - 450;

        /// <summary>
        /// ‌درصد چربی بدن برای خانم ها
        /// </summary>
        /// <param name="height"></param>
        /// <param name="neck"></param>
        /// <param name="waist"></param>
        /// <param name="hip"></param>
        /// <returns></returns>
        public static double CalcBfpForFemale(double height, double neck, double waist, double hip) =>
            (495 / ((1.29579 - 0.35004 * Math.Log10(hip + waist - neck) + Math.Log10(height) * 0.22100))) - 450;
    }
}
