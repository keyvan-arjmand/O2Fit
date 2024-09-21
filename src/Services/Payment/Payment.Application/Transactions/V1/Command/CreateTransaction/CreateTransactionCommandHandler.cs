using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage;
using EventBus.Messages.Contracts.Services.Payments.Package;
using EventBus.Messages.Contracts.Services.Wallet;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Dtos.Mellat;
using Payment.Domain.Aggregates.SequenceAggregate;
using Payment.Domain.Enums;

namespace Payment.Application.Transactions.V1.Command.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, long>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetByIdPackage> _clientPackage;
    // private readonly IRequestClient<CalculateDiscountCode> _clientDiscountPackageCode;
    // private readonly IRequestClient<CalculateAmountPackageById> _clientDiscountPackage;
    private readonly IRequestClient<ExchangerCurrency> _clientCurrency;
    private readonly IRequestClient<GetWalletByUserId> _clientWallet;

    public CreateTransactionCommandHandler(IUnitOfWork uow, IRequestClient<GetByIdPackage> client,
     
        IRequestClient<ExchangerCurrency> clientCurrency, IRequestClient<GetWalletByUserId> clientWallet)
    {
        _uow = uow;
        _clientPackage = client;
        _clientCurrency = clientCurrency;
        _clientWallet = clientWallet;
    }

    public async Task<long> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //package
            // var package = await _uow.GenericRepository<Package>()
            //     .GetByIdAsync(request.PackageId, cancellationToken);
            // if (package == null) throw new NotFoundException("package Not Found");
            //
            // var wallet = await _clientWallet.GetResponse<GetWalletByUserIdResult>(new GetWalletByUserId
            // {
            //     UserId = request.UserId
            // }, cancellationToken);
            // if (wallet == null) throw new NotFoundException("wallet Not Found");
            // TransactionDietPackage transaction;
            // //country id package != countryId user =====> Exchange rate req
            // if (string.IsNullOrWhiteSpace(request.DiscountCode))
            // {
            //     var discountPackage = await _clientDiscountPackage.GetResponse<CalculateAmountPackageByIdResult>(
            //         new CalculateAmountPackageById
            //         {
            //             Id = package.Id,
            //             Price = package.Price,
            //         }, cancellationToken);
            //     transaction = new TransactionDietPackage
            //     {
            //         Amount = package.Price,
            //         PackageId = package.Id.StringToObjectId(),
            //         Wage = discountPackage.Message.Wage, //
            //         Bank = request.Bank.ToDescription(),
            //         CurrencyCode = package.CurrencyCode,
            //         Discount = 0, //code
            //         FinalAmount = discountPackage.Message.DiscountPrice == 0
            //             ? package.Price
            //             : discountPackage.Message.DiscountPrice,
            //         FinalState = PaymentResult.Pending.ToDescription(),
            //         DiscountCode = string.Empty,
            //         BankOrderId = await _uow.SequenceRepository().BankOrderGeneration(cancellationToken),
            //         CurrencyId = package.CurrencyId,
            //         PaymentFor = package.PackageType.ToEnum<PackageType>().GetPaymentType().ToString(),
            //         UserType = request.UserType.ToDescription(),
            //         DiscountType = string.Empty,
            //     };
            //     //exchange amount
            //     if (!wallet.Message.CurrencyCode.Equals(package.CurrencyCode))
            //     {
            //         var exChange = await _clientCurrency.GetResponse<ExchangerCurrencyResult>(new ExchangerCurrency
            //         {
            //             SourceCurrencyAmount = transaction.FinalAmount,
            //             SourceCurrencyCode = transaction.CurrencyCode,
            //             DestinationCurrencyCode = wallet.Message.CurrencyCode,
            //         }, cancellationToken);
            //
            //         transaction.ExchangeCurrencyId = wallet.Message.CurrencyId.StringToObjectId();
            //         transaction.ExchangeCurrencyCode = wallet.Message.CurrencyCode;
            //         transaction.FinalAmountExchange = exChange.Message.DestinationCurrencyAmount;
            //         transaction.ExchangeRate = exChange.Message.ExRate;
            //     }
            // }
            // else
            // {
            //     //discount Code
            //     var discountCode = await _clientDiscountPackageCode.GetResponse<CalculateDiscountCodeResult>(
            //         new CalculateDiscountCode()
            //         {
            //             PackageId = package.Id,
            //             DiscountCode = request.DiscountCode,
            //             CountryId = request.CountryId,
            //             UserId = request.UserId
            //         }, cancellationToken);
            //     if (discountCode.Message.Price == 0) throw new AppException("Discount Code Not Valid");
            //
            //     if (!wallet.Message.CurrencyCode.Equals(package.CurrencyCode))
            //     {
            //         var exChange = await _clientCurrency.GetResponse<ExchangerCurrencyResult>(new ExchangerCurrency
            //         {
            //             SourceCurrencyAmount = discountCode.Message.DiscountByCode,
            //             SourceCurrencyCode = package.CurrencyCode,
            //             DestinationCurrencyCode = wallet.Message.CurrencyCode,
            //         }, cancellationToken);
            //         transaction = new TransactionDietPackage
            //         {
            //             Amount = package.Price,
            //             PackageId = package.Id.StringToObjectId(),
            //             Wage = discountCode.Message.AmountDiscountPackage,
            //             Bank = request.Bank.ToDescription(),
            //             CurrencyCode = package.CurrencyCode,
            //             Discount = discountCode.Message.AmountDiscountCode,
            //             FinalAmount = discountCode.Message.DiscountByCode,
            //             FinalState = PaymentResult.Pending.ToDescription(),
            //             DiscountCode = request.DiscountCode,
            //             BankOrderId = await _uow.SequenceRepository().BankOrderGeneration(cancellationToken),
            //             CurrencyId = package.CurrencyId,
            //             PaymentFor = package.PackageType.ToEnum<PackageType>().GetPaymentType().ToString(),
            //             UserType = request.UserType.ToDescription(),
            //             DiscountType = discountCode.Message.DiscountType,
            //             ExchangeCurrencyId = wallet.Message.CurrencyId.StringToObjectId(),
            //             ExchangeCurrencyCode = wallet.Message.CurrencyCode,
            //             FinalAmountExchange = exChange.Message.DestinationCurrencyAmount,
            //             ExchangeRate = exChange.Message.ExRate,
            //         };
            //     }
            //     else
            //     {
            //         transaction = new TransactionDietPackage
            //         {
            //             Amount = package.Price,
            //             PackageId = package.Id.StringToObjectId(),
            //             Wage = discountCode.Message.AmountDiscountPackage,
            //             Bank = request.Bank.ToDescription(),
            //             CurrencyCode = package.CurrencyCode,
            //             Discount = discountCode.Message.AmountDiscountCode,
            //             FinalAmount = discountCode.Message.DiscountByCode,
            //             FinalState = PaymentResult.Pending.ToDescription(),
            //             DiscountCode = request.DiscountCode,
            //             BankOrderId = await _uow.SequenceRepository().BankOrderGeneration(cancellationToken),
            //             CurrencyId = package.CurrencyId,
            //             PaymentFor = package.PackageType.ToEnum<PackageType>().GetPaymentType().ToString(),
            //             UserType = request.UserType.ToDescription(),
            //             DiscountType = discountCode.Message.DiscountType,
            //         };
            //     }
            // }
            //
            // await _uow.GenericRepository<TransactionDietPackage>()
            //     .InsertOneAsync(transaction, null, cancellationToken);
            // return transaction.BankOrderId;
            return 1;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}