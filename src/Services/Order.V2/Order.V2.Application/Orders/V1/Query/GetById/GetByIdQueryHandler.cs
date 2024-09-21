using Order.V2.Application.Common.Exceptions;
using Order.V2.Application.Common.Interfaces.Persistence.UoW;
using Order.V2.Application.Common.Mapping;
using Order.V2.Application.Dtos;

namespace Order.V2.Application.Orders.V1.Query.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, OrderDto>
{
    private readonly IUnitOfWork _uow;
    public GetByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<OrderDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _uow.GenericRepository<Domain.Aggregates.OrderAggregate.Order>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (order == null) throw new NotFoundException("order not found");
        return order.ToDto<OrderDto>();
    }
}