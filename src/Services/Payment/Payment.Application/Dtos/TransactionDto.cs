namespace Payment.Application.Dtos;

public class TransactionDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string? PackageId { get; set; }
    public string? NutritionistId { get; set; }
    public string? PackageType { get; set; }
    public int? DurationPackage { get; set; }
    public int CountryId { get; set; }
    public long BankOrderId { get; set; } //sequence Value                
    public double Amount { get; set; }
    public double Wage { get; set; }
    public double Discount { get; set; }

    public double FinalAmount { get; set; }

    //when amount exchange => wallet currency                             
    public double? FinalAmountExchange { get; set; }
    public double? ExchangeRate { get; set; } //rate exchange             
    public string? ExchangeCurrencyId { get; set; }
    public string? ExchangeCurrencyCode { get; set; }

    public string? DiscountCode { get; set; }
    public string? DiscountType { get; set; }
    public string CurrencyId { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public string FinalState { get; set; } = string.Empty; //enum         
    public string Bank { get; set; } = string.Empty; //bank enum          
    public string PaymentFor { get; set; } = string.Empty; //paymentType E
    public string UserType { get; set; } = string.Empty; //UserType Enum  
    public DateTime CreateTime { get; set; }
    public DateTime PayTime { get; set; }

    public string? ResNum { get; set; }
    public string? RefNum { get; set; }
    public string? State { get; set; }
    public string? StateCode { get; set; }
    public string? CId { get; set; }
    public string? TraceNo { get; set; }
    public string? SecurePan { get; set; }
    public string? Authority { get; set; }
    public string? SaleReferenceId { get; set; }
}