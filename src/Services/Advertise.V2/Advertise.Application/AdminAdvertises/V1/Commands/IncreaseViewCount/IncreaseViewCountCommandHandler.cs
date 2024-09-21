namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseViewCount;

public class IncreaseViewCountCommandHandler : IRequestHandler<IncreaseViewCountCommand>
{
    private readonly IUnitOfWork _uow;

    public IncreaseViewCountCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(IncreaseViewCountCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<AdminAdvertise>.Filter.Eq(x => x.Id, request.Id);
        filter &= Builders<AdminAdvertise>.Filter.Eq(x => x.Status, AdvertiseStatus.Active);
        filter &= Builders<AdminAdvertise>.Filter.Eq(x => x.IsDelete, false);
        var adminAdvertise = await _uow.GenericRepository<AdminAdvertise>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (adminAdvertise == null)
            throw new NotFoundException(nameof(AdminAdvertise), request.Id);

        adminAdvertise.ViewCount+= new NotNegativeForIntegerTypes(1);

        await _uow.GenericRepository<AdminAdvertise>().UpdateOneAsync(x => x.Id == request.Id, adminAdvertise,
            new Expression<Func<AdminAdvertise, object>>[]
            {
                x => x.ViewCount
            }, null, cancellationToken);

    }
}