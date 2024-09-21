using Common.Enums.TypeEnums;
using MongoDB.Bson;
using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Domain.Aggregates.TransactionAggregate;
using Wallet.Domain.Aggregates.TransactionCompanyAggregate;

namespace Wallet.Application.Transactions.V1.Commands.ChargeWalletTransaction;

public class ChargeWalletTransactionCommandHandler : IRequestHandler<ChargeWalletTransactionCommand, string>
{
    private readonly IUnitOfWork _uow;
    public ChargeWalletTransactionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<string> Handle(ChargeWalletTransactionCommand request, CancellationToken cancellationToken)
    {
      
        var transactionWallet = new Transaction
        {
            Amount = request.Amount,
            CurrencyId = new ObjectId(request.CurrencyId),
            CurrencyCode = request.CurrencyCode,
            DateTime = DateTime.Now,
            WalletId = new ObjectId(request.WalletId),
            UserType = request.UserType.ToDescription(),
            PaymentFor = request.PaymentType.ToDescription(),
            TransactionType = TransactionType.WalletCharge.ToDescription(),
            UserId = new ObjectId(request.UserId),
            Created = DateTime.UtcNow,
            CreatedBy = request.UserName,
            CreatedById = ObjectId.Parse(request.UserId)
        };
        await _uow.GenericRepository<Domain.Aggregates.TransactionAggregate.Transaction>()
            .InsertOneAsync(transactionWallet, null, cancellationToken);
        return transactionWallet.Id;
    }
}