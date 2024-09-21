using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage;
using MassTransit;
using Order.V2.Application.Common.Interfaces.Persistence.UoW;
using Order.V2.Application.Common.Mapping;
using Order.V2.Application.Dtos;

namespace Order.V2.Application.Orders.V1.Query.GetAllOrder;

public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, List<OrderDto>>
{
    private readonly IUnitOfWork _uow;
    public GetAllOrderQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        var orders =await _uow.GenericRepository<Domain.Aggregates.OrderAggregate.Order>().GetAllAsync(cancellationToken);
        return orders.ToDto<OrderDto>().ToList();
    }
}