namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseClickCount;

public class IncreaseClickCountCommandHandler : IRequestHandler<IncreaseClickCountCommand>
{
    private readonly IUnitOfWork _uow;

    public IncreaseClickCountCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(IncreaseClickCountCommand request, CancellationToken cancellationToken)
    {
        var adminAdvertise = await _uow.GenericRepository<AdminAdvertise>().GetByIdAsync(request.Id, cancellationToken);
        if (adminAdvertise == null)
            throw new NotFoundException(nameof(AdminAdvertise), request.Id);
        
        adminAdvertise.ClickCount+= new NotNegativeForIntegerTypes(1);
        
        await _uow.GenericRepository<AdminAdvertise>().UpdateOneAsync(x => x.Id == request.Id, adminAdvertise,
            new Expression<Func<AdminAdvertise, object>>[]
            {
                a => a.ClickCount
            }, null, cancellationToken);
        
    }
}