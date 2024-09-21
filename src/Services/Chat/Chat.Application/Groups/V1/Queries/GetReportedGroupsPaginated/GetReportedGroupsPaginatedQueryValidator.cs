namespace Chat.Application.Groups.V1.Queries.GetReportedGroupsPaginated;

public class GetReportedGroupsPaginatedQueryValidator : AbstractValidator<GetReportedGroupsPaginatedQuery>
{
    public GetReportedGroupsPaginatedQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page must be greater than zero");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than zero");
    }
}