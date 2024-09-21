using MongoDB.Bson;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Domain.Aggregates.TransactionCompanyAggregate;

namespace Wallet.Application.TransactionCompanies.V1.Command.CreateTransactionComp;

public class TransactionCompanyCommandHandler : IRequestHandler<TransactionCompanyCommand, string>
{
    private readonly IUnitOfWork _uow;

    public TransactionCompanyCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<string> Handle(TransactionCompanyCommand request, CancellationToken cancellationToken)
    {
        var transaction = new TransactionCompany
        {
            Amount = request.Amount,
            Bank = request.Bank,
            CurrencyId = new ObjectId(request.CurrencyId),
            CurrencyCode = request.CurrencyCode,
            DateTime = DateTime.Now,
            DebtToTheNutritionist = request.DebtToTheNutritionist,
            Discount = request.Discount,
            DiscountCode = request.DiscountCode,
            FinalAmount = request.FinalAmount,
            Income = request.Income,
            NetIncome = request.NetIncome,
            PackageId = string.IsNullOrEmpty(request.PackageId)
                ? ObjectId.Empty
                : new ObjectId(request.PackageId),
            PaymentFor = request.PaymentFor,
            WalletTransactionId = new ObjectId(request.WalletTransactionId),
            UserId = new ObjectId(request.UserId),
            UserType = request.UserType,
            ValueAdded = request.ValueAdded,
            Wage = request.Wage,
            WalletId = new ObjectId(request.WalletId),
            SaleReferenceId = request.SaleReferenceId,
            TraceNo = request.TraceNo,
            CountryId = request.CountryId,
            BankOrderId = request.BankOrderId,
            DiscountType = request.DiscountType,
            ExchangeCurrencyId = string.IsNullOrEmpty(request.ExchangeCurrencyId)
                ? ObjectId.Empty
                : new ObjectId(request.ExchangeCurrencyId),
            ExchangeCurrencyCode = request.ExchangeCurrencyCode,
            ExchangeRate = request.ExchangeRate,
            FinalAmountExchange = request.FinalAmountExchange,
            NutritionistId = string.IsNullOrEmpty(request.NutritionistId)
                ? ObjectId.Empty
                : new ObjectId(request.NutritionistId),
            OrderId = string.IsNullOrEmpty(request.OrderId)
                ? ObjectId.Empty
                : new ObjectId(request.OrderId),
            TransactionType = request.TransactionType,
            PaymentTransactionId = new ObjectId(request.PaymentTransactionId),
            PackageType = request.PackageType,
            Created = DateTime.UtcNow,
            CreatedBy = request.UserName,
            CreatedById = ObjectId.Parse(request.UserId)
        };
        await _uow.GenericRepository<TransactionCompany>()
            .InsertOneAsync(transaction, null, cancellationToken);
        return transaction.Id;
    }
}