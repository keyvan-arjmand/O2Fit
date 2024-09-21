namespace Payment.Web.Models;

public class SamanPayment:BaseApi
{
    public SamanGetApi Data { get; set; } = default!;
}
public class SamanGetApi
{
    public string Token { get; set; } = string.Empty;
    public string RedirectUrl { get; set; } = string.Empty;
    public string PostUrl { get; set; } = string.Empty;
}