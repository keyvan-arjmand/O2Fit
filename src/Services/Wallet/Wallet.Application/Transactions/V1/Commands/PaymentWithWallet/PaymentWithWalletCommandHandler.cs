using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Payments.Package;
using EventBus.Messages.Events.Services.Order;
using MassTransit;
using MongoDB.Bson;
using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Application.Transactions.V1.Commands.ChargeWalletTransaction;
using Wallet.Domain.Aggregates.TransactionAggregate;
using Wallet.Domain.Aggregates.TransactionCompanyAggregate;
using Wallet.Domain.Aggregates.WalletAggregate;

namespace Wallet.Application.Transactions.V1.Commands.PaymentWithWallet;

public class PaymentWithWalletCommandHandler : IRequestHandler<PaymentWithWalletCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetByIdPackage> _clientPackage;
    private readonly IRequestClient<ExchangerCurrency> _clientCurrency;

    public PaymentWithWalletCommandHandler(IUnitOfWork uow, IRequestClient<GetByIdPackage> clientPackage,
        IRequestClient<ExchangerCurrency> clientCurrency)
    {
        _uow = uow;
        _clientPackage = clientPackage;
        _clientCurrency = clientCurrency;
    }

    public async Task<string> Handle(PaymentWithWalletCommand request, CancellationToken cancellationToken)
    {
        var package = await _clientPackage.GetResponse<GetByIdPackageResult>(new GetByIdPackage
        {
            Id = request.PackageId
        }, cancellationToken);
        if (string.IsNullOrEmpty(package.Message.Id)) throw new NotFoundException("package Not Found");

        var filter =
            Builders<Domain.Aggregates.WalletAggregate.Wallet>.Filter.Eq(x => x.UserId, ObjectId.Parse(request.UserId));
        var wallet = await _uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        
        Transaction tr;
        TransactionCompany trComp;
        if (!package.Message.CurrencyId.Equals(wallet.CurrencyId.ToString()))
        {
            var exChange = await _clientCurrency.GetResponse<ExchangerCurrencyResult>(new ExchangerCurrency
            {
                SourceCurrencyCode = wallet.CurrencyCode,
                SourceCurrencyAmount = package.Message.DiscountPrice,
                DestinationCurrencyCode = package.Message.CurrencyCode
            }, cancellationToken);
            if (exChange.Message.ExRate < 0) throw new NotFoundException("package Not Found");
            if (wallet.Amount < exChange.Message.DestinationCurrencyAmount)
                throw new AppException("The balance of the wallet is not enough");

            tr = new Transaction
            {
                Amount = exChange.Message.DestinationCurrencyAmount,
                CurrencyId = wallet.CurrencyId,
                CurrencyCode = wallet.CurrencyCode,
                DateTime = DateTime.Now,
                PackageId = new ObjectId(package.Message.Id),
                // PaymentFor = package.Message.PackageType.ToEnum<PackageType>().ValidO2Package()
                //     ? PaymentType.PackageO2.ToDescription()
                //     : PaymentType.PackageNutritionist.ToDescription(),
                TransactionType = TransactionType.WalletSubTrack.ToDescription(),
                UserType = UserType.User.ToDescription(),
                WalletId = new ObjectId(wallet.Id),
                UserId = new ObjectId(request.UserId),
                CreatedBy = request.UserName,
                CreatedById = ObjectId.Parse(request.UserId),
                Created = DateTime.UtcNow
            };
            trComp = new TransactionCompany
            {
                CountryId = request.CountryId,
                Bank = "Wallet",
                BankOrderId = 0,
                CurrencyId = new ObjectId(package.Message.CurrencyId),
                Amount = package.Message.Price,
                FinalAmount = package.Message.DiscountPrice,
                CurrencyCode = package.Message.CurrencyCode,
                DateTime = DateTime.Now,
                DebtToTheNutritionist = 0,
                Discount = 0,
                DiscountCode = string.Empty,
                DiscountType = string.Empty,
                ExchangeCurrencyId = tr.CurrencyId,
                ExchangeCurrencyCode = tr.CurrencyCode,
                ExchangeRate = exChange.Message.ExRate,
                FinalAmountExchange = tr.Amount,
                WalletId = new ObjectId(wallet.Id),
                Income = 0,
                NetIncome = 0,
                Wage = package.Message.Wage,
                NutritionistId = new ObjectId(package.Message.UserId),
                UserType = UserType.User.ToDescription(),
                PackageId = new ObjectId(package.Message.Id),
                ValueAdded = 0,
                TransactionType = tr.TransactionType,
                UserId = new ObjectId(request.UserId),
                PaymentFor = tr.PaymentFor,
                // PackageType = package.Message.PackageType,
                CreatedBy = request.UserName,
                CreatedById = ObjectId.Parse(request.UserId),
                Created = DateTime.UtcNow
            };
        }
        else
        {
            if (wallet.Amount < package.Message.DiscountPrice)
                throw new AppException("The balance of the wallet is not enough");
            tr = new Transaction
            {
                Amount = package.Message.DiscountPrice,
                CurrencyId = wallet.CurrencyId,
                CurrencyCode = wallet.CurrencyCode,
                DateTime = DateTime.Now,
                PackageId = new ObjectId(package.Message.Id),
                // PaymentFor = package.Message.PackageType.ToEnum<PackageType>().ValidO2Package()
                //     ? PaymentType.PackageO2.ToDescription()
                //     : PaymentType.PackageNutritionist.ToDescription(),
                TransactionType = TransactionType.WalletSubTrack.ToDescription(),
                UserType = UserType.User.ToDescription(),
                WalletId = new ObjectId(wallet.Id),
                UserId = new ObjectId(request.UserId),
                CreatedBy = request.UserName,
                CreatedById = ObjectId.Parse(request.UserId),
                Created = DateTime.UtcNow
            };
            trComp = new TransactionCompany
            {
                CountryId = request.CountryId,
                Bank = "Wallet",
                BankOrderId = 0,
                CurrencyId = new ObjectId(package.Message.CurrencyId),
                Amount = package.Message.Price,
                FinalAmount = package.Message.DiscountPrice,
                CurrencyCode = package.Message.CurrencyCode,
                DateTime = DateTime.Now,
                DebtToTheNutritionist = 0,
                Discount = 0,
                DiscountCode = string.Empty,
                DiscountType = string.Empty,
                WalletId = new ObjectId(wallet.Id),
                Income = 0,
                NetIncome = 0,
                Wage = package.Message.Wage,
                NutritionistId = new ObjectId(package.Message.UserId),
                UserType = UserType.User.ToDescription(),
                PackageId = new ObjectId(package.Message.Id),
                ValueAdded = 0,
                TransactionType = tr.TransactionType,
                UserId = new ObjectId(request.UserId),
                PaymentFor = tr.PaymentFor,
                // PackageType = package.Message.PackageType,
                CreatedBy = request.UserName,
                CreatedById = ObjectId.Parse(request.UserId),
                Created = DateTime.UtcNow
            };
        }

        await _uow.GenericRepository<Transaction>()
            .InsertOneAsync(tr, null, cancellationToken);
        trComp.WalletTransactionId = new ObjectId(tr.Id);
        await _uow.GenericRepository<TransactionCompany>()
            .InsertOneAsync(trComp, null, cancellationToken);

        wallet.Amount -= tr.Amount;
        wallet.AddDomainEvent(new OrderCreatedEvent
        {
            WalletTransactionId = trComp.Id,
            PaymentTransactionId = string.Empty,
            Username = request.UserName,
            UserId = wallet.UserId.ToString()
        });

        await _uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
            .UpdateOneAsync(x => x.Id == wallet.Id, wallet,
                new Expression<Func<Domain.Aggregates.WalletAggregate.Wallet, object>>[]
                {
                    x => x.Amount,
                }, null, cancellationToken);
        return trComp.Id;
    }
}