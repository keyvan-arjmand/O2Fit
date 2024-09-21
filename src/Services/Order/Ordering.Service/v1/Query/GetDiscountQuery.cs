using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Ordering.Domain.Enum;

namespace Ordering.Service.v1.Query
{
    public class GetDiscountQuery : IRequest<DiscountSelectDto>
    {
        public int UserId { get; set; }
        public string Code { get; set; }
        public int PackageId { get; set; }
    }

    public class DiscountSelectDto
    {
        public int UserId { get; set; }
        public double Amount { get; set; }
        public int PackageId { get; set; }
        public int? DiscountId { get; set; }
        public DiscountUser? DiscountUser { get; set; }
        public bool IsActive { get; set; }
    }
}
