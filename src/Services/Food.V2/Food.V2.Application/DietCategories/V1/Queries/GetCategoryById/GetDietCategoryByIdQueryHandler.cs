using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Application.Dtos.FoodCategory;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.CategoryAggregate;
using Food.V2.Domain.Aggregates.DietCategoryAggregate;

namespace Food.V2.Application.DietCategories.V1.Queries.GetCategoryById;

public class GetDietCategoryByIdQueryHandler : IRequestHandler<GetDietCategoryByIdQuery, DietCategoryDto>
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;
    public GetDietCategoryByIdQueryHandler(IUnitOfWork work, IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
    }

    public async Task<DietCategoryDto> Handle(GetDietCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<DietCategory>()
            .GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        var dietCategory = result.ToDto<DietCategoryDto>();
        if (!string.IsNullOrEmpty(result.ParentId.ToString()))
        {
            var parent = await _work.GenericRepository<DietCategory>()
                .GetByIdAsync(result.ParentId.ToString(), cancellationToken);
            dietCategory.ParentDescription =_mapper.Map<DietCategoryTranslation, TranslationDto>(parent.Description) ;
            dietCategory.ParentName = _mapper.Map<DietCategoryTranslation, TranslationDto>(parent.Name);
        }

        return dietCategory;
    }
}