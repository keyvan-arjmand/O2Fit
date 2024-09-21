namespace Payment.Application.Dtos.Mellat;

public class MellatMessage
{
    public bool IsError { get; set; }
    public string ErrorMsg { get; set; } = string.Empty;
    public string SucceedMsg { get; set; } = string.Empty;
}