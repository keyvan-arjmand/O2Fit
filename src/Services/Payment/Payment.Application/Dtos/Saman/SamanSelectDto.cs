namespace Payment.Application.Dtos.Saman;

public class SamanSelectDto
{
    public string Token { get; set; }=string.Empty;
    public string RedirectUrl { get; set; } = string.Empty;
    public string PostUrl { get; set; } = string.Empty;
    public long BankOrderId { get; set; }
}