namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateBodyShapes;

public class UpdateBodyShapesCommandValidator : AbstractValidator<UpdateBodyShapesCommand>
{
    public UpdateBodyShapesCommandValidator()
    {
        RuleFor(x=>x.TargetNeckSize).NotEmpty().WithMessage("TargetNeckSize can not be empty")
            .NotNull().WithMessage("TargetNeckSize can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetNeckSize is 0");
        
        RuleFor(x=>x.HeightSize).NotEmpty().WithMessage("HeightSize can not be empty")
            .NotNull().WithMessage("HeightSize can not be null").GreaterThanOrEqualTo(60).WithMessage("Minimum value of height size is 60 cm")
            .LessThanOrEqualTo(230).WithMessage("Maximum value of height size is 230 cm");
        
        RuleFor(x=>x.TargetThighSize).NotEmpty().WithMessage("TargetThighSize can not be empty")
            .NotNull().WithMessage("TargetThighSize can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetThighSize is 0");

        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty")
            .NotNull().WithMessage("UserId can not be null");
        
        RuleFor(x=>x.TargetArm).NotEmpty().WithMessage("TargetArm can not be empty")
            .NotNull().WithMessage("TargetArm can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetArm is 0");
        
        RuleFor(x=>x.TargetChest).NotEmpty().WithMessage("TargetChest can not be empty")
            .NotNull().WithMessage("TargetChest can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetChest is 0");
        
        RuleFor(x=>x.TargetHip).NotEmpty().WithMessage("TargetHip can not be empty")
            .NotNull().WithMessage("TargetHip can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetHip is 0");
        
        RuleFor(x=>x.TargetShoulder).NotEmpty().WithMessage("TargetShoulder can not be empty")
            .NotNull().WithMessage("TargetShoulder can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetShoulder is 0");
        
        RuleFor(x=>x.TargetWaist).NotEmpty().WithMessage("TargetWaist can not be empty")
            .NotNull().WithMessage("TargetWaist can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetWaist is 0");
        
        RuleFor(x=>x.TargetWrist).NotEmpty().WithMessage("TargetWrist can not be empty")
            .NotNull().WithMessage("TargetWrist can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetWrist is 0");
        
        RuleFor(x=>x.TargetHighHip).NotEmpty().WithMessage("TargetHighHip can not be empty")
            .NotNull().WithMessage("TargetHighHip can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("Minimum value of TargetHighHip is 0");
    }
}