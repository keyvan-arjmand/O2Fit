using Common.Enums.TypeEnums;
using EventBus.Messages.Events.Services.Wallet;
using MassTransit;
using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Application.TransactionCompanies.V1.Command.CreateTransactionComp;
using Wallet.Application.TransactionCompanies.V1.Query.GetTransactionCompanyById;
using Wallet.Application.Transactions.V1.Commands.ChargeWalletTransaction;
using Wallet.Application.Wallets.V1.Command.WalletCharge;
using Wallet.Application.Wallets.V1.Query.GetWalletById;
using Wallet.Application.Wallets.V1.Query.GetWalletByUserId;

namespace Wallet.Application.Consumers.Wallet;

public class RejectedOrderConsumer : IConsumer<WalletChargedUserEvent>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _uow;

    public RejectedOrderConsumer(IMediator mediator, IUnitOfWork uow)
    {
        _mediator = mediator;
        _uow = uow;
    }
    public async Task Consume(ConsumeContext<WalletChargedUserEvent> context)
    {
        var transactionComp = await _mediator.Send(new GetTransactionCompanyByIdCommand
        {
            Id = context.Message.TransactionId
        });
        if (transactionComp == null) throw new NotFoundException("Transaction not found");

        var wallet = await _mediator.Send(new GetWalletByUserIdQuery
        {
            UserId = transactionComp.UserId
        });
        if (wallet == null) throw new NotFoundException($"wallet not found For User :{transactionComp.NutritionistId}");

        var commandComp = new TransactionCompanyCommand
        {
            Amount = transactionComp.Amount,
            Bank = "Wallet",
            BankOrderId = 0,
            CountryId = transactionComp.CountryId,
            CurrencyId = transactionComp.CurrencyId,
            CurrencyCode = transactionComp.CurrencyCode,
            DebtToTheNutritionist = 0,
            Discount = 0,
            DiscountCode = string.Empty,
            DiscountType = string.Empty,
            ExchangeCurrencyId = transactionComp.ExchangeCurrencyId,
            ExchangeCurrencyCode = transactionComp.ExchangeCurrencyCode,
            ExchangeRate = transactionComp.ExchangeRate,
            FinalAmount = transactionComp.FinalAmount,
            FinalAmountExchange = transactionComp.FinalAmountExchange,
            Income = 0,
            NetIncome = 0,
            NutritionistId = transactionComp.NutritionistId,
            OrderId = context.Message.OrderId,
            PackageId = transactionComp.PackageId,
            PackageType = transactionComp.PackageType,
            PaymentFor = PaymentType.WalletCharge.ToDescription(),
            PaymentTransactionId = transactionComp.PaymentTransactionId,
            SaleReferenceId = transactionComp.SaleReferenceId,
            TraceNo = transactionComp.TraceNo,
            TransactionType = TransactionType.OrderRejected.ToDescription(),
            UserId = transactionComp.UserId,
            UserType = UserType.User.ToDescription(),
            WalletId = wallet.Id,
            ValueAdded = 0,
            Wage = 0,
            UserName = context.Message.Username,
            WalletTransactionId = transactionComp.WalletTransactionId
        };
        var commandTr = new ChargeWalletTransactionCommand();

        if (transactionComp.FinalAmountExchange == null || transactionComp.FinalAmountExchange == 0)
        {
            commandTr = new ChargeWalletTransactionCommand
            {
                WalletId = commandComp.WalletId,
                Amount = commandComp.FinalAmount,
                Bank = commandComp.Bank,
                CurrencyId = commandComp.CurrencyId,
                CurrencyCode = commandComp.CurrencyCode,
                PaymentType = PaymentType.WalletCharge,
                SaleReferenceId = transactionComp.SaleReferenceId,
                UserId = commandComp.UserId,
                UserType = commandComp.UserType.ToEnum<UserType>(),
                UserName = context.Message.Username
            };
        }
        else
        {
            commandTr = new ChargeWalletTransactionCommand
            {
                WalletId = commandComp.WalletId,
                Amount = (double)commandComp.FinalAmountExchange,
                Bank = commandComp.Bank,
                CurrencyId = commandComp.ExchangeCurrencyId,
                CurrencyCode = commandComp.ExchangeCurrencyCode,
                PaymentType = PaymentType.WalletCharge,
                SaleReferenceId = transactionComp.SaleReferenceId,
                UserId = commandComp.UserId,
                UserType = commandComp.UserType.ToEnum<UserType>(),
                UserName = context.Message.Username,
            };
        }
        var trId = await _mediator.Send(commandTr);
        commandComp.WalletTransactionId = trId;

        await _mediator.Send(commandComp);

        await _mediator.Send(new WalletChargeCommand
        {
            Amount = commandTr.Amount,
            CurrencyCode = commandTr.CurrencyCode,
            PaymentType = commandTr.PaymentType,
            UserId = commandTr.UserId,
            PackageType = transactionComp.PackageType.ToEnum<PackageType>(),
            Id = wallet.Id,
            Duration = transactionComp.DurationPackage,
            UserName = context.Message.Username
        });
    }
}