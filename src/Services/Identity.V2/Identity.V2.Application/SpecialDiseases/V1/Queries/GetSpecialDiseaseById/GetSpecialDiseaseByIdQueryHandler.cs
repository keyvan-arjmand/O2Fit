namespace Identity.V2.Application.SpecialDiseases.V1.Queries.GetSpecialDiseaseById;

public class GetSpecialDiseaseByIdQueryHandler : IRequestHandler<GetSpecialDiseaseByIdQuery, SpecialDiseaseDto>
{
    private readonly IUnitOfWork _uow;

    public GetSpecialDiseaseByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<SpecialDiseaseDto> Handle(GetSpecialDiseaseByIdQuery request, CancellationToken cancellationToken)
    {
        var specialDisease = await _uow.GenericRepository<SpecialDisease>().GetByIdAsync(request.Id, cancellationToken);
        if (specialDisease == null)
            throw new NotFoundException(nameof(SpecialDisease), request.Id);

        return specialDisease.ToDto<SpecialDiseaseDto>();
    }
}