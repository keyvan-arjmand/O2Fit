using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.Service.Formula
{
    public static class Bmr
    {
        /// <summary>
        /// BMR
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public static double CalBmr(double weight, double height, int age,Gender gender, IEnumerable<Disability> disabilities)
        {
            double bmr;
            if (disabilities != null)
            {
                bmr = gender switch
                {
                    Gender.Male => CalBmrForDisableMale(weight, height, age, disabilities),
                    Gender.Female => CalBmrForDisableFemale(weight, height, age, disabilities),
                    //_ => throw new NotFoundException("")
                    _ => throw new Exception("Oh ooh"),
                };
            }
            else
            {
                bmr = gender switch
                {
                    Gender.Male => CalBmrForMale(weight, height, age),
                    Gender.Female => CalBmrForFemale(weight, height, age),
                    _ => throw new Exception("Oh ooh")
                    //_ => throw new NotFoundException("")
                };
            }

            return bmr;
        }


        /// <summary>
        /// مردها BMR
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        public static double CalBmrForMale(double weight, double height, int age) => (10 * weight) + (6.25 * NormalizeHeight.ToCm(height)) - (5 * age) + 5;

        /// <summary>
        /// خانم ها BMR 
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        public static double CalBmrForFemale(double weight, double height, int age) => (10 * weight) + (6.25 * NormalizeHeight.ToCm(height)) - (5 * age) - 161;

        /// <summary>
        /// Bmr افراد مذکر دارای نقص جسمی
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <param name="disabilities"></param>
        /// <returns></returns>
        public static double CalBmrForDisableMale(double weight, double height, int age, IEnumerable<Disability> disabilities)
        {
            double WeightBeforeDisability = Disabilities.CalcWeightBeforeDisability(weight, disabilities);
            var bmr = CalBmrForMale(WeightBeforeDisability, height, age);
            var result = bmr * (1 - (Disabilities.DisabilityPercents.Where(x => disabilities.Contains(x.Disability)).Sum(x => x.Percent) / 100));
            return result;
        }

        /// <summary>
        /// Bmr افراد مونث دارای نقص جسمی
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <param name="disabilities"></param>
        /// <returns></returns>
        public static double CalBmrForDisableFemale(double weight, double height, int age, IEnumerable<Disability> disabilities)
        {
            double WeightBeforeDisability = Disabilities.CalcWeightBeforeDisability(weight, disabilities);
            var bmr = CalBmrForFemale(WeightBeforeDisability, height, age);
            var result = bmr * (100 - Disabilities.DisabilityPercents.Where(x => disabilities.Contains(x.Disability)).Sum(x => x.Percent) / 100);
            return result;
        }

    }
}
