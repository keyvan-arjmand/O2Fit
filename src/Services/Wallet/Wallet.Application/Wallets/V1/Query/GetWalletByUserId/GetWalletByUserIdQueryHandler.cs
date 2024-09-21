using MongoDB.Bson;
using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Application.Common.Mapping;
using Wallet.Application.Dtos;
using Wallet.Application.Wallets.V1.Query.GetWalletById;

namespace Wallet.Application.Wallets.V1.Query.GetWalletByUserId;

public class GetWalletByUserIdQueryHandler : IRequestHandler<GetWalletByUserIdQuery, WalletDto>
{
    private readonly IUnitOfWork _uow;

    public GetWalletByUserIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<WalletDto> Handle(GetWalletByUserIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.WalletAggregate.Wallet>.Filter.Eq(x => x.UserId, ObjectId.Parse(request.UserId));
        var wallet = await _uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (wallet == null) throw new NotFoundException("Not Found wallet");
        return wallet.ToDto<WalletDto>();
    }
}