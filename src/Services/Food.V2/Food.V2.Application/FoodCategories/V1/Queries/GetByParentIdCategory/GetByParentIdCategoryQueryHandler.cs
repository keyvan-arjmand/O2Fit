using Food.V2.Application.Dtos.FoodCategory;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.CategoryAggregate;

namespace Food.V2.Application.FoodCategories.V1.Queries.GetByParentIdCategory;

public class GetByParentIdCategoryQueryHandler : IRequestHandler<GetByParentIdCategoryQuery, List<FoodCategoryDto>>
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;

    public GetByParentIdCategoryQueryHandler(IUnitOfWork work, IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
    }

    public async Task<List<FoodCategoryDto>> Handle(GetByParentIdCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var parent = await _work.GenericRepository<Category>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (parent == null) throw new NotFoundException("Parent Not Found");

        var filter = Builders<Category>.Filter.Eq(x => x.ParentId, ObjectId.Parse(request.Id));
        var category = await _work.GenericRepository<Category>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);

        var list = category.ToDto<FoodCategoryDto>().ToList();
        if (category.Count > 0)
        {
            foreach (var i in list)
            {
                if (category.Any(x => x.Id == i.ParentId))
                {
                    i.ParentTranslation =_mapper.Map<CategoryTranslation, TranslationDto>(category.Single(x => x.Id == i.ParentId)!.Translation) ;
                }
            }
        }

        return list;
    }
}