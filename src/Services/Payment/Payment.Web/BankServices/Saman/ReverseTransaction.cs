namespace Payment.Web.BankServices.Saman
{
    public static class ReverseTransaction
    {
        public static void Send(string refNum, string Mid)
        {
            SamanBankVerify.PaymentIFBindingSoap paymentIFBindingSoap = new SamanBankVerify.PaymentIFBindingSoapClient(SamanBankVerify.PaymentIFBindingSoapClient.EndpointConfiguration.PaymentIFBindingSoap);
            paymentIFBindingSoap.reverseTransactionAsync(refNum, Mid, SepConfig.MerchantId, SepConfig.PassMid);
        }
    }
}
