using State = Identity.V2.Domain.Aggregates.CountryAggregate.State;

namespace Identity.V2.Application.Countries.V1.Commands.EditStateInCountry;

public class EditStateInCountryCommandHandler : IRequestHandler<EditStateInCountryCommand>
{
    private readonly IUnitOfWork _uow;

    public EditStateInCountryCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(EditStateInCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _uow.GenericRepository<Country>().GetByIdAsync(request.CountryId, cancellationToken);
        if (country == null)
            throw new NotFoundException(nameof(Country), request.CountryId);
        
        var state = country.States.FirstOrDefault(x => x.Id == request.StateId);
        if (state == null)
            throw new NotFoundException(nameof(State), request.StateId);

        state.Name = request.Name.ToEntity<Translation>();

        await _uow.GenericRepository<Country>().UpdateOneAsync(x => x.Id == request.CountryId, country,
            new Expression<Func<Country, object>>[]
            {
                c => c.States
            },null,cancellationToken);
    }
}