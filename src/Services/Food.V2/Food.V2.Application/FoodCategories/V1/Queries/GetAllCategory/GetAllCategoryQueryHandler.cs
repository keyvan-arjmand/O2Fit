using Food.V2.Application.Dtos.FoodCategory;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.CategoryAggregate;

namespace Food.V2.Application.FoodCategories.V1.Queries.GetAllCategory;

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<FoodCategoryDto>>
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;

    public GetAllCategoryQueryHandler(IUnitOfWork work, IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
    }

    public async Task<List<FoodCategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _work.GenericRepository<Category>().GetAllAsync(cancellationToken);
        var list = categories.ToDto<FoodCategoryDto>().ToList();
        if (categories.Count > 0)
        {
            foreach (var i in list)
            {
                if (categories.Any(x => x.Id == i.ParentId))
                {
                    i.ParentTranslation =_mapper.Map<CategoryTranslation, TranslationDto>(categories.Single(x => x.Id == i.ParentId)!.Translation) ;
                }
            }
        }

        return list;
    }
}