using Food.V2.Application.Dtos.Nationality;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.NationalityAggregate;

namespace Food.V2.Application.Nationalities.V1.Queries.GetAllNationalityPagination;

public class GetAllNationalityPaginationQueryHandler:IRequestHandler<GetAllNationalityPaginationQuery,List<NationalityDto>>
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;

    public GetAllNationalityPaginationQueryHandler(IUnitOfWork work, IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
    }

    public async Task<List<NationalityDto>> Handle(GetAllNationalityPaginationQuery request, CancellationToken cancellationToken)
    {
        var nationality = await _work.GenericRepository<Nationality>()
            .GetAllPaginationAsync(pageIndex: request.Page, pageSize: request.PageSize, cancellationToken);
        var list = nationality.Data.ToDto<NationalityDto>().ToList();
        
        foreach (var i in list)
        {
            if (nationality.Data.Any(x => x.Id == i.ParentId))
            {
                i.ParentTranslation =
                    _mapper.Map<NationalityTranslation, TranslationDto>(nationality.Data.Single(x => x.Id == i.ParentId)!
                        .Translation);
            }
        }
        return list;
    }
}