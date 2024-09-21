using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.Services.Payment.Saman
{
    public class SamanResult
    {
        public string State { get; set; }
        public string StateCode { get; set; }
        public string ResNum { get; set; }
        public string MID { get; set; }
        public string RefNum { get; set; }
        public string CID { get; set; }
        public string TRACENO { get; set; }
        public string SecurePan { get; set; }
    }
}
