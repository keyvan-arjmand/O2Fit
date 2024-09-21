namespace Nutritionist.Application.NutritionistOrders.V1.Queries.GetAllNutritionistPendingOrders;

public class GetAllNutritionistPendingOrdersQueryHandler : IRequestHandler<GetAllNutritionistPendingOrdersQuery, List<NutritionistOrderDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllNutritionistPendingOrdersQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<NutritionistOrderDto>> Handle(GetAllNutritionistPendingOrdersQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<NutritionistOrder>.Filter.Eq(x => x.Status, OrderStatus.Pending);
        var allNutritionistOrders = await _uow.GenericRepository<NutritionistOrder>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return allNutritionistOrders.ToDto<NutritionistOrderDto>().ToList();
    }
}