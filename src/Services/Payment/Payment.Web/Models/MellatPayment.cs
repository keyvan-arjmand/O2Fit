namespace Payment.Web.Models;

public class MellatPayment : BaseApi
{
    public MellatGetApi Data { get; set; } = default!;
}
public class MellatGetApi
{
    public bool IsError { get; set; }
    public string RefId { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public string RedirectUrl { get; set; } = string.Empty;
    public string PostUrl { get; set; } = string.Empty;
    public long BankOrderId { get; set; }

}