namespace Payment.Web.Models;

public class MellatResultResponse : BaseApi
{
    public MellatMessage Data { get; set; } = default!;
}
public class MellatMessage
{
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public string SucceedMessage { get; set; } = string.Empty;
}