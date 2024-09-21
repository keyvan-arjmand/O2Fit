using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Enum;

namespace User.Service.Formula
{
    public static class BodyTypeCalc
    {
        /// <summary>
        /// محاسبه نوع بدن کاربر
        /// </summary>
        /// <param name="wristSize"></param>
        /// <param name="height"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static BodyType CalcBodyType(double wristSize, double height, Gender gender)
        {
            double factor = height / wristSize;
            BodyType result = gender switch
            {
                Gender.Male => CalcMaleBodyFactor(factor),
                Gender.Female => CalcFemaleBodyFactor(factor),
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };
            return result;

        }

        /// <summary>
        /// تیپ بدنی برای مردان
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static BodyType CalcMaleBodyFactor(double factor)
        {
            if (factor > 10.4) return BodyType.Ectomorph;
            else if (factor < 9.6) return BodyType.Endomorph;
            else return BodyType.Mesomorph;
        }

        /// <summary>
        /// تیپ بدنی برای خانم ها
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static BodyType CalcFemaleBodyFactor(double factor)
        {
            if (factor > 11) return BodyType.Ectomorph;
            else if (factor < 10.1) return BodyType.Endomorph;
            else return BodyType.Mesomorph;
        }

    }
}
