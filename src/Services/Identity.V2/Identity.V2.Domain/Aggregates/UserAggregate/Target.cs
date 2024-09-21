namespace Identity.V2.Domain.Aggregates.UserAggregate;

public class Target : BaseEntity
{
    public Target()
    {
        
    }
    public Target(string id)
    {
        Id = id;
    }
    //all not negetive
    public NonNegativeForIntegerTypes TargetStep { get; set; } = new(0);
    public NotNegativeForDoubleTypes TargetWeight { get; set; } = new(0);
    public NotNegativeForDoubleTypes TargetChest { get; set; } = new(0);
    public NotNegativeForDoubleTypes TargetArm { get; set; } = new(0);
    public NotNegativeForDoubleTypes TargetWaist { get; set; } = new(0);
    public NotNegativeForDoubleTypes TargetHighHip { get; set; }= new(0);
    public NotNegativeForDoubleTypes TargetThighSize { get; set; } = new(0);
    public NotNegativeForDoubleTypes TargetNeckSize { get; set; } = new(0);
    public NotNegativeForDoubleTypes TargetHip { get; set; } = new(0);
    public NotNegativeForDoubleTypes TargetShoulder { get; set; } = new(0);
    public NotNegativeForDoubleTypes TargetWrist { get; set; } = new(0);
    public List<NotNegativeForDoubleTypes> TargetNutrient { get; set; }
    public NotNegativeForDoubleTypes TargetWater { get; set; }= new(0);
}