namespace Food.V2.Application.Foods.V1.Queries.GetFoodByBarcode;

public class GetFoodByBarcodeQueryValidator : AbstractValidator<GetFoodByBarcodeQuery>
{
    public GetFoodByBarcodeQueryValidator()
    {
        RuleFor(x=>x.Barcode).NotEmpty().NotNull().WithMessage("Barcode cannot null or empty");
    }

}