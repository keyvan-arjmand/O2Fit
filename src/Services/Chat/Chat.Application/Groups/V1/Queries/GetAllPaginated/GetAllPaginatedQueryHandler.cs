namespace Chat.Application.Groups.V1.Queries.GetAllPaginated;

public class GetAllPaginatedQueryHandler : IRequestHandler<GetAllPaginatedQuery, PaginationResult<GetGroupPaginatedDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllPaginatedQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<GetGroupPaginatedDto>> Handle(GetAllPaginatedQuery request, CancellationToken cancellationToken)
    {
        var data = await _uow.GenericRepository<Group>()
            .GetAllPaginationAsync(request.Page, request.PageSize, cancellationToken);

        var dtoData = data.Data.ToDto<GetGroupPaginatedDto>().ToList();

        return PaginationResult<GetGroupPaginatedDto>.CreatePaginationResult(request.Page, request.PageSize, data.Count,
            dtoData);
    }
}