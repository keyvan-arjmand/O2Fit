using MongoDB.Bson;

namespace Order.V2.Application.Orders.V1.Command.InsertOrder;

public class InsertOrderCommand:IRequest<string>
{
    public string WalletTransactionId { get; set; } = string.Empty;
    public string PaymentTransactionId { get; set; } = string.Empty;
    public string UserId { get;  set; } = string.Empty;
    public string Username { get;  set; } = string.Empty;
}