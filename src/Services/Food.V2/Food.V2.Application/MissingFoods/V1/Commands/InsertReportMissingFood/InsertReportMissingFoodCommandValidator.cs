namespace Food.V2.Application.MissingFoods.V1.Commands.InsertReportMissingFood;

public class InsertReportMissingFoodCommandValidator:AbstractValidator<InsertReportMissingFoodCommand>
{
    public InsertReportMissingFoodCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name cannot null or empty");
        RuleFor(x => x.Barcode).NotNull().NotEmpty().WithMessage("Barcode cannot null or empty");
    }
}