
namespace Payment.Web.BankServices.Saman;

public static class VerifyTransaction
{
    public static async Task<double> Check(string RefNum)
    {
        SamanBankVerify.PaymentIFBindingSoap paymentIFBindingSoap = new SamanBankVerify.PaymentIFBindingSoapClient(SamanBankVerify.PaymentIFBindingSoapClient.EndpointConfiguration.PaymentIFBindingSoap);
        return await paymentIFBindingSoap.verifyTransactionAsync(RefNum, SepConfig.MerchantId);
    }
}