using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using User.Domain.Enum;

namespace User.Service.Formula
{
    public static class MinimumWeight
    {
        /// <summary>
        /// کمترین وزن کاربر
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double MinWeight(double height) => Math.Round(Math.Pow(NormalizeHeight.ToMeter(height), 2) * 18.5);

        /// <summary>
        /// کمترین وزن کاربر دارای نقص عضو
        /// </summary>
        /// <param name="height"></param>
        /// <param name="disabilities"></param>
        /// <returns></returns>
        public static double MinWeightForDisable(double height, IEnumerable<Disability> disabilities)
        {
            var min = MinWeight(height) * (1 - (Disabilities.DisabilityPercents.Where(x => disabilities.Contains(x.Disability)).Sum(x => x.Percent) / 100));
            var result = min;
            return Math.Round(result,2);
        }

    }
}
