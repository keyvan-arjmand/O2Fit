using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.FAQCategoryAggregate;

namespace Market.Application.FaqCategories.V1.Queries.GetFaqCategoryAllPagination;

public class GetAllPaginationQueryHandler : IRequestHandler<GetAllPaginationQuery, PaginationResult<FaqCategoryDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllPaginationQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<PaginationResult<FaqCategoryDto>> Handle(GetAllPaginationQuery request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<FaqCategory>()
            .GetAllPaginationAsync(request.Page, request.PageSize, cancellationToken);
      return PaginationResult<FaqCategoryDto>.CreatePaginationResult(request.Page, request.PageSize,
            result.Data.Count, result.Data.ToDto<FaqCategoryDto>().ToList());
    }
}