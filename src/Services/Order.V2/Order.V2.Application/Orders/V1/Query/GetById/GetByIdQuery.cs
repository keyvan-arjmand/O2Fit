using Order.V2.Application.Dtos;

namespace Order.V2.Application.Orders.V1.Query.GetById;

public class GetByIdQuery : IRequest<OrderDto>
{
    public string Id { get; set; } = string.Empty;
}