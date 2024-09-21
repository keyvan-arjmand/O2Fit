namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseViewAndSubtractCostFromBudget;

public class
    IncreaseViewCountAndSubtractCostFromBudgetCommandHandler : IRequestHandler<IncreaseViewCountAndSubtractCostFromBudgetCommand>
{
    private readonly IUnitOfWork _uow;

    public IncreaseViewCountAndSubtractCostFromBudgetCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(IncreaseViewCountAndSubtractCostFromBudgetCommand request, CancellationToken cancellationToken)
    {
        var adminAdvertise = await _uow.GenericRepository<AdminAdvertise>().GetByIdAsync(request.Id, cancellationToken);
        if (adminAdvertise == null)
            throw new NotFoundException(nameof(AdminAdvertise), request.Id);

        if (adminAdvertise.Budget <= 0)
        {
            throw new BadRequestException("Budget is zero");
        }

        adminAdvertise.Budget -= new NotNegativeForDoubleTypes(adminAdvertise.Cost) ;
        adminAdvertise.ViewCount -= new NotNegativeForIntegerTypes(1);

        await _uow.GenericRepository<AdminAdvertise>().UpdateOneAsync(x => x.Id == request.Id, adminAdvertise,
            new Expression<Func<AdminAdvertise, object>>[]
            {
                a => a.Budget,
                a => a.ViewCount
            }, null, cancellationToken);
    }
}