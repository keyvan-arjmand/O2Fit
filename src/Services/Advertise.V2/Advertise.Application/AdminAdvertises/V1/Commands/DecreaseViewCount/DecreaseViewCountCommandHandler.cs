namespace Advertise.Application.AdminAdvertises.V1.Commands.DecreaseViewCount;

public class DecreaseViewCountCommandHandler : IRequestHandler<DecreaseViewCountCommand>
{
    private readonly IUnitOfWork _uow;

    public DecreaseViewCountCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DecreaseViewCountCommand request, CancellationToken cancellationToken)
    {
        var adminAdvertise = await _uow.GenericRepository<AdminAdvertise>().GetByIdAsync(request.Id, cancellationToken);
        if (adminAdvertise == null)
            throw new NotFoundException(nameof(AdminAdvertise), request.Id);
        if (adminAdvertise.ViewCount <= 0)
            throw new BadRequestException("View count is zero");
        
        adminAdvertise.ViewCount-= new NotNegativeForIntegerTypes(1);
        if (adminAdvertise.ViewCount <= 0)
        {
            adminAdvertise.Status = AdvertiseStatus.OutOfBudget;
            await _uow.GenericRepository<AdminAdvertise>().UpdateOneAsync(x => x.Id == request.Id, adminAdvertise,
                new Expression<Func<AdminAdvertise, object>>[]
                {
                    a => a.ViewCount,
                    a=>a.Status
                }, null, cancellationToken);    
        }
        else
        {
            await _uow.GenericRepository<AdminAdvertise>().UpdateOneAsync(x => x.Id == request.Id, adminAdvertise,
                new Expression<Func<AdminAdvertise, object>>[]
                {
                    a => a.ViewCount
                }, null, cancellationToken);    
        }
    }
}