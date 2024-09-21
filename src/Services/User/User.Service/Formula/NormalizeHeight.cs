using System;
using System.Collections.Generic;
using System.Text;

namespace User.Service.Formula
{
    public static class  NormalizeHeight
    {
        /// <summary>
        /// تبدیل قد به متر
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double ToMeter(double height) => (height > 2.5) ? height / 100 : height;
       
        /// <summary>
        /// تبدیل قد به سانتیمتر
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double ToCm(double height) => (height > 2.5) ? height : height * 100;
    }
}
