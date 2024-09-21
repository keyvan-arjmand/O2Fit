namespace Food.V2.Application.DietPacks.V1.Queries.GetDietPackById;

public class GetDietPackByIdQueryHandler: IRequestHandler<GetDietPackByIdQuery, DietPackDto>
{
    private readonly IUnitOfWork _uow;

    public GetDietPackByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<DietPackDto> Handle(GetDietPackByIdQuery request, CancellationToken cancellationToken)
    {
        var dietPack = await _uow.GenericRepository<DietPack>().GetByIdAsync(request.Id, cancellationToken);
        if (dietPack == null)
            throw new NotFoundException(nameof(DietPack), request.Id);

        return dietPack.ToDto<DietPackDto>();
    }
}