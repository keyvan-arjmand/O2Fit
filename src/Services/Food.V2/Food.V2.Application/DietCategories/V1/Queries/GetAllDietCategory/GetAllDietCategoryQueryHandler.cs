using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.DietCategoryAggregate;

namespace Food.V2.Application.DietCategories.V1.Queries.GetAllDietCategory;

public class GetAllDietCategoryQueryHandler : IRequestHandler<GetAllDietCategoryQuery, List<DietCategoryDto>>
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;

    public GetAllDietCategoryQueryHandler(IUnitOfWork work, IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
    }

    public async Task<List<DietCategoryDto>> Handle(GetAllDietCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<DietCategory>.Filter.Eq(x => x.IsActive, true);
        var dietCategory = await _work.GenericRepository<DietCategory>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        if (dietCategory.Count <= 0) throw new NotFoundException("dietCategory Not Found");
        var list = dietCategory.ToDto<DietCategoryDto>().ToList();
        
        foreach (var i in list)
        {
            if (dietCategory.Any(x => x.Id == i.ParentId))
            {
                i.ParentDescription = _mapper.Map<DietCategoryTranslation, TranslationDto>(dietCategory.Single(x => x.Id == i.ParentId)!.Description) ;
                i.ParentName = _mapper.Map<DietCategoryTranslation, TranslationDto>(dietCategory.Single(x => x.Id == i.ParentId)!.Name);
            }
        }
        return list;
    }
}