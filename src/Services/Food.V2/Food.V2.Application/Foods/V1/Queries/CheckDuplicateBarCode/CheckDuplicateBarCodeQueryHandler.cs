namespace Food.V2.Application.Foods.V1.Queries.CheckDuplicateBarCode;

public class CheckDuplicateBarCodeQueryHandler : IRequestHandler<CheckDuplicateBarCodeQuery , bool>
{
    private readonly IUnitOfWork _uow;

    public CheckDuplicateBarCodeQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(CheckDuplicateBarCodeQuery request, CancellationToken cancellationToken)
    {
        return _uow.GenericRepository<Domain.Aggregates.FoodAggregate.Food>().AnyAsync(
            x => x.BarcodeGs1 == request.BarcodeGs1 && x.BarcodeNational == request.BarcodeNational, cancellationToken);
    }
}