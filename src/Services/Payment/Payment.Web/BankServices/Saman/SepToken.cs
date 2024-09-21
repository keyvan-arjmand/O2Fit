namespace Payment.Web.BankServices.Saman;

public static class SepToken
{
    /// <summary>
    /// Sep Token
    /// </summary>
    /// <param name="ResNum">Factor Number</param>
    /// <param name="Amount">Totall Amount Factor</param>
    /// <returns></returns>
    public static async Task<string> Get(string ResNum, string Amount)
    {
        SamanBank.PaymentIFBindingSoap paymentIFBindingSoap = new SamanBank.PaymentIFBindingSoapClient(SamanBank.PaymentIFBindingSoapClient.EndpointConfiguration.PaymentIFBindingSoap);
        string token = await paymentIFBindingSoap.RequestTokenAsync(SepConfig.MerchantId, ResNum, long.Parse(Amount), 0, 0, 0, 0, 0, 0, "", "", 0, SepConfig.RedirectUrl);
        return token;
    }
}