using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.Calculate
{
    public static class CalculateMacroNut { 
        public static MacroNutrients CalculateMacroNutByBodyType(double DailyCalorie, int BodyType, int FoodMeal)
        {
            double calorie = DailyCalorie;
            switch (FoodMeal)
            {
                case 1:
                    calorie = DailyCalorie / 0.25;
                    break;
                case 2:
                    calorie = DailyCalorie / 0.35;
                    break;
                case 3:
                    calorie = DailyCalorie / 0.15;
                    break;
                case 4:
                    calorie = DailyCalorie / 0.25;
                    break;
                case 5:
                    calorie = DailyCalorie / 0.05;
                    break;
                case 6:
                    calorie = DailyCalorie / 0.05;
                    break;
                case 7:
                    calorie = DailyCalorie / 0.05;
                    break;
                default:
                    break;
            }
            var macroNutrients = new MacroNutrients();
            switch (BodyType)
            {
                case 0:
                    macroNutrients = new MacroNutrients()
                    {
                        Calorie = calorie,
                        Carbohydrate = calorie * 0.2 / 4,
                        Protein = calorie * 0.5 / 4,
                        Fat = calorie * 0.3 / 9
                    };
                    break;
                case 1:
                    macroNutrients = new MacroNutrients()
                    {
                        Calorie = calorie,
                        Carbohydrate = calorie * 0.4 / 4,
                        Protein = calorie * 0.3 / 4,
                        Fat = calorie * 0.3 / 9
                    };
                    break;
                case 2:
                    macroNutrients = new MacroNutrients()
                    {
                        Calorie = calorie,
                        Carbohydrate = calorie * 0.25 / 4,
                        Protein = calorie * 0.35 / 4,
                        Fat = calorie * 0.4 / 9
                    };
                    break;
                default:
                    macroNutrients = new MacroNutrients()
                    {
                        Calorie = calorie,
                        Carbohydrate = calorie / (3 * 4),
                        Protein = calorie / (3 * 4),
                        Fat  = calorie / (3 * 9)
                    };
                    break;
            };
            return new MacroNutrients()
            {
                Calorie = Math.Round(macroNutrients.Calorie, 2),
                Carbohydrate = Math.Round(macroNutrients.Carbohydrate, 2),
                Protein = Math.Round(macroNutrients.Protein, 2),
                Fat = Math.Round(macroNutrients.Fat, 2)
            };
        }


    }
    public class MacroNutrients
    {
        public double Calorie { get; set; }
        public double Carbohydrate { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
    }
}
