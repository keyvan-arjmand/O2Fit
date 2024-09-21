using Common.Constants.Food;
using Track.Application.TrackStep;
using Track.Domain.Enums;

namespace Track.Application.Common.Utilities;

public static class Calculate
{
 
    public static class  NormalizeHeight
    {
        public static double ToMeter(double height) => (height > 2.5) ? height / 100 : height;
        public static double ToCm(double height) => (height > 2.5) ? height : height * 100;
    }
    public static decimal CalculateBurnedCalorieSteps(this int steps, double weight)
    {
        return Convert.ToDecimal((steps * Keys.BurnedCalorieMultiplication * weight));
    }
    public static double CalBmrForDisableMale(double weight, double height, int age, IEnumerable<Disability> disabilities)
    {
        double WeightBeforeDisability = Disabilities.CalcWeightBeforeDisability(weight, disabilities);
        var bmr = CalBmrForMale(WeightBeforeDisability, height, age);
        var result = bmr * (1 - (Disabilities.DisabilityPercents.Where(x => disabilities.Contains(x.Disability)).Sum(x => x.Percent) / 100));
        return result;
    }
    public static double CalBmrForDisableFemale(double weight, double height, int age, IEnumerable<Disability> disabilities)
    {
        double WeightBeforeDisability = Disabilities.CalcWeightBeforeDisability(weight, disabilities);
        var bmr = CalBmrForFemale(WeightBeforeDisability, height, age);
        var result = bmr * (100 - Disabilities.DisabilityPercents.Where(x => disabilities.Contains(x.Disability)).Sum(x => x.Percent) / 100);
        return result;
    }
    public static double CalBmrForFemale(double weight, double height, int age) => (10 * weight) + (6.25 * NormalizeHeight.ToCm(height)) - (5 * age) - 161;
    public static double CalBmrForMale(double weight, double height, int age) => (10 * weight) + (6.25 * NormalizeHeight.ToCm(height)) - (5 * age) + 5;

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

}