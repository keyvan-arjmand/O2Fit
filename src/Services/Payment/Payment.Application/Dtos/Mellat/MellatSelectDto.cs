namespace Payment.Application.Dtos.Mellat;

public class MellatSelectDto
{
    public bool IsError { get; set; }
    public string RefId { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public string RedirectUrl { get; set; } = string.Empty;
    public string PostUrl { get; set; } = string.Empty;
    public long BankOrderId { get; set; }

}