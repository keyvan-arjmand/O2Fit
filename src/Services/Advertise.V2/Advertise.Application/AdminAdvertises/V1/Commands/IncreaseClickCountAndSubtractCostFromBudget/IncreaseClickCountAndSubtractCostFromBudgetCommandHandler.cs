namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseClickCountAndSubtractCostFromBudget;

public class
    IncreaseClickCountAndSubtractCostFromBudgetCommandHandler : IRequestHandler<
        IncreaseClickCountAndSubtractCostFromBudgetCommand>
{
    private readonly IUnitOfWork _uow;

    public IncreaseClickCountAndSubtractCostFromBudgetCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(IncreaseClickCountAndSubtractCostFromBudgetCommand request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<AdminAdvertise>.Filter.Eq(x => x.Id, request.Id);
        filter &= Builders<AdminAdvertise>.Filter.Eq(x => x.Status, AdvertiseStatus.Active);
        filter &= Builders<AdminAdvertise>.Filter.Eq(x => x.IsDelete, false);
        var adminAdvertise = await _uow.GenericRepository<AdminAdvertise>().GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (adminAdvertise == null)
            throw new NotFoundException(nameof(AdminAdvertise), request.Id);


        adminAdvertise.Budget -= new NotNegativeForDoubleTypes(adminAdvertise.Cost);
        adminAdvertise.ClickCount += new NotNegativeForIntegerTypes(1);
        if (adminAdvertise.Budget <= 0)
        {
            adminAdvertise.Status = AdvertiseStatus.OutOfBudget;
            await _uow.GenericRepository<AdminAdvertise>().UpdateOneAsync(x => x.Id == request.Id, adminAdvertise,
                new Expression<Func<AdminAdvertise, object>>[]
                {
                    a => a.Budget,
                    a => a.ClickCount,
                    a => a.Status
                }, null, cancellationToken);
        }
        else
        {
            await _uow.GenericRepository<AdminAdvertise>().UpdateOneAsync(x => x.Id == request.Id, adminAdvertise,
                new Expression<Func<AdminAdvertise, object>>[]
                {
                    a => a.Budget,
                    a => a.ClickCount
                }, null, cancellationToken);
        }
    }
}