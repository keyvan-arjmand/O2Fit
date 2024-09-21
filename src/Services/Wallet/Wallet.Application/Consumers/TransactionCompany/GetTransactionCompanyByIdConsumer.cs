using EventBus.Messages.Contracts.Services.Payments.Package;
using EventBus.Messages.Contracts.Services.Wallet;
using MassTransit;
using Wallet.Application.Common.Mapping;
using Wallet.Application.TransactionCompanies.V1.Query.GetTransactionCompanyById;

namespace Wallet.Application.Consumers.TransactionCompany;

public class GetTransactionCompanyByIdConsumer : IConsumer<GetTransactionCompanyById>
{
    private readonly IMediator _mediator;

    public GetTransactionCompanyByIdConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<GetTransactionCompanyById> context)
    {
        var transaction = await _mediator.Send(new GetTransactionCompanyByIdCommand
        {
            Id = context.Message.Id,
        });

        if (transaction != null)
        {
            await context.RespondAsync<GetTransactionCompanyByIdResult>(new
                GetTransactionCompanyByIdResult
            {
                FinalAmount = transaction.FinalAmount,
                UserId = transaction.UserId,
                WalletTransactionId = transaction.WalletTransactionId,
                PackageId = transaction.PackageId,
                UserType = transaction.UserType,
                Amount = transaction.Amount,
                Discount = transaction.Discount,
                CurrencyCode = transaction.CurrencyCode,
                CurrencyId = transaction.CurrencyId,
                Wage = transaction.Wage,
                DiscountCode = transaction.DiscountCode,
                Bank = transaction.Bank,
                DateTime = transaction.DateTime,
                PaymentFor = transaction.PaymentFor,
                Id = transaction.Id,
                WalletId = transaction.WalletId,
                CountryId = transaction.CountryId,
                SaleReferenceId = transaction.SaleReferenceId,
                DiscountType = transaction.DiscountType,
                PackageType = transaction.PackageType,
                PaymentTransactionId = transaction.PaymentTransactionId,
                NetIncome = transaction.NetIncome,
                Income = transaction.Income,
                TransactionType = transaction.TransactionType,
                TraceNo = transaction.TraceNo,
                DebtToTheNutritionist = transaction.DebtToTheNutritionist,
                BankOrderId = transaction.BankOrderId,
                ExchangeCurrencyId = transaction.ExchangeCurrencyId,
                ExchangeCurrencyCode = transaction.ExchangeCurrencyCode,
                ExchangeRate = transaction.ExchangeRate,
                FinalAmountExchange = transaction.FinalAmountExchange,
                NutritionistId = transaction.NutritionistId,
                ValueAdded = transaction.ValueAdded,
                OrderId = transaction.OrderId,
                DurationPackage = transaction.DurationPackage,
            });
        }
        else
        {
            await context.RespondAsync<GetTransactionCompanyByIdResult>(new GetTransactionCompanyByIdResult());
        }
    }
}