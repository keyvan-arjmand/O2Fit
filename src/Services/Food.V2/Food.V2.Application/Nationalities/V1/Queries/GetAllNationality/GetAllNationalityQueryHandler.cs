using Food.V2.Application.Dtos.Nationality;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.CategoryAggregate;
using Food.V2.Domain.Aggregates.NationalityAggregate;

namespace Food.V2.Application.Nationalities.V1.Queries.GetAllNationality;

public class GetAllNationalityQueryHandler : IRequestHandler<GetAllNationalityQuery, List<NationalityDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllNationalityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<NationalityDto>> Handle(GetAllNationalityQuery request, CancellationToken cancellationToken)
    {
        var nationalities = await _unitOfWork.GenericRepository<Nationality>().GetAllAsync(cancellationToken);
        var list = nationalities.ToDto<NationalityDto>().ToList();
        if (nationalities.Count > 0)
        {
            foreach (var i in list)
            {
                if (nationalities.Any(x => x.Id == i.ParentId))
                {
                    i.ParentTranslation =
                        _mapper.Map<NationalityTranslation, TranslationDto>(
                            nationalities.Single(x => x.Id == i.ParentId)!.Translation);
                }
            }
        }

        return list;
    }
}