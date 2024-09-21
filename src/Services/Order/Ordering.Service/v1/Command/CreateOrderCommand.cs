using MediatR;
using Ordering.Domain.Entities.Order;
using Ordering.Domain.Enum;

namespace Ordering.Service.v1.Command
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public bool IsDiscountActive { get; set; }
        public int? DiscountId { get; set; }
        public DiscountUser? DiscountUser { get; set; }
   }
}
