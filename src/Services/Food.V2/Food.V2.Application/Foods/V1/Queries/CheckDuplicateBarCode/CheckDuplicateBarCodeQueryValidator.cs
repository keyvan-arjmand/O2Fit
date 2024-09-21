namespace Food.V2.Application.Foods.V1.Queries.CheckDuplicateBarCode;

public class CheckDuplicateBarCodeQueryValidator : AbstractValidator<CheckDuplicateBarCodeQuery>
{
    public CheckDuplicateBarCodeQueryValidator()
    {
        RuleFor(x=>x.BarcodeGs1).NotEmpty().WithMessage("BarcodeGs1 can not be empty")
            .NotNull().WithMessage("BarcodeGs1 can not be null");

        RuleFor(x=>x.BarcodeNational).NotEmpty().WithMessage("BarcodeNational can not be empty")
            .NotNull().WithMessage("BarcodeNational can not be null");

    }
}