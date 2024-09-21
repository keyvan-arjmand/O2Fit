namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateUserProfileTarget;

public class UpdateUserProfileTargetCommand: IRequest
{
    public string UserId { get; set; } = string.Empty; 
    public double TargetWeight { get; set; } 
    public double WeightChangeRate { get; set; } 
    public int TargetStep { get; set; }

    public double TargetChest { get; set; }

    public double TargetArm { get; set; }

    public double TargetWaist { get; set; }

    public double TargetHighHip { get; set; }

    public double TargetThighSize { get; set; }

    public double TargetNeckSize { get; set; }

    public double TargetHip { get; set; }

    public double TargetShoulder { get; set; }

    public double TargetWrist { get; set; }

    public List<double> TargetNutrient { get; set; }
    public double TargetWater { get; set; }

    // public DailyActivityRate DailyActivityRate { get; set; } 
}