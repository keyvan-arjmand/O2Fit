using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.Services.Payment.YekPay
{
    public static class YekPayConfig
    {
        public static string callback { get { return "https://bank.o2fitt.com/home/yekpayback"; } }
        public static string requestUrl { get { return "https://gate.yekpay.com/api/payment/request"; } }
        public static string paymentUrl { get { return "https://gate.yekpay.com/api/payment/start/"; } }
        public static string verifyUrl { get { return "https://gate.yekpay.com/api/payment/verify"; } }
        public static string merchant { get { return "DWP8EZFVPGDR3D628QK9SUTTY4KXB29G"; } }
        public static string fcc { get { return "978"; } }
        public static string tcc { get { return "978"; } }
    }
}
