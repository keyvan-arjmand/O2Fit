namespace Identity.V2.Application.Countries.V1.Commands.UpdateUnCountryInfo;

public class UpdateUnCountryInfoCommandHandler : IRequestHandler<UpdateUnCountryInfoCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateUnCountryInfoCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateUnCountryInfoCommand request, CancellationToken cancellationToken)
    {
        var country = await _uow.GenericRepository<Country>().GetByIdAsync(request.Id, cancellationToken);
        if (country == null)
            throw new NotFoundException(nameof(Country), request.Id);

        country.Alpha = request.Alpha;
        country.Culture = request.Culture;
        country.CountryCode = request.CountryCode;
        country.CurrencyName = request.CurrencyName;
        country.CurrencyCode = request.CurrencyCode;
        country.UtcTime = request.UtcTime;
        await _uow.GenericRepository<Country>().UpdateOneAsync(x => x.Id == request.Id, country,
            new Expression<Func<Country, object>>[]
            {
                x => x.Alpha,
                x => x.Culture,
                x => x.CountryCode,
                x => x.CurrencyName,
                x => x.CurrencyCode,
                x => x.UtcTime
            }, null, cancellationToken);
    }
}