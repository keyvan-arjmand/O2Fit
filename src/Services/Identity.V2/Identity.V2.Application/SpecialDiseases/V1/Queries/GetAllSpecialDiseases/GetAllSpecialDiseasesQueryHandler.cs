using SpecialDisease = Identity.V2.Domain.Aggregates.SpecialDiseaseAggregate.SpecialDisease;

namespace Identity.V2.Application.SpecialDiseases.V1.Queries.GetAllSpecialDiseases;

public class GetAllSpecialDiseasesQueryHandler : IRequestHandler<GetAllSpecialDiseasesQuery,List<SpecialDiseaseDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllSpecialDiseasesQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<SpecialDiseaseDto>> Handle(GetAllSpecialDiseasesQuery request, CancellationToken cancellationToken)
    {
        var allSpecialDiseases = await _uow.GenericRepository<SpecialDisease>().GetAllAsync(cancellationToken);
        return allSpecialDiseases.ToDto<SpecialDiseaseDto>().ToList();
    }
}