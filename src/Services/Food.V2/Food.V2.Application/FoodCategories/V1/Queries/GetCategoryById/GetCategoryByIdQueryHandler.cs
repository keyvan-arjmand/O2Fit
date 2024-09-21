using Food.V2.Application.Dtos.FoodCategory;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.CategoryAggregate;

namespace Food.V2.Application.FoodCategories.V1.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, FoodCategoryDto>
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IUnitOfWork work, IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
    }

    public async Task<FoodCategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<Category>()
            .GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (result == null) throw new NotFoundException("FoodCategory not Found");
        var category = result.ToDto<FoodCategoryDto>();

        if (result.ParentId != ObjectId.Empty)
        {
            var parent = await _work.GenericRepository<Category>()
                .GetByIdAsync(result.ParentId.ToString(), cancellationToken);
            category.ParentTranslation =_mapper.Map<CategoryTranslation, TranslationDto>(parent.Translation) ;
        }

        return category;
    }
}