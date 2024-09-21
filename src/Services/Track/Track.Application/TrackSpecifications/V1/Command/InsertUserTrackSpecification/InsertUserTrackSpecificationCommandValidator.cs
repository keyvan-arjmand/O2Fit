namespace Track.Application.TrackSpecifications.V1.Command.InsertUserTrackSpecification;

public class InsertUserTrackSpecificationCommandValidator:AbstractValidator<InsertUserTrackSpecificationCommand>
{
    public InsertUserTrackSpecificationCommandValidator()
    {
        RuleFor(x => x.AppId).NotEmpty().WithMessage("AppId is empty").NotNull().WithMessage("AppId is Null");
        RuleFor(x => x.InsertDate).NotEmpty().WithMessage("InsertDate is empty").NotNull().WithMessage("InsertDate is Null");
    }
}