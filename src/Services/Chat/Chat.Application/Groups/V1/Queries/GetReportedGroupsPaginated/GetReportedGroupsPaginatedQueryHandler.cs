namespace Chat.Application.Groups.V1.Queries.GetReportedGroupsPaginated;

public class GetReportedGroupsPaginatedQueryHandler : IRequestHandler<GetReportedGroupsPaginatedQuery, PaginationResult<GetGroupPaginatedDto>>
{
    private readonly IUnitOfWork _uow;

    public GetReportedGroupsPaginatedQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<GetGroupPaginatedDto>> Handle(GetReportedGroupsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Group>.Filter.Eq(x => x.IsReported, true);

        var groupsPaginated = await _uow.GenericRepository<Group>()
            .GetListPaginationOfDocumentsByFilterAsync(request.Page, request.PageSize, filter, cancellationToken);

        var dtoData = groupsPaginated.Data.ToDto<GetGroupPaginatedDto>().ToList();
        return PaginationResult<GetGroupPaginatedDto>.CreatePaginationResult(request.Page, request.PageSize,
            groupsPaginated.Count, dtoData);
    }
}