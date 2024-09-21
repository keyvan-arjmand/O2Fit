using System;
using System.Collections.Generic;
using System.Text;

namespace User.Service.Formula
{
   public static class FmAndLm
    {
        /// <summary>
        ///کل چربی بدن
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="bfp"></param>
        /// <returns></returns>
        public static double CalcFm(double weight, double bfp) => bfp * weight / 100;
      
        /// <summary>
        /// وزن بدن بدون چربی
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="bfp"></param>
        /// <returns></returns>
        public static double CalcLm(double weight, double bfp) => weight - CalcFm(weight, bfp);

    }
}
