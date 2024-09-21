using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Events.Services.Order;
using MassTransit;
using MongoDB.Bson;
using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Application.Wallets.V1.Command.WalletCharge;

namespace Wallet.Application.Wallets.V1.Command.SubtractWallet;

public class SubtractWalletCommandHandler : IRequestHandler<SubtractWalletCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _client;

    public SubtractWalletCommandHandler(IUnitOfWork uow, IRequestClient<GetCurrencyByCode> client)
    {
        _uow = uow;
        _client = client;
    }

    public async Task Handle(SubtractWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
           .GetByIdAsync(request.Id, cancellationToken);
        if (wallet == null) throw new NotFoundException("wallet not found");
        if (!wallet.CurrencyCode.Equals(request.CurrencyCode)) throw new AppException("Currency not Valid");
        if (wallet.Amount < request.SubtractAmount) throw new AppException("The balance of the wallet is not enough");

        wallet.Amount -= request.SubtractAmount;
        wallet.LastModified = DateTime.UtcNow;
        wallet.LastModifiedBy = request.UserName;
        wallet.LastModifiedById = wallet.UserId;
        wallet.AddDomainEvent(new OrderCreatedEvent
        {
            WalletTransactionId = request.TransactionCompId,
            PaymentTransactionId = request.PaymentTransactionId,
            Username = request.UserName,
            UserId = wallet.UserId.ToString()
        });

        await _uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
            .UpdateOneAsync(x => x.Id == request.Id, wallet,
                new Expression<Func<Domain.Aggregates.WalletAggregate.Wallet, object>>[]
                {
                    x => x.Amount,
                }, null, cancellationToken);
    }
}