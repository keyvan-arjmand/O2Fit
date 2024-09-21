using Order.V2.Domain.Enums;

namespace Order.V2.Application.Orders.V1.Command.UpdateOrder;

public class UpdateOrderCommand:IRequest
{
    public string Id { get; set; } = string.Empty;
    public OrderState OrderState { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}