namespace Order.V2.Application.Dtos;

public class OrderDto
{
    public string UserId { get; set; } = string.Empty;
    public double Amount { get; set; }
    public double Wage { get; set; }
    public double Discount { get; set; }
    public double FinalAmount { get; set; }
    public DateTime CreateDate { get; set; }
    public string BankTransactionId { get; set; } = string.Empty;

    public string? DiscountPackageId { get; set; }
    public string? DiscountCode { get; set; }
    public string PackageId { get; set; } = string.Empty;

    public string SaleReferenceId { get; set; } = string.Empty;
    public string TraceNo { get; set; } = string.Empty;
}