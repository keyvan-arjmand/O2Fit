namespace Identity.V2.Application.NutritionistProfiles.V1.Commands.CreateNutritionistProfile;

public class CreateNutritionistProfileCommandValidator : AbstractValidator<CreateNutritionistProfileCommand>
{
    public CreateNutritionistProfileCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty").NotNull().WithMessage("UserId can not be null");

        RuleFor(x => x.Gender).IsInEnum();
        RuleFor(x => x.ProfileImage).NotEmpty().WithMessage("ProfileImageName can not be empty").NotNull().WithMessage("ProfileImageName can not be null");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName can not be empty").NotNull().WithMessage("FirstName can not be null");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName can not be empty").NotNull()
            .WithMessage("LastName can not be null");
        RuleFor(x => x.ScientificDegree).IsInEnum();
        RuleFor(x => x.MedicalSystemNumber).NotEmpty().WithMessage("MedicalSystemNumber can not be empty").NotNull().WithMessage("MedicalSystemNumber can not be null");
        RuleFor(x => x.ExperienceYear).GreaterThanOrEqualTo(0)
            .WithMessage("ExperienceYear should be greater than or equal zero");
        RuleFor(x => x.LicenseImage).NotEmpty().WithMessage("LicenseImageName can not be empty").NotNull().WithMessage("LicenseImageName can not be null");
       // RuleFor(x => x.OtherDocumentsImageName).NotEmpty().WithMessage("OtherDocumentsImageName can not be empty").NotNull().WithMessage("OtherDocumentsImageName can not be null");
        RuleFor(x => x.AboutTheSpecialist).NotEmpty().WithMessage("AboutTheSpecialist can not be empty").NotNull().WithMessage("AboutTheSpecialist can not be null");
        RuleFor(x => x.CategoryOfFitnessGoal).IsInEnum();
        RuleForEach(x => x.DietTypeIds).NotEmpty().WithMessage("DietType can not be empty").NotNull().WithMessage("DietType can not be null");

    }
}