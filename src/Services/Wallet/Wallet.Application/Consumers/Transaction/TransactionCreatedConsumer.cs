using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Payments.Transaction;
using EventBus.Messages.Events.Services.Wallet;
using MassTransit;
using Wallet.Application.TransactionCompanies.V1.Command.CreateTransactionComp;
using Wallet.Application.Transactions.V1.Commands.ChargeWalletTransaction;
using Wallet.Application.Transactions.V1.Commands.SubTrackWalletForPackage;
using Wallet.Application.Wallets.V1.Command.SubtractWallet;
using Wallet.Application.Wallets.V1.Command.WalletCharge;
using Wallet.Application.Wallets.V1.Query.GetWalletByUserId;

namespace Wallet.Application.Consumers.Transaction;

public class TransactionCreatedConsumer : IConsumer<WalletTransactionCreatedEvent>
{
    private readonly IMediator _mediator;
    private readonly IRequestClient<GetTransactionById> _clientTransaction;

    public TransactionCreatedConsumer(IMediator mediator, IRequestClient<GetTransactionById> clientTransaction)
    {
        _mediator = mediator;
        _clientTransaction = clientTransaction;
    }

    public async Task Consume(ConsumeContext<WalletTransactionCreatedEvent> context)
    {
        var transactionPay = await _clientTransaction.GetResponse<GetTransactionByIdResult>(new GetTransactionById
        {
            Id = context.Message.TransactionId,
        });
        var wallet = await _mediator.Send(new GetWalletByUserIdQuery
        {
            UserId = transactionPay.Message.UserId
        });

        switch (context.Message.PaymentType)
        {
            case PaymentType.WalletCharge:
                string walletChargeTrId = string.Empty;
                var walletChargeCommand = new ChargeWalletTransactionCommand();

                if (transactionPay.Message.FinalAmountExchange == null ||
                    transactionPay.Message.FinalAmountExchange == 0)
                {
                    //transaction wallet
                    walletChargeCommand = new ChargeWalletTransactionCommand()
                    {
                        Amount = transactionPay.Message.FinalAmount,
                        Bank = transactionPay.Message.Bank,
                        CurrencyId = transactionPay.Message.CurrencyId,
                        CurrencyCode = transactionPay.Message.CurrencyCode,
                        PaymentType = transactionPay.Message.PaymentFor.ToEnum<PaymentType>(),
                        SaleReferenceId = transactionPay.Message.SaleReferenceId,
                        UserId = transactionPay.Message.UserId,
                        UserType = transactionPay.Message.UserType.ToEnum<UserType>(),
                        WalletId = wallet.Id,
                        UserName = context.Message.Username
                    };
                }
                else
                {
                    //transaction wallet
                    walletChargeCommand = new ChargeWalletTransactionCommand()
                    {
                        Amount = (double)transactionPay.Message.FinalAmountExchange,
                        Bank = transactionPay.Message.Bank,
                        CurrencyId = transactionPay.Message.ExchangeCurrencyId,
                        CurrencyCode = transactionPay.Message.ExchangeCurrencyCode,
                        PaymentType = transactionPay.Message.PaymentFor.ToEnum<PaymentType>(),
                        SaleReferenceId = transactionPay.Message.SaleReferenceId,
                        UserId = transactionPay.Message.UserId,
                        UserType = transactionPay.Message.UserType.ToEnum<UserType>(),
                        WalletId = wallet.Id,
                        UserName = context.Message.Username
                    };
                }

                walletChargeTrId = await _mediator.Send(walletChargeCommand);
                await _mediator.Send(new TransactionCompanyCommand
                {
                    PaymentTransactionId = transactionPay.Message.Id,
                    TransactionType = TransactionType.WalletCharge.ToDescription(),
                    Amount = transactionPay.Message.Amount,
                    Bank = transactionPay.Message.Bank,
                    BankOrderId = transactionPay.Message.BankOrderId,
                    CountryId = transactionPay.Message.CountryId,
                    CurrencyId = transactionPay.Message.CurrencyId,
                    CurrencyCode = transactionPay.Message.CurrencyCode,
                    DebtToTheNutritionist = 0,
                    Discount = transactionPay.Message.Discount,
                    DiscountCode = transactionPay.Message.DiscountCode,
                    DiscountType = transactionPay.Message.DiscountType,
                    ExchangeCurrencyId = transactionPay.Message.ExchangeCurrencyId,
                    ExchangeCurrencyCode = transactionPay.Message.ExchangeCurrencyCode,
                    ExchangeRate = transactionPay.Message.ExchangeRate,
                    FinalAmount = transactionPay.Message.FinalAmount,
                    FinalAmountExchange = transactionPay.Message.FinalAmountExchange,
                    Income = 0,
                    NetIncome = 0,
                    NutritionistId = string.Empty,
                    OrderId = string.Empty,
                    PackageId = string.Empty,
                    PaymentFor = transactionPay.Message.PaymentFor,
                    SaleReferenceId = transactionPay.Message.SaleReferenceId,
                    TraceNo = transactionPay.Message.TraceNo,
                    UserId = transactionPay.Message.UserId,
                    UserType = transactionPay.Message.UserType,
                    ValueAdded = 0,
                    Wage = transactionPay.Message.Wage,
                    WalletTransactionId = walletChargeTrId,
                    WalletId = wallet.Id,
                    PackageType = transactionPay.Message.PackageType,
                    UserName = context.Message.Username
                });

                await _mediator.Send(new WalletChargeCommand
                {
                    CurrencyCode = walletChargeCommand.CurrencyCode,
                    Amount = walletChargeCommand.Amount,
                    Id = wallet.Id,
                    // PackageType = PackageType.None,
                    PaymentType = transactionPay.Message.PaymentFor.ToEnum<PaymentType>(),
                    UserId = transactionPay.Message.UserId,
                    Duration = transactionPay.Message.DurationPackage,
                    UserName = context.Message.Username
                });
                break;
            case PaymentType.PackageNutritionist:
                string walletChargePackageNutritionistTrId = string.Empty;
                var commandChargePackageNutritionist = new ChargeWalletTransactionCommand();

                if (transactionPay.Message.FinalAmountExchange == null ||
                    transactionPay.Message.FinalAmountExchange == 0)
                {
                    //transaction wallet
                    commandChargePackageNutritionist = new ChargeWalletTransactionCommand()
                    {
                        Amount = transactionPay.Message.FinalAmount,
                        Bank = transactionPay.Message.Bank,
                        CurrencyId = transactionPay.Message.CurrencyId,
                        CurrencyCode = transactionPay.Message.CurrencyCode,
                        PaymentType = transactionPay.Message.PaymentFor.ToEnum<PaymentType>(),
                        SaleReferenceId = transactionPay.Message.SaleReferenceId,
                        UserId = transactionPay.Message.UserId,
                        UserType = transactionPay.Message.UserType.ToEnum<UserType>(),
                        WalletId = wallet.Id,
                        UserName = context.Message.Username
                    };
                }
                else
                {
                    //transaction wallet
                    commandChargePackageNutritionist = new ChargeWalletTransactionCommand()
                    {
                        Amount = (double)transactionPay.Message.FinalAmountExchange,
                        Bank = transactionPay.Message.Bank,
                        CurrencyId = transactionPay.Message.ExchangeCurrencyId,
                        CurrencyCode = transactionPay.Message.ExchangeCurrencyCode,
                        PaymentType = transactionPay.Message.PaymentFor.ToEnum<PaymentType>(),
                        SaleReferenceId = transactionPay.Message.SaleReferenceId,
                        UserId = transactionPay.Message.UserId,
                        UserType = transactionPay.Message.UserType.ToEnum<UserType>(),
                        WalletId = wallet.Id,
                        UserName = context.Message.Username
                    };
                }

                walletChargePackageNutritionistTrId = await _mediator.Send(commandChargePackageNutritionist);
                await _mediator.Send(new TransactionCompanyCommand
                {
                    PaymentTransactionId = transactionPay.Message.Id,
                    TransactionType = TransactionType.WalletCharge.ToDescription(),
                    Amount = transactionPay.Message.Amount,
                    Bank = transactionPay.Message.Bank,
                    BankOrderId = transactionPay.Message.BankOrderId,
                    CountryId = transactionPay.Message.CountryId,
                    CurrencyId = transactionPay.Message.CurrencyId,
                    CurrencyCode = transactionPay.Message.CurrencyCode,
                    DebtToTheNutritionist = 0,
                    Discount = transactionPay.Message.Discount,
                    DiscountCode = transactionPay.Message.DiscountCode,
                    DiscountType = transactionPay.Message.DiscountType,
                    ExchangeCurrencyId = transactionPay.Message.ExchangeCurrencyId,
                    ExchangeCurrencyCode = transactionPay.Message.ExchangeCurrencyCode,
                    ExchangeRate = transactionPay.Message.ExchangeRate,
                    FinalAmount = transactionPay.Message.FinalAmount,
                    FinalAmountExchange = transactionPay.Message.FinalAmountExchange,
                    Income = 0,
                    NetIncome = 0,
                    NutritionistId = string.Empty,
                    OrderId = string.Empty,
                    PackageId = string.Empty,
                    PaymentFor = transactionPay.Message.PaymentFor,
                    SaleReferenceId = transactionPay.Message.SaleReferenceId,
                    TraceNo = transactionPay.Message.TraceNo,
                    UserId = transactionPay.Message.UserId,
                    UserType = transactionPay.Message.UserType,
                    ValueAdded = 0,
                    Wage = transactionPay.Message.Wage,
                    WalletTransactionId = walletChargePackageNutritionistTrId,
                    PackageType = transactionPay.Message.PackageType,
                    WalletId = wallet.Id,
                    UserName = context.Message.Username
                });
                //charge wallet
                await _mediator.Send(new WalletChargeCommand
                {
                    CurrencyCode = commandChargePackageNutritionist.CurrencyCode,
                    Amount = commandChargePackageNutritionist.Amount,
                    Id = wallet.Id,
                    PackageType = transactionPay.Message.PackageType.ToEnum<PackageType>(),
                    PaymentType = transactionPay.Message.PaymentFor.ToEnum<PaymentType>(),
                    UserId = transactionPay.Message.UserId,
                    Duration = transactionPay.Message.DurationPackage,
                    UserName = context.Message.Username
                });
                //subTrack wallet
                var subTrackPackageNutritionistCommand = new SubTrackWalletForPackageCommand
                {
                    UserId = transactionPay.Message.UserId,
                    Amount = commandChargePackageNutritionist.Amount,
                    PackageId = context.Message.PackageId,
                    PaymentType = PaymentType.PackageNutritionist,
                    UserType = transactionPay.Message.UserType.ToEnum<UserType>(),
                    UserName = context.Message.Username
                };
                var subTrackPackageNutritionistTrId = await _mediator.Send(subTrackPackageNutritionistCommand);
                var subTrackPackageNutritionistTrCompId = await _mediator.Send(new TransactionCompanyCommand
                {
                    PaymentTransactionId = transactionPay.Message.Id,
                    TransactionType = TransactionType.WalletSubTrack.ToDescription(),
                    Amount = transactionPay.Message.Amount,
                    Bank = transactionPay.Message.Bank,
                    BankOrderId = transactionPay.Message.BankOrderId,
                    CountryId = transactionPay.Message.CountryId,
                    CurrencyId = transactionPay.Message.CurrencyId,
                    CurrencyCode = transactionPay.Message.CurrencyCode,
                    DebtToTheNutritionist = 0,
                    Discount = transactionPay.Message.Discount,
                    DiscountCode = transactionPay.Message.DiscountCode,
                    DiscountType = transactionPay.Message.DiscountType,
                    ExchangeCurrencyId = transactionPay.Message.ExchangeCurrencyId,
                    ExchangeCurrencyCode = transactionPay.Message.ExchangeCurrencyCode,
                    ExchangeRate = transactionPay.Message.ExchangeRate,
                    FinalAmount = transactionPay.Message.FinalAmount,
                    FinalAmountExchange = transactionPay.Message.FinalAmountExchange,
                    Income = 0,
                    NetIncome = 0,
                    NutritionistId = transactionPay.Message.NutritionistId,
                    OrderId = string.Empty,
                    PackageId = transactionPay.Message.PackageId,
                    PaymentFor = PaymentType.PackageNutritionist.ToDescription(),
                    SaleReferenceId = transactionPay.Message.SaleReferenceId,
                    TraceNo = transactionPay.Message.TraceNo,
                    UserId = transactionPay.Message.UserId,
                    UserType = transactionPay.Message.UserType,
                    ValueAdded = 0,
                    Wage = transactionPay.Message.Wage,
                    WalletTransactionId = subTrackPackageNutritionistTrId,
                    PackageType = transactionPay.Message.PackageType,
                    WalletId = wallet.Id,
                    UserName = context.Message.Username
                });
                await _mediator.Send(new SubtractWalletCommand
                {
                    CurrencyCode = commandChargePackageNutritionist.CurrencyCode,
                    Id = wallet.Id,
                    PaymentTransactionId = transactionPay.Message.Id,
                    TransactionCompId = subTrackPackageNutritionistTrCompId,
                    SubtractAmount = subTrackPackageNutritionistCommand.Amount,
                    UserName = context.Message.Username
                });
                break;
            case PaymentType.PackageO2:
                string walletChargePackageO2TrId = string.Empty;
                var commandwalletChargePackageO2 = new ChargeWalletTransactionCommand();

                if (transactionPay.Message.FinalAmountExchange == null ||
                    transactionPay.Message.FinalAmountExchange == 0)
                {
                    //transaction wallet
                    commandwalletChargePackageO2 = new ChargeWalletTransactionCommand()
                    {
                        Amount = transactionPay.Message.FinalAmount,
                        Bank = transactionPay.Message.Bank,
                        CurrencyId = transactionPay.Message.CurrencyId,
                        CurrencyCode = transactionPay.Message.CurrencyCode,
                        PaymentType = transactionPay.Message.PaymentFor.ToEnum<PaymentType>(),
                        SaleReferenceId = transactionPay.Message.SaleReferenceId,
                        UserId = transactionPay.Message.UserId,
                        UserType = transactionPay.Message.UserType.ToEnum<UserType>(),
                        WalletId = wallet.Id,
                        UserName = context.Message.Username
                    };
                }
                else
                {
                    //transaction wallet
                    commandwalletChargePackageO2 = new ChargeWalletTransactionCommand()
                    {
                        Amount = (double)transactionPay.Message.FinalAmountExchange,
                        Bank = transactionPay.Message.Bank,
                        CurrencyId = transactionPay.Message.ExchangeCurrencyId,
                        CurrencyCode = transactionPay.Message.ExchangeCurrencyCode,
                        PaymentType = transactionPay.Message.PaymentFor.ToEnum<PaymentType>(),
                        SaleReferenceId = transactionPay.Message.SaleReferenceId,
                        UserId = transactionPay.Message.UserId,
                        UserType = transactionPay.Message.UserType.ToEnum<UserType>(),
                        WalletId = wallet.Id,
                        UserName = context.Message.Username
                    };
                }

                walletChargePackageO2TrId = await _mediator.Send(commandwalletChargePackageO2);
                var chargePackageO2TrCompId = await _mediator.Send(new TransactionCompanyCommand
                {
                    PaymentTransactionId = transactionPay.Message.Id,
                    TransactionType = TransactionType.WalletCharge.ToDescription(),
                    Amount = transactionPay.Message.Amount,
                    Bank = transactionPay.Message.Bank,
                    BankOrderId = transactionPay.Message.BankOrderId,
                    CountryId = transactionPay.Message.CountryId,
                    CurrencyId = transactionPay.Message.CurrencyId,
                    CurrencyCode = transactionPay.Message.CurrencyCode,
                    DebtToTheNutritionist = 0,
                    Discount = transactionPay.Message.Discount,
                    DiscountCode = transactionPay.Message.DiscountCode,
                    DiscountType = transactionPay.Message.DiscountType,
                    ExchangeCurrencyId = transactionPay.Message.ExchangeCurrencyId,
                    ExchangeCurrencyCode = transactionPay.Message.ExchangeCurrencyCode,
                    ExchangeRate = transactionPay.Message.ExchangeRate,
                    FinalAmount = transactionPay.Message.FinalAmount,
                    FinalAmountExchange = transactionPay.Message.FinalAmountExchange,
                    Income = 0,
                    NetIncome = 0,
                    NutritionistId = string.Empty,
                    OrderId = string.Empty,
                    PackageId = string.Empty,
                    PaymentFor = transactionPay.Message.PaymentFor,
                    SaleReferenceId = transactionPay.Message.SaleReferenceId,
                    TraceNo = transactionPay.Message.TraceNo,
                    UserId = transactionPay.Message.UserId,
                    UserType = transactionPay.Message.UserType,
                    ValueAdded = 0,
                    Wage = transactionPay.Message.Wage,
                    WalletTransactionId = walletChargePackageO2TrId,
                    PackageType = transactionPay.Message.PackageType,
                    WalletId = wallet.Id,
                    UserName = context.Message.Username
                });
                //charge wallet
                await _mediator.Send(new WalletChargeCommand
                {
                    CurrencyCode = commandwalletChargePackageO2.CurrencyCode,
                    Amount = commandwalletChargePackageO2.Amount,
                    Id = wallet.Id,
                    PackageType = transactionPay.Message.PackageType.ToEnum<PackageType>(),
                    PaymentType = transactionPay.Message.PaymentFor.ToEnum<PaymentType>(),
                    UserId = transactionPay.Message.UserId,
                    Duration = transactionPay.Message.DurationPackage,
                    UserName = context.Message.Username
                });
                //subTrack wallet
                var subTrackCommand = new SubTrackWalletForPackageCommand
                {
                    UserId = transactionPay.Message.UserId,
                    Amount = commandwalletChargePackageO2.Amount,
                    PackageId = context.Message.PackageId,
                    PaymentType = PaymentType.PackageO2,
                    UserType = transactionPay.Message.UserType.ToEnum<UserType>(),
                    UserName = context.Message.Username
                };
                var trWalletSub = await _mediator.Send(subTrackCommand);
                var trSubComp = await _mediator.Send(new TransactionCompanyCommand
                {
                    PaymentTransactionId = transactionPay.Message.Id,
                    TransactionType = TransactionType.WalletSubTrack.ToDescription(),
                    Amount = transactionPay.Message.Amount,
                    Bank = transactionPay.Message.Bank,
                    BankOrderId = transactionPay.Message.BankOrderId,
                    CountryId = transactionPay.Message.CountryId,
                    CurrencyId = transactionPay.Message.CurrencyId,
                    CurrencyCode = transactionPay.Message.CurrencyCode,
                    DebtToTheNutritionist = 0,
                    Discount = transactionPay.Message.Discount,
                    DiscountCode = transactionPay.Message.DiscountCode,
                    DiscountType = transactionPay.Message.DiscountType,
                    ExchangeCurrencyId = transactionPay.Message.ExchangeCurrencyId,
                    ExchangeCurrencyCode = transactionPay.Message.ExchangeCurrencyCode,
                    ExchangeRate = transactionPay.Message.ExchangeRate,
                    FinalAmount = transactionPay.Message.FinalAmount,
                    FinalAmountExchange = transactionPay.Message.FinalAmountExchange,
                    Income = 0,
                    NetIncome = 0,
                    NutritionistId = string.Empty,
                    OrderId = string.Empty,
                    PackageId = transactionPay.Message.PackageId,
                    PaymentFor = PaymentType.PackageO2.ToDescription(),
                    SaleReferenceId = transactionPay.Message.SaleReferenceId,
                    TraceNo = transactionPay.Message.TraceNo,
                    UserId = transactionPay.Message.UserId,
                    UserType = transactionPay.Message.UserType,
                    ValueAdded = 0,
                    Wage = transactionPay.Message.Wage,
                    WalletTransactionId = trWalletSub,
                    PackageType = transactionPay.Message.PackageType,
                    WalletId = wallet.Id,
                    UserName = context.Message.Username
                });
                await _mediator.Send(new SubtractWalletCommand
                {
                    CurrencyCode = commandwalletChargePackageO2.CurrencyCode,
                    Id = wallet.Id,
                    PaymentTransactionId = transactionPay.Message.Id,
                    SubtractAmount = subTrackCommand.Amount,
                    TransactionCompId = trSubComp,
                    UserName = context.Message.Username
                });
                break;
        }
    }
}