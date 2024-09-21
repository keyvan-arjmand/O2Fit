using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.DietCategoryAggregate;

namespace Food.V2.Application.DietCategories.V1.Queries.GetAllDietCategoryPagination;

public class
    GetAllDietCategoryPaginationQueryHandler : IRequestHandler<GetAllDietCategoryPaginationQuery,
        List<DietCategoryDto>>
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;

    public GetAllDietCategoryPaginationQueryHandler(IUnitOfWork work, IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
    }

    public async Task<List<DietCategoryDto>> Handle(GetAllDietCategoryPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var dietCats = await _work.GenericRepository<DietCategory>()
            .GetAllPaginationAsync(pageIndex: request.Page, pageSize: request.PageSize, cancellationToken);
        var list = dietCats.Data.ToDto<DietCategoryDto>().ToList();
        
        foreach (var i in list)
        {
            if (dietCats.Data.Any(x => x.Id == i.ParentId))
            {
                i.ParentDescription =
                    _mapper.Map<DietCategoryTranslation, TranslationDto>(dietCats.Data.Single(x => x.Id == i.ParentId)!
                        .Description);
                i.ParentName =
                    _mapper.Map<DietCategoryTranslation, TranslationDto>(dietCats.Data.Single(x => x.Id == i.ParentId)!
                        .Name);
            }
        }

        return list;
    }
}