namespace EventBus.Messages.Contracts.Services.Wallet;

public class GetTransactionCompanyByIdResult
{
    public string Id { get; init; }
    public string UserId { get; init; }
    public string NutritionistId { get; init; } //when order Accepted
    public string OrderId { get; init; } //when order Accepted or accept
    public string WalletId { get; init; }
    public string WalletTransactionId { get; init; }
    public string PaymentTransactionId { get; init; }
    public string PackageId { get; init; }
    public string PackageType { get; init; }
    public int DurationPackage { get; init; }
    public string CurrencyId { get; init; }
    public string CurrencyCode { get; init; }
    public int CountryId { get; init; }
    public DateTime DateTime { get; init; }
    public double Amount { get; init; } //مبلغ کل
    public double FinalAmount { get; init; }
    //when amount exchange currency
    public double FinalAmountExchange { get; init; }
    public double ExchangeRate { get; init; } //rate exchange
    public string ExchangeCurrencyId { get; init; }
    public string ExchangeCurrencyCode { get; init; } 
    //commission - Payment For Package 
    public double NetIncome { get; init; } //درآمد خالص-ارزش افزوده
    public double Income { get; init; } //درآمد ناخالص+ارزش افزوده وکمیسیون کمپانی
    public double ValueAdded { get; init; } //مالیات
    public double DebtToTheNutritionist { get; init; } //بدهی به متخصص

    //Package
    public double Wage { get; init; }
    public double Discount { get; init; }
    public string DiscountCode { get; init; }
    //type
    public string DiscountType { get; init; }
    public string UserType { get; init; } //enum UserType
    public string PaymentFor { get; init; } //enum PaymentType
    public string TransactionType { get; init; } //enum TransactionType
    public string Bank { get; init; } //enum bank
    //bank params
    public string SaleReferenceId { get; init; }
    public string TraceNo { get; init; }
    public long BankOrderId { get; init; }

}