using Domain;
using Ordering.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Entities.Wallet
{
    public class WalletHistory : BaseEntity
    {
        public int UserId { get; set; }
        public float Amount { get; set; }
        public Currency Currency{get; set;}
        public DateTime InsertDate { get; set; }
    }
}
