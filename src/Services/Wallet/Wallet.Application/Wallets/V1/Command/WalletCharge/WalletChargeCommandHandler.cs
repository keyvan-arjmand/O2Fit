using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Events.Services.Identity.Package;
using EventBus.Messages.Events.Services.Order;
using MassTransit;
using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Application.Wallets.V1.Command.CreateWallet;

namespace Wallet.Application.Wallets.V1.Command.WalletCharge;

public class WalletChargeCommandHandler : IRequestHandler<WalletChargeCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _client;

    public WalletChargeCommandHandler(IUnitOfWork uow, IRequestClient<GetCurrencyByCode> client)
    {
        _uow = uow;
        _client = client;
    }

    public async Task Handle(WalletChargeCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (wallet == null) throw new NotFoundException("wallet not found");
        if (!wallet.CurrencyCode.Equals(request.CurrencyCode)) throw new AppException("Currency not Valid");

        wallet.Amount += request.Amount;
        wallet.LastModified = DateTime.UtcNow;
        wallet.LastModifiedBy = request.UserName;
        wallet.LastModifiedById = wallet.UserId;
        if (request.PaymentType == PaymentType.PackageNutritionist)
        {
            wallet.AddDomainEvent(new UserPackageRegistered
            {
                PackageType = request.PackageType,
                UserId = request.UserId,
                ExpireDate = DateTime.Now.AddDays((int)request.Duration)
            });
        }
        await _uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
            .UpdateOneAsync(x => x.Id == request.Id, wallet,
                new Expression<Func<Domain.Aggregates.WalletAggregate.Wallet, object>>[]
                {
                    x => x.Amount,
                }, null, cancellationToken);

    }
}