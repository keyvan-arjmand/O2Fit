using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Dtos;

namespace Payment.Application.Transactions.V1.Query.GetById;

public class GetByIdTransactionQueryHandler : IRequestHandler<GetByIdTransactionQuery, TransactionDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetByIdTransactionQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<TransactionDto> Handle(GetByIdTransactionQuery request, CancellationToken cancellationToken)
    {
        // var result = await _uow.GenericRepository<TransactionDietPackage>()
        //     .GetByIdAsync(request.Id, cancellationToken);
        // if (result == null) throw new NotFoundException("Transaction Not Found ");
        //
        // return _mapper.Map<TransactionDietPackage, TransactionDto>(result);
        return new TransactionDto();
    }
}