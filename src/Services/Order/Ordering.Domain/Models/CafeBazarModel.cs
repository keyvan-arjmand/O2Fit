using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Models
{
  public  class CafeBazarModel
    {
        public int UserId { get; set; }
        public double Price { get; set; }
        public int PackageId { get; set; }
        public bool IsSuccess { get; set; }
        public Domain.Enum.Bank Bank { get; set; }
        public string SaleReferenceId { get; set; }//شماره پیگیری
        public string Language { get; set; }
    }
}
