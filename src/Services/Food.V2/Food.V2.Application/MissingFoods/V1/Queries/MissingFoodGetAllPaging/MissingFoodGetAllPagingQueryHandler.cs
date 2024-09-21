using Food.V2.Application.Dtos.MissingFood;
using Food.V2.Domain.Aggregates.MissingFoodAggregate;

namespace Food.V2.Application.MissingFoods.V1.Queries.MissingFoodGetAllPaging;

public class
    MissingFoodGetAllPagingQueryHandler : IRequestHandler<MissingFoodGetAllPagingQuery,
        PaginationResult<MissingFoodDto>>
{
    private readonly IUnitOfWork _work;

    public MissingFoodGetAllPagingQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<PaginationResult<MissingFoodDto>> Handle(MissingFoodGetAllPagingQuery request,
        CancellationToken cancellationToken)
    {
        var recipe = await _work.GenericRepository<MissingFood>()
            .GetAllPaginationAsync(request.Page, request.PageSize, cancellationToken);
        var dtoResult = recipe.Data.ToDto<MissingFoodDto>().ToList();
        return PaginationResult<MissingFoodDto>.CreatePaginationResult(request.Page, request.PageSize,
            dtoResult.Count, dtoResult);
    }
}