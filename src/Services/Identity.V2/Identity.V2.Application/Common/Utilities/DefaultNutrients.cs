namespace Identity.V2.Application.Common.Utilities;

public static class DefaultNutrients
{
    public static List<NotNegativeForDoubleTypes> DailyNutrients(int age, Gender gender)
    {
        string defaultNutrient = string.Empty;

        if (gender == Gender.Female)
        {
            if (age >= 14 && age < 19) defaultNutrient = "0,1800,3000,0,4700,700,1500,2.4,360,0,9,0,55,1.3,0,1.1,14,50,15,65,1250,75,0.9,0,5,10000,2500,14,400,11,1.1,0,0,15" ;
            if (age >= 19 && age <= 50) defaultNutrient ="0,2200,3500,0,4700,700,1500,2.4,310,0,8,0,55,1.3,0,1.1,14,50,18,75,700,90,0.9,0,5,10000,2500,14,400,11,1.1,0,0,15";
            if (age > 50 && age <= 70)
                defaultNutrient =
                    "0,2200,3500,0,4700,700,1300,2.4,320,0,8,0,55,1.5,0,1.1,14,50,8,75,700,90,0.9,0,5,10000,2500,14,400,11,1.1,0,0,15";
            if (age > 70) defaultNutrient = "0,2200,3500,0,4700,700,1200,2.4,320,0,8,0,55,1.5,0,1.1,14,50,8,75,700,90,0.9,0,5,10000,2500,14,400,11,1.1,0,0,15";
        }
        else
        {
            if (age >= 14 && age < 19) defaultNutrient = "0,2600,3000,0,4700,900,1500,2.4,410,0,11,0,55,1.3,0,1.3,16,50,11,75,1250,75,0.9,0,5,10000,2500,14,400,9,1.2,0,0,15";
            if (age >= 19 && age <= 50) defaultNutrient = "0,3000,3500,0,4700,900,1500,2.4,420,0,11,0,55,1.3,0,1.3,16,50,8,90,700,120,0.9,0,5,10000,2500,14,400,11,1.2,0,0,15";
            if (age > 50 && age <= 70) defaultNutrient = "0,3000,3500,0,4700,900,1300,2.4,420,0,11,0,55,1.7,0,1.3,16,50,8,90,700,120,0.9,0,5,10000,2500,14,400,11,1.2,0,0,15";
            if (age > 70) defaultNutrient = "0,3000,3500,0,4700,900,1200,2.4,420,0,11,0,55,1.7,0,1.3,16,50,8,90,700,120,0.9,0,5,10000,2500,14,400,11,1.2,0,0,15";
        }

        var result = defaultNutrient.Split(',').ToList();
        var finalCalc = result.Select(s => new NotNegativeForDoubleTypes(Convert.ToDouble(s))).ToList();
        return finalCalc;
    }
}