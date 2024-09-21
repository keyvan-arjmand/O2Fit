namespace Identity.V2.Application.Countries.V1.Queries.GetAllCountries;

public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, List<CountryDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllCountriesQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var result = await _uow.GenericRepository<Country>().GetAllAsync(cancellationToken);
        return result.ToDto<CountryDto>().ToList();
    }
}