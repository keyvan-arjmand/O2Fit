namespace Payment.Web.BankServices.Saman
{
    public class SamanMessage
    {
        public bool IsError { get; set; }
        public string ErrorMsg { get; set; } = string.Empty;
        public string SucceedMsg { get; set; } = string.Empty;
    }
}
