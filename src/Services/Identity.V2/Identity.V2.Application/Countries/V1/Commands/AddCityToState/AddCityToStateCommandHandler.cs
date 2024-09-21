using State = Identity.V2.Domain.Aggregates.CountryAggregate.State;

namespace Identity.V2.Application.Countries.V1.Commands.AddCityToState;

public class AddCityToStateCommandHandler : IRequestHandler<AddCityToStateCommand>
{
    private readonly IUnitOfWork _uow;

    public AddCityToStateCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(AddCityToStateCommand request, CancellationToken cancellationToken)
    {
        var country = await _uow.GenericRepository<Country>().GetByIdAsync(request.CountryId, cancellationToken);
        if (country == null)
            throw new NotFoundException(nameof(Country), request.CountryId);

        var state = country.States.FirstOrDefault(x => x.Id == request.StateId);
        if (state == null)
            throw new NotFoundException(nameof(State), request.StateId);

        var name = request.Name.ToEntity<Translation>();
        var city = new City
        {
            Name = name
        };
        
        state.Cities.Add(city);

        await _uow.GenericRepository<Country>().UpdateOneAsync(x => x.Id == request.CountryId, country,
            new Expression<Func<Country, object>>[]
            {
                c => c.States
            }, null, cancellationToken);
    }
}