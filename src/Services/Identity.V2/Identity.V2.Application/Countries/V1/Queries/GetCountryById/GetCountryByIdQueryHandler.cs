namespace Identity.V2.Application.Countries.V1.Queries.GetCountryById;

public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryDto>
{
    private readonly IUnitOfWork _uow;

    public GetCountryByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<CountryDto> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await _uow.GenericRepository<Country>().GetByIdAsync(request.Id, cancellationToken);
        if (country == null)
            throw new NotFoundException(nameof(Country), request.Id);
        return country.ToDto<CountryDto>();
    }
}