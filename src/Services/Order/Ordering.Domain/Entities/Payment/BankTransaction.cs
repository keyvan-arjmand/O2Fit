using Domain;
using Ordering.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ordering.Domain.Entities.Payment
{
    public class BankTransaction : BaseEntity
    {
        public string ResNum { get; set; }
        public string RefNum { get; set; }
        public double Amount { get; set; }
        public float Wage { get; set; }
        public Bank? Bank { get; set; }
        public DateTime DateTime { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public string CID { get; set; }
        public string TraceNo { get; set; }
        public string SecurePan { get; set; }
        public string Authority { get; set; }
        public string SaleReferenceId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order.Order Order { get; set; }
    }
}
