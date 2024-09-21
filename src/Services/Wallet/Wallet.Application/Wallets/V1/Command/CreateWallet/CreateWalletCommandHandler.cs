using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using MassTransit;
using MongoDB.Bson;
using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;

namespace Wallet.Application.Wallets.V1.Command.CreateWallet;

public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _client;

    public CreateWalletCommandHandler(IUnitOfWork uow, IRequestClient<GetCurrencyByCode> client)
    {
        _uow = uow;
        _client = client;
    }

    public async Task<string> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        if (await _uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
                .AnyAsync(x => x.UserId == ObjectId.Parse(request.UserId), cancellationToken))
            throw new AppException($"User Already Exist{request.UserId}");

        var currency = await _client.GetResponse<GetCurrencyByCodeResult>(new GetCurrencyByCode
        {
            CurrencyCode = request.CurrencyCode
        }, cancellationToken);
        if (currency == null) throw new NotFoundException("Not Found Currency");

        var wallet = new Domain.Aggregates.WalletAggregate.Wallet
        {
            Amount = 0,
            CurrencyId = new ObjectId(currency.Message.Id),
            CurrencyCode = currency.Message.CurrencyCode,
            UserType = request.UserType.ToDescription(),
            UserId = new ObjectId(request.UserId),
            CountryId = request.CountryId,
            Created = DateTime.UtcNow,
            CreatedBy = request.UserName,
            CreatedById = ObjectId.Parse(request.UserId)
        };
        await _uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
            .InsertOneAsync(wallet, null, cancellationToken);
        return wallet.Id;
    }
}