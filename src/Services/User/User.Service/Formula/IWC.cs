using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using User.Domain.Enum;

namespace User.Service.Formula
{
    public static class IWC
    {

        /// <summary>
        ///     وزن ایده آل برای آقایان
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double CalcIwcForMale(double height)
        {
            height = NormalizeHeight.ToCm(height);
            if (height < 152)
                return 56.2;
            var result = 56.2 + (((height - 152) / 2.54) * 1.41);
            return Math.Round(result,2);
        }

        /// <summary>
        ///     وزن ایده آل برای آقایان معلول
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double CalcIwcForDisableMale(double height, IEnumerable<Disability> disabilities)
        {
            var iwc = (CalcIwcForMale(height)) * (1 - (Disabilities.DisabilityPercents.Where(x => disabilities.Contains(x.Disability)).Sum(x => x.Percent) / 100));
            return iwc;
        }

        /// <summary>
        ///     وزن ایده آل برای خانمها
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double CalcIwcForFemale(double height)
        {
            height = NormalizeHeight.ToCm(height);
            if (height < 152)
                return 53.1;
            var result = 53.1 + ((height - 152) / 2.54) * 1.36;
            return Math.Round(result,2);
        }

        /// <summary>
        ///     وزن ایده آل برای خانمهای معلول
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double CalcIwcForDisableFemale(double height, IEnumerable<Disability> disabilities)
        {
            var iwc = CalcIwcForFemale(height) * (1 - (Disabilities.DisabilityPercents.Where(x => disabilities.Contains(x.Disability)).Sum(x => x.Percent) / 100));
            return iwc;
        }
    }
}
