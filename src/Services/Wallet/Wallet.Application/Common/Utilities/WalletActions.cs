using MongoDB.Bson;
using System.Threading;
using EventBus.Messages.Events.Services.Order;
using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Application.Common.Mapping;
using Wallet.Application.Dtos;
using Wallet.Application.Wallets.V1.Command.SubtractWallet;
using Wallet.Application.Wallets.V1.Command.WalletCharge;
using Wallet.Application.Wallets.V1.Query.GetWalletById;
using Wallet.Application.Wallets.V1.Query.GetWalletByUserId;
using Wallet.Domain.Aggregates.TransactionCompanyAggregate;

namespace Wallet.Application.Common.Utilities
{
    public class WalletActions
    {
        private readonly IMediator _mediator;

        public WalletActions(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        //public async Task<WalletDto> GetWalletById(string id)
        //{
        //    var wallet = await _Uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
        //         .GetByIdAsync(id);
        //    if (wallet == null) throw new NotFoundException("wallet not found");
        //    return wallet.ToDto<WalletDto>();
        //}
        //public async Task<WalletDto> GetWalletByUserId(string id)
        //{
        //    var filter = Builders<Domain.Aggregates.WalletAggregate.Wallet>.Filter.Eq(x => x.UserId, ObjectId.Parse(id));
        //    var wallet = await _Uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
        //        .GetSingleDocumentByFilterAsync(filter);
        //    if (wallet == null) throw new NotFoundException("wallet not found");
        //    return wallet.ToDto<WalletDto>();
        //}
        //public async Task<string> InsertWalletTransaction(CreateTransactionCommand request)
        //{
        //    var wallet = await GetWalletByUserId(request.UserId);
        //    if (wallet == null) throw new NotFoundException("wallet not found");
        //    if (wallet.Amount < request.Amount) throw new AppException("Your wallet balance is not enough");

        //    var transaction = new Domain.Aggregates.TransactionAggregate.Transaction
        //    {
        //        Amount = request.Amount,
        //        Bank = "Wallet",
        //        CurrencyName = request.CurrencyName,
        //        CurrencyId = ObjectId.Parse(request.CurrencyId),
        //        DateTime = DateTime.Now,
        //        PackageId = ObjectId.Parse(request.PackageId),
        //        UserType = request.UserType.ToDescription(),
        //        SaleReferenceId = string.Empty,
        //        TransactionType = request.PaymentType.ToDescription(),
        //        UserId = ObjectId.Parse(request.UserId),
        //        WalletId = ObjectId.Parse(wallet.Id),
        //    };
        //    await _Uow.GenericRepository<Domain.Aggregates.TransactionAggregate.Transaction>().InsertOneAsync(transaction, null);

        //    return transaction.Id;
        //}

        //public async Task<string> InsertCompanyTransaction(TransactionCompanyCommand request)
        //{
        //    var transaction = new TransactionCompany
        //    {
        //        Amount = request.Amount,
        //        Bank = request.Bank,
        //        CurrencyId = new ObjectId(request.CurrencyId),
        //        CurrencyName = request.CurrencyName,
        //        DateTime = DateTime.Now,
        //        DebtToTheNutritionist = request.DebtToTheNutritionist,
        //        Discount = request.Discount,
        //        DiscountCode = request.DiscountCode,
        //        FinalAmount = request.FinalAmount,
        //        Income = request.Income,
        //        NetIncome = request.NetIncome,
        //        PackageId = new ObjectId(request.PackageId),
        //        PaymentFor = request.PaymentFor,
        //        WalletTransactionId = new ObjectId(request.TransactionId),
        //        UserId = new ObjectId(request.UserId),
        //        UserType = request.UserType,
        //        ValueAdded = request.ValueAdded,
        //        Wage = request.Wage,
        //        WalletId = new ObjectId(request.WalletId),
        //        SaleReferenceId = request.SaleReferenceId,
        //        TraceNo = request.TraceNo,
        //    };
        //    await _Uow.GenericRepository<TransactionCompany>()
        //        .InsertOneAsync(transaction, null);

        //    return transaction.Id;
        //}

        //public async Task WalletCharge(double amountCharge, string id, string currencyName)
        //{
        //    var wallet = await _Uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
        //        .GetByIdAsync(id);
        //    if (wallet == null) throw new NotFoundException("wallet not found");
        //    if (!wallet.CurrencyName.Equals(currencyName)) throw new AppException("Currency not Valid");

        //    wallet.Amount += amountCharge;

        //    await _Uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
        //        .UpdateOneAsync(x => x.Id == id, wallet,
        //            new Expression<Func<Domain.Aggregates.WalletAggregate.Wallet, object>>[]
        //            {
        //                x => x.Amount,
        //            }, null);
        //}

        //public async Task WalletSubTrack(double subtractAmount, string id, string currencyName,
        //    string packageId, string walletTransactionId, string compTransactionId)
        //{
        //    var wallet = await _Uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
        //        .GetByIdAsync(id);

        //    if (wallet == null) throw new NotFoundException("wallet not found");
        //    if (!wallet.CurrencyName.Equals(currencyName)) throw new AppException("Currency not Valid");
        //    if (wallet.Amount < subtractAmount) throw new AppException("Your wallet balance is not enough");

        //    wallet.Amount -= subtractAmount;

        //    wallet.AddDomainEvent(new OrderCreatedEvent
        //    {
        //        PackageId = packageId,
        //        WalletTransactionId = walletTransactionId,
        //        PaymentTransactionId = compTransactionId
        //    });

        //    await _Uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
        //        .UpdateOneAsync(x => x.Id == id, wallet,
        //            new Expression<Func<Domain.Aggregates.WalletAggregate.Wallet, object>>[]
        //            {
        //                x => x.Amount,
        //            }, null);
        //}

    }

}
