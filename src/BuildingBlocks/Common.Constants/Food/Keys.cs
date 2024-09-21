using System.Transactions;

namespace Common.Constants.Food;

public class Keys
{
    public const string TransactionKey = "Translation.";
    public const string TransactionNameKey = "Name.";
    public const double BurnedCalorieMultiplication = 0.00061809;
    public const string MeasureUnitGrams = "6506b671850c34448898af40";
    
    //bodyType
    public const double EctoMorphFatValue = 0.3 / 9;
    public const double EctoMorphProteinValue = 0.5 / 4;
    public const double EctoMorphCarbohydrateValue = 0.2 / 4;
    
    public const double MesoMorphFatValue = 0.3 / 9;
    public const double MesoMorphProteinValue = 0.3 / 4;
    public const double MesoMorphCarbohydrateValue = 0.4 / 4;
    
    public const double EndoMorphFatValue =  0.4 / 9;
    public const double EndoMorphProteinValue = 0.35 / 4;
    public const double EndoMorphCarbohydrateValue = 0.25 / 4;
    
    public const double DefaultFatValue = 3 * 4;
    public const double DefaultProteinValue = 3 * 4;
    public const double DefaultCarbohydrateValue = 3 * 9;
    
    
    ///MeasureUnit Ids
    public const string Grams = "6506b671850c34448898af40";
    public const string Pounds ="6506b654850c34448898af3e";
    public const string Ounces ="6506b62b850c34448898af3c";


}