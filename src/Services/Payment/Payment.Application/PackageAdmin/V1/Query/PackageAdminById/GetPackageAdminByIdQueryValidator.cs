namespace Payment.Application.PackageAdmin.V1.Query.PackageAdminById;

public class GetPackageAdminByIdQueryValidator : AbstractValidator<GetPackageAdminByIdQuery>
{
    public GetPackageAdminByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be Empty").NotNull().WithMessage("Id can not be Null");
    }
}