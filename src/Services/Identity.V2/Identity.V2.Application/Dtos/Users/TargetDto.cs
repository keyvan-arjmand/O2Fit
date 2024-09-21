namespace Identity.V2.Application.Dtos.Users;

public class TargetDto : IDto
{
    public int? TargetStep { get; set; } 
    public double? TargetWeight { get; set; } 
    public double? TargetChest { get; set; } 
    public double? TargetArm { get; set; } 
    public double? TargetWaist { get; set; } 
    public double? TargetHighHip { get; set; }
    public double? TargetThighSize { get; set; } 
    public double? TargetNeckSize { get; set; } 
    public double? TargetHip { get; set; } 
    public double? TargetShoulder { get; set; } 
    public double? TargetWrist { get; set; } 
    public List<double> TargetNutrient { get; set; } = null!;
    public double? TargetWater { get; set; }
}