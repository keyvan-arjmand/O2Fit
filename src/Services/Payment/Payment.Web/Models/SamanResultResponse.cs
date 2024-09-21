namespace Payment.Web.Models;

public class SamanResultResponse:BaseApi
{
    public SamanMessage Data { get; set; } = default!;
}
public class SamanMessage
{
    public bool IsError { get; set; }
    public string ErrorMsg { get; set; } = string.Empty;
    public string SucceedMsg { get; set; } = string.Empty;
}