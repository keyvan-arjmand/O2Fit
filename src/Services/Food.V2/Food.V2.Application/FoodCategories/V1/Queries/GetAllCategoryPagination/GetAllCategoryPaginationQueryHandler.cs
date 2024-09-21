using Food.V2.Application.Dtos.FoodCategory;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.CategoryAggregate;

namespace Food.V2.Application.FoodCategories.V1.Queries.GetAllCategoryPagination;

public class GetAllCategoryPaginationQueryHandler : IRequestHandler<GetAllCategoryPaginationQuery, List<FoodCategoryDto>>
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;

    public GetAllCategoryPaginationQueryHandler(IUnitOfWork work, IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
    }

    public async Task<List<FoodCategoryDto>> Handle(GetAllCategoryPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var foodCats = await _work.GenericRepository<Category>()
            .GetAllPaginationAsync(pageIndex: request.Page, pageSize: request.PageSize, cancellationToken);
        var list = foodCats.Data.ToDto<FoodCategoryDto>().ToList();

        foreach (var i in list)
        {
            if (foodCats.Data.Any(x => x.Id == i.ParentId))
            {
                i.ParentTranslation =
                    _mapper.Map<CategoryTranslation, TranslationDto>(foodCats.Data.Single(x => x.Id == i.ParentId)!
                        .Translation);
            }
        }
        return list;
    }
}