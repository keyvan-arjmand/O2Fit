using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Application.Common.Mapping;
using Wallet.Application.Dtos;

namespace Wallet.Application.Wallets.V1.Query.GetWalletById;

public class GetWalletByIdQueryHandler : IRequestHandler<GetWalletByIdQuery, WalletDto>
{
    private readonly IUnitOfWork _uow;

    public GetWalletByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<WalletDto> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
    {
        var wallet = await _uow.GenericRepository<Domain.Aggregates.WalletAggregate.Wallet>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (wallet == null) throw new NotFoundException("Wallet Not Found");
        return wallet.ToDto<WalletDto>();
    }
}