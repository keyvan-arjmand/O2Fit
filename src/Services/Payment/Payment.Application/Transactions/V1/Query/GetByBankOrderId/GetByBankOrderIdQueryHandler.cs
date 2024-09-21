using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Dtos;

namespace Payment.Application.Transactions.V1.Query.GetByBankOrderId;

public class GetByBankOrderIdQueryHandler : IRequestHandler<GetByBankOrderIdQuery, TransactionDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetByBankOrderIdQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<TransactionDto> Handle(GetByBankOrderIdQuery request, CancellationToken cancellationToken)
    {
        // var filter = Builders<TransactionDietPackage>.Filter.Eq(x => x.BankOrderId, request.BankOrderId);
        // var transaction = await _uow.GenericRepository<TransactionDietPackage>()
        //     .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        // if (transaction == null) throw new NotFoundException("Not Found transaction");
        // var result = _mapper.Map<TransactionDietPackage, TransactionDto>(transaction);
        // result.UserName = transaction.CreatedBy;
        // return result;
        return new TransactionDto();
    }
}