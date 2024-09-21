using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.Services.Payment.Saman
{
    public static class ReverseTransaction
    {
        public static void Send(string refNum, string Mid)
        {
            SamanVerify.PaymentIFBindingSoap paymentIFBindingSoap = new SamanVerify.PaymentIFBindingSoapClient(SamanVerify.PaymentIFBindingSoapClient.EndpointConfiguration.PaymentIFBindingSoap);
            paymentIFBindingSoap.reverseTransactionAsync(refNum, Mid, SepConfig.MerchantId, SepConfig.PassMid);
        }
    }
}
