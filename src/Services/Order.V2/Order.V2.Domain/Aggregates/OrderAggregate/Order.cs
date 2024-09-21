using Mongo.Migration.Documents;
using Mongo.Migration.Documents.Attributes;
using Order.V2.Domain.Common;

namespace Order.V2.Domain.Aggregates.OrderAggregate;

[RuntimeVersion("0.0.2")]
//[StartUpVersion("0.0.1")]
[CollectionLocation("orders", "order")]
public class Order : AggregateRoot, IDocument
{
    public ObjectId UserId { get; set; }
    public ObjectId NutritionistId { get; set; }
    public double Amount { get; set; } //مبلغ پکیج
    public double Wage { get; set; } //مبلغ تخفیف پکیج
    public double Discount { get; set; } //مبلغ کد تخفیف
    public double FinalAmount { get; set; } //مبلغ نهایی
    public DateTime InsertDate { get; set; }
    public ObjectId WalletTransactionId { get; set; }
    public ObjectId PaymentTransactionId { get; set; }
    public ObjectId CurrencyId { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string? DiscountCode { get; set; }
    public ObjectId PackageId { get; set; }
    public string PackageType { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public string SaleReferenceId { get; set; } = string.Empty;
    public string TraceNo { get; set; } = string.Empty;
    public string PaymentFor { get; set; } = string.Empty; //paymentType Enum
    public string DiscountType { get; set; } = string.Empty; //paymentType Enum
    public string UserType { get; set; } = string.Empty; //UserType Enum
    public string FinalState { get; set; } = string.Empty; //Order State Enum
}