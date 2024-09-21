using Wallet.Application.Common.Exceptions;
using Wallet.Application.Common.Interfaces.Persistence.UoW;
using Wallet.Application.Common.Mapping;
using Wallet.Application.Dtos;
using Wallet.Domain.Aggregates.TransactionCompanyAggregate;

namespace Wallet.Application.TransactionCompanies.V1.Query.GetTransactionCompanyById;

public class GetTransactionCompanyByIdCommandHandler : IRequestHandler<GetTransactionCompanyByIdCommand, TransactionCompDto>
{
    private readonly IUnitOfWork _uow;

    public GetTransactionCompanyByIdCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<TransactionCompDto> Handle(GetTransactionCompanyByIdCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _uow.GenericRepository<TransactionCompany>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (transaction == null) throw new NotFoundException("transaction Not 31232131232132134134");
        return transaction.ToDto<TransactionCompDto>();
    }
}