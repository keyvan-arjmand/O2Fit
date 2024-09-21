using Ordering.Domain.Enum;

namespace Ordering.API.Models
{
   public class OrderDto
   {
      public int UserId { get; set; }
      public int PackageId { get; set; }
      public bool IsDiscountActive { get; set; }
      public int? DiscountId { get; set; }
      public DiscountUser? DiscountUser { get; set; }

   }
}
