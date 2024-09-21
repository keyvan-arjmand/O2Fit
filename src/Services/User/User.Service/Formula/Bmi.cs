using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using User.Domain.Enum;
using User.Domain.Models;

namespace User.Service.Formula
{
    public static class Bmi
    {
        /// <summary>
        /// محاسبه BMIو مریضی های هر نوع
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static BmiCalculation CalculateBmi(double weight, double height)
        {
            var result = new BmiCalculation();
            var Bmi = weight / Math.Pow(NormalizeHeight.ToMeter(height), 2);
            if (Bmi < 16)
            {
                result.DiseaseExpose = DiseaseExpose.DangerousThinness.ToString();
                result.DiseaseExposeDescription= DiseaseExposeDescription.DangerousThinnessDes.ToString();
            }
            else if (Bmi >= 16 && Bmi < 17)
            {
                result.DiseaseExpose = DiseaseExpose.WorryingThinness.ToString();
                result.DiseaseExposeDescription = DiseaseExposeDescription.WorryingThinnessDes.ToString();
            }
            else if (Bmi >= 17 && Bmi < 18.5)
            {
                result.DiseaseExpose = DiseaseExpose.ThinPerson.ToString();
                result.DiseaseExposeDescription = DiseaseExposeDescription.ThinPersonDes.ToString();
            }
            else if (Bmi >= 18.5 && Bmi < 25)
            {
                result.DiseaseExpose = DiseaseExpose.NormalProportionalStatus.ToString();
                result.DiseaseExposeDescription = DiseaseExposeDescription.NormalProportionalStatusDes.ToString();
            }
            else if (Bmi >= 25 && Bmi < 30)
            {
                result.DiseaseExpose = DiseaseExpose.HavingOverweight.ToString();
                result.DiseaseExposeDescription = DiseaseExposeDescription.HavingOverweightDes.ToString();
            }
            else if (Bmi >= 30 && Bmi < 35)
            {
                result.DiseaseExpose = DiseaseExpose.HavingOverweight.ToString();
                result.DiseaseExposeDescription = DiseaseExposeDescription.HavingOverweightDes.ToString();
            }
            else if (Bmi >= 35 && Bmi < 40)
            {
                result.DiseaseExpose = DiseaseExpose.WorryingOverweight.ToString();
                result.DiseaseExposeDescription = DiseaseExposeDescription.WorryingOverweightDes.ToString();
            }
            else
            {
                result.DiseaseExpose = DiseaseExpose.DangerouslyOverweight.ToString();
                result.DiseaseExposeDescription = DiseaseExposeDescription.DangerouslyOverweightDes.ToString();
            }
            return result;
        }


        /// <summary>
        /// محاسبه BMIافراد دارای نقص عضو
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="disabilities"></param>
        /// <returns></returns>
        public static BmiCalculation CalBmiForDisable(double weight, double height, IEnumerable<Disability> disabilities)
        {
            double WeightBeforeDisability = Disabilities.CalcWeightBeforeDisability(weight, disabilities);
            var BmiResult = CalculateBmi(WeightBeforeDisability, height);
            return BmiResult;
        }

    }
}
