namespace Discount.Application.Dtos;

public class CurrencyResult
{
    public string Id { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public List<int> CountryIds { get; set; } = new();
    public double CoefficientCurrency { get; set; }
}