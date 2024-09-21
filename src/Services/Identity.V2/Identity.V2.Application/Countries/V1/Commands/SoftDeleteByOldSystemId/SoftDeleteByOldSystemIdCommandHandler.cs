namespace Identity.V2.Application.Countries.V1.Commands.SoftDeleteByOldSystemId;

public class SoftDeleteByOldSystemIdCommandHandler : IRequestHandler<SoftDeleteByOldSystemIdCommand>
{
    private readonly IUnitOfWork _uow;

    public SoftDeleteByOldSystemIdCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(SoftDeleteByOldSystemIdCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Country>.Filter.Eq(x => x.CountryId, request.Id);
        var country = await _uow.GenericRepository<Country>().GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (country == null)
            throw new  NotFoundException(nameof(Country), request.Id);

        country.IsDelete = true;
        await _uow.GenericRepository<Country>().SoftDeleteByIdAsync(country.Id, country, null, cancellationToken);
    }
}