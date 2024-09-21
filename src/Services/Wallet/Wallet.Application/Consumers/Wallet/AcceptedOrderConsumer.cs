using EventBus.Messages.Events.Services.Wallet;
using MassTransit;
using MongoDB.Bson;
using System.Threading;
using Common.Enums.TypeEnums;
using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Application.Common.Utilities;
using Wallet.Application.TransactionCompanies.V1.Command.CreateTransactionComp;
using Wallet.Application.TransactionCompanies.V1.Query.GetTransactionCompanyById;
using Wallet.Application.Wallets.V1.Command.WalletCharge;
using Wallet.Application.Wallets.V1.Query.GetWalletById;
using Wallet.Domain.Aggregates.TransactionCompanyAggregate;
using Wallet.Application.Transactions.V1.Commands.ChargeWalletTransaction;
using Wallet.Application.Wallets.V1.Query.GetWalletByUserId;

namespace Wallet.Application.Consumers.Wallet;

public class AcceptedOrderConsumer : IConsumer<WalletChargedNutritionistEvent>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _uow;

    public AcceptedOrderConsumer(IMediator mediator, IUnitOfWork uow)
    {
        _mediator = mediator;
        _uow = uow;
    }

    public async Task Consume(ConsumeContext<WalletChargedNutritionistEvent> context)
    {
        var transactionComp = await _mediator.Send(new GetTransactionCompanyByIdCommand
        {
            Id = context.Message.TransactionId
        });
        if (transactionComp == null)
            throw new NotFoundException($"transaction not found :{context.Message.TransactionId}");
        var wallet = await _mediator.Send(new GetWalletByUserIdQuery
        {
            UserId = transactionComp.NutritionistId
        });
        if (wallet == null) throw new NotFoundException($"wallet not found For User :{transactionComp.NutritionistId}");

        var commandComp = new TransactionCompanyCommand
        {
            Amount = transactionComp.Amount,
            Bank = "Wallet",
            BankOrderId = transactionComp.BankOrderId,
            CountryId = transactionComp.CountryId,
            CurrencyId = transactionComp.CurrencyId,
            CurrencyCode = transactionComp.CurrencyCode,
            DebtToTheNutritionist = transactionComp.Discount > 0
                ? (transactionComp.Amount - transactionComp.Discount).DebtToTheNutritionist()
                : transactionComp.Amount.DebtToTheNutritionist(),
            Discount = transactionComp.Discount,
            DiscountCode = transactionComp.DiscountCode,
            DiscountType = transactionComp.DiscountType,
            ExchangeCurrencyId = transactionComp.ExchangeCurrencyId,
            ExchangeCurrencyCode = transactionComp.ExchangeCurrencyCode,
            ExchangeRate = transactionComp.ExchangeRate,
            FinalAmount = transactionComp.FinalAmount,
            FinalAmountExchange = transactionComp.FinalAmountExchange,
            Income = transactionComp.Discount > 0
                ? (transactionComp.Amount - transactionComp.Discount).Income()
                : transactionComp.Amount.Income(),
            NetIncome = transactionComp.Discount > 0
                ? (transactionComp.Amount - transactionComp.Discount).NetIncome()
                : transactionComp.Amount.NetIncome(),
            NutritionistId = transactionComp.NutritionistId,
            OrderId = context.Message.OrderId,
            PackageId = transactionComp.PackageId,
            PackageType = transactionComp.PackageType,
            PaymentFor = PaymentType.WalletCharge.ToDescription(),
            PaymentTransactionId = transactionComp.PaymentTransactionId,
            SaleReferenceId = transactionComp.SaleReferenceId,
            TraceNo = transactionComp.TraceNo,
            TransactionType = TransactionType.OrderAccepted.ToDescription(),
            UserId = transactionComp.NutritionistId,
            UserType = UserType.Nutritionist.ToDescription(),
            WalletId = wallet.Id,
            ValueAdded = transactionComp.Discount > 0
                ? (transactionComp.Amount - transactionComp.Discount).ValueAdded()
                : transactionComp.Amount.ValueAdded(),
            Wage = transactionComp.Wage,
            UserName = context.Message.Username,
            WalletTransactionId = transactionComp.WalletTransactionId
        };
        var commandTr = new ChargeWalletTransactionCommand
        {
            WalletId = commandComp.WalletId,
            Amount = commandComp.DebtToTheNutritionist,
            Bank = commandComp.Bank,
            CurrencyId = commandComp.CurrencyId,
            CurrencyCode = commandComp.CurrencyCode,
            PaymentType = PaymentType.WalletCharge,
            SaleReferenceId = transactionComp.SaleReferenceId,
            UserId = commandComp.UserId,
            UserType = commandComp.UserType.ToEnum<UserType>(),
            UserName = context.Message.Username
        };
        var trId = await _mediator.Send(commandTr);
        commandComp.WalletTransactionId = trId;
        await _mediator.Send(commandComp);
        await _mediator.Send(new WalletChargeCommand
        {
            Amount = commandComp.DebtToTheNutritionist,
            CurrencyCode = commandComp.CurrencyCode,
            Id = commandComp.WalletId,
            UserId = transactionComp.NutritionistId,
            // PackageType = PackageType.NutritionistPack,
            Duration = transactionComp.DurationPackage,
            PaymentType = PaymentType.PackageNutritionist,
            UserName = context.Message.Username
        });
    }
}