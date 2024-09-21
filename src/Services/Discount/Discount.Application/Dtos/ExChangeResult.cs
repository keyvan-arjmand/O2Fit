namespace Discount.Application.Dtos;

public class ExChangeResult
{
    public string SourceCurrencyCode { get; set; } = string.Empty;
    public double SourceCurrencyAmount { get; set; }
    public string DestinationCurrencyCode { get; set; } = string.Empty;
    public double DestinationCurrencyAmount { get; set; }
    public double ExRate { get; set; }
}