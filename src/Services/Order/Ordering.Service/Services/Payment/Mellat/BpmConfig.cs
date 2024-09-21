using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.Services.Payment.Mellat
{
    public static class BpmConfig
    {
        public static string TerminalID { get { return "5231565"; } }
        public static string UserName { get { return "Oxygen1398"; } }
        public static string Password { get { return "33321922"; } }
        public static string PostUrl { get { return "https://bpm.shaparak.ir/pgwchannel/startpay.mellat"; } }
        public static string RedirectUrl { get { return "https://bank.o2fitt.com/home/melatback"; } }

    }
}
