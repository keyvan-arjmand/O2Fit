using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ordering.Service.Services.Payment.YekPay
{
    public class YekPayResult
    {
        [Required]
        public string Success { get; set; }
        [Required]
        public string Authority { get; set; }
    }
}
