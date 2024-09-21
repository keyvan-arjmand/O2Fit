using Domain;
using Ordering.Domain.Entities.Discount;
using Ordering.Domain.Entities.Payment;
using Ordering.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Authentication;
using System.Text;

namespace Ordering.Domain.Entities.Order
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDate { get; set; }

        public int? DiscountId { get; set; }
        [ForeignKey(nameof(DiscountId))]
        public Discount.Discount Discount { get; set; }

        public DiscountUser? DiscountUser { get; set; }

        public int PackageId { get; set; }
        [ForeignKey(nameof(PackageId))]
        public Package.Package Package { get; set; }

        public BankTransaction BankTransaction { get; set; }

        public DateTime ExpireTime { get; set; }

        public string RefId { get; set; }



      #region YekPay
      public string Email { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string Authority { get; set; }
        #endregion
    }
}
