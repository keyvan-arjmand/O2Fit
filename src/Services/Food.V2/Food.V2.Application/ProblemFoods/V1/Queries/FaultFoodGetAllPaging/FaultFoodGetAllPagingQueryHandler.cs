using Food.V2.Application.Dtos.ProblemFood;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.FaultFoodAggregate;

namespace Food.V2.Application.ProblemFoods.V1.Queries.FaultFoodGetAllPaging;

public class FaultFoodGetAllPagingQueryHandler : IRequestHandler<FaultFoodGetAllPagingQuery,
    PaginationResult<FaultFoodDto>>
{
    private readonly IUnitOfWork _work;

    public FaultFoodGetAllPagingQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<PaginationResult<FaultFoodDto>> Handle(FaultFoodGetAllPagingQuery request,
        CancellationToken cancellationToken)
    {
        var report = await _work.GenericRepository<FaultFood>()
            .GetAllPaginationAsync(request.Page, request.PageSize, cancellationToken);
        var dtoResult = report.Data.ToDto<FaultFoodDto>().ToList();
        if (report.Count > 0)
        {
            foreach (var i in dtoResult)
            {
                var food = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
                    .GetByIdAsync(i.FoodId, cancellationToken);
                if (food != null)
                {
                    i.FoodTranslation = food.Name.MapTo<TranslationDto, FoodTranslation>();
                }
            }
        }

        return PaginationResult<FaultFoodDto>.CreatePaginationResult(request.Page, request.PageSize,
            dtoResult.Count, dtoResult);
    }
}