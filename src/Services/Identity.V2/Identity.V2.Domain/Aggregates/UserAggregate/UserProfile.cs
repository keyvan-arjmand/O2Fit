namespace Identity.V2.Domain.Aggregates.UserAggregate;

public class UserProfile: BaseEntity
{
    public UserProfile()
    {
        
    }
    public UserProfile(string id)
    {
        Id = id;
    }
    //public ObjectId UserId { get; set; }
    //50
    public string FullName { get; set; } = string.Empty;
    public string? ImageUri { get; set; }
    public double? WeightChangeRate { get; set; }
    public FoodHabit FoodHabit { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    //not negative and up to 999
    public WeightTimeRange? WeightTimeRange { get; set; }
    //not negative from 60 to 230
    public HeightSize? HeightSize { get; set; }
    public DailyActivityRate DailyActivityRate { get; set; }
    public DateTime? PkExpireDate { get; set; }
    public DateTime? DietPkExpireDate { get; set; }
    //public string? ReferralCount { get; set; } => always null in old system
    //public string? BonusCount { get; set; }
    public bool FastingMode { get; set; }
    public bool IsPurchase { get; set; }

    //public ObjectId DeviceInformationId { get; set; }
    //public ObjectId WalletId { get; set; }
    public Target Target { get; set; } = new Target(ObjectId.GenerateNewId().ToString());
    public List<NutritionistList> NutritionistsList { get; set; } = new List<NutritionistList>();

}