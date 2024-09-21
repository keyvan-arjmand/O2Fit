namespace Payment.Web.Models;

public class MellatResult
{
    public string RefId { get; set; } = string.Empty;
    public string ResCode { get; set; } = string.Empty;
    public long SaleOrderId { get; set; }
    public string SaleReferenceId { get; set; } = string.Empty;
}