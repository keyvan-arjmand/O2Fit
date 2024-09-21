namespace Advertise.Application.AdminAdvertises.V1.Queries.GetAllActiveAdminAdvertise;

public class GetAllActiveAdminAdvertiseQueryValidator : AbstractValidator<GetAllActiveAdminAdvertiseQuery>
{
    public GetAllActiveAdminAdvertiseQueryValidator()
    {
        RuleFor(x => x.Language).IsInEnum();
    }
}