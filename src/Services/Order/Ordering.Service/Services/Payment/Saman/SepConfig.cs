using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.Services.Payment.Saman
{
    public static class SepConfig
    {
        public static string MerchantId { get { return "11877913"; } }
        public static string RedirectUrl { get { return "https://bank.o2fitt.com/home/samanback"; } }
        //public static string RedirectUrl { get { return "https://localhost:44353/home/samanback"; } }
        public static string PostUrl { get { return "https://sep.shaparak.ir/payment.aspx"; } }
        public static string PassMid { get { return "8665167"; } }
    }
}
