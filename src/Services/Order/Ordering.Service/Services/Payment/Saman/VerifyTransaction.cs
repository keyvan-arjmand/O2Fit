using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Service.Services.Payment.Saman
{
    public static class VerifyTransaction
    {
        public static Task<double> Check(string RefNum)
        {
            SamanVerify.PaymentIFBindingSoap paymentIFBindingSoap = new SamanVerify.PaymentIFBindingSoapClient(SamanVerify.PaymentIFBindingSoapClient.EndpointConfiguration.PaymentIFBindingSoap);
            var result = paymentIFBindingSoap.verifyTransactionAsync(RefNum, SepConfig.MerchantId);

            return result;
        }
    }
}
