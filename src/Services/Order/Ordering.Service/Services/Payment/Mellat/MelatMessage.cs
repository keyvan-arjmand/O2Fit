using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.Services.Payment.Mellat
{
    public class MelatMessage
    {
        public bool isError { get; set; }
        public string errorMsg { get; set; }
        public string succeedMsg { get; set; }
    }
}
