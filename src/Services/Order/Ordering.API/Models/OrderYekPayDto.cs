using Ordering.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ordering.Domain.Enum;
using WebFramework.Api;

namespace Ordering.API.Models
{
    public class OrderYekPayDto : BaseDto<OrderYekPayDto,Order>
    {
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public bool IsDiscountActive { get; set; }
        public int? DiscountId { get; set; }
        public DiscountUser? DiscountUser { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        public string Description { get; set; }
    }
}
