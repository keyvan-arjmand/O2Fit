using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Payments.Package;
using MassTransit;
using MongoDB.Bson;
using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Domain.Aggregates.TransactionAggregate;

namespace Wallet.Application.Transactions.V1.Commands.SubTrackWalletForPackage;

public class SubTrackWalletForPackageCommandHandler : IRequestHandler<SubTrackWalletForPackageCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetByIdPackage> _clientPackage;

    public SubTrackWalletForPackageCommandHandler(IUnitOfWork uow, IRequestClient<GetByIdPackage> clientPackage)
    {
        _uow = uow;
        _clientPackage = clientPackage;
    }

    public async Task<string> Handle(SubTrackWalletForPackageCommand request, CancellationToken cancellationToken)
    {
        var package = await _clientPackage.GetResponse<GetByIdPackageResult>(new GetByIdPackage
        {
            Id = request.PackageId
        }, cancellationToken);
        if (string.IsNullOrEmpty(package.Message.Id)) throw new NotFoundException("package Not Found");

        var filter = Builders<Domain.Aggregates.WalletAggregate.Wallet>.Filter.Eq(x => x.UserId, new ObjectId(request.UserId));
        var wallet = await _uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (wallet == null) throw new NotFoundException("wallet Not Found");

        var transaction = new Transaction
        {
            Amount = request.Amount,
            CurrencyId = wallet.CurrencyId,
            CurrencyCode = wallet.CurrencyCode,
            DateTime = DateTime.Now,
            PackageId = new ObjectId(package.Message.Id),
            PaymentFor = request.PaymentType.ToDescription(),
            TransactionType = TransactionType.WalletSubTrack.ToDescription(),
            UserId = new ObjectId(request.UserId),
            UserType = request.UserType.ToDescription(),
            WalletId = new ObjectId(wallet.Id),
            CreatedBy = request.UserName,
            CreatedById = ObjectId.Parse(request.UserId),
            Created = DateTime.UtcNow
        };
        await _uow.GenericRepository<Transaction>()
            .InsertOneAsync(transaction, null, cancellationToken);
        return transaction.Id;
    }
}