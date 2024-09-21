using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using User.Domain.Enum;

namespace User.Service.Formula
{
    public static class MaximumWeight
    {
        /// <summary>
        /// بیشترین وزن کاربر
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double MaxWeight(double height) => Math.Round(Math.Pow(NormalizeHeight.ToMeter(height), 2) * 25.0);

        /// <summary>
        /// بیشترین وزن کاربر دارای نقض عضو
        /// </summary>
        /// <param name="height"></param>
        /// <param name="disabilities"></param>
        /// <returns></returns>
        public static double MaxWeightForDisable(double height, IEnumerable<Disability> disabilities)
        {
            var max = MaxWeight(height) * (1 - (Disabilities.DisabilityPercents.Where(x => disabilities.Contains(x.Disability)).Sum(x => x.Percent) / 100));
            var result = max;
            return Math.Round(result,2);
        }

    }
}
