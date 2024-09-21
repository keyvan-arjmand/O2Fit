using State = Identity.V2.Domain.Aggregates.CountryAggregate.State;

namespace Identity.V2.Application.Countries.V1.Commands.AddStateToCountry;

public class AddStateToCountryCommandHandler : IRequestHandler<AddStateToCountryCommand>
{
    private readonly IUnitOfWork _uow;

    public AddStateToCountryCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(AddStateToCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _uow.GenericRepository<Country>().GetByIdAsync(request.CountryId, cancellationToken);
        if (country == null)
            throw new NotFoundException(nameof(Country), request.CountryId);
        var name = request.Name.ToEntity<Translation>();
        var state = new State
        {
            Name = name
        };
        country.States.Add(state);

        await _uow.GenericRepository<Country>().UpdateOneAsync(x => x.Id == request.CountryId, country,
            new Expression<Func<Country, object>>[]
            {
                c => c.States
            },null,cancellationToken);

    }
}