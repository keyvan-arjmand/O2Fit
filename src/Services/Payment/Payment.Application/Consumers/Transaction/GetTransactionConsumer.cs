using EventBus.Messages.Contracts.Services.Payments.Package;
using EventBus.Messages.Contracts.Services.Payments.Transaction;
using MassTransit;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Mapping;
using Payment.Application.Dtos;
using Payment.Application.Transactions.V1.Query.GetById;

namespace Payment.Application.Consumers.Transaction;

public class GetTransactionConsumer : IConsumer<GetTransactionById>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetTransactionConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<GetTransactionById> context)
    {
        var transaction = await _mediator.Send(new GetByIdTransactionQuery { Id = context.Message.Id });
        if (transaction != null)
        {
            await context.RespondAsync<GetTransactionByIdResult>(
                _mapper.Map<TransactionDto, GetTransactionByIdResult>(transaction));
        }
        else
        {
            await context.RespondAsync<GetTransactionByIdResult>(new GetTransactionByIdResult
            {
                Amount = 0,
                DateTime = new DateTime(),
                Discount = 0,
                FinalAmount = 0,
                Id = string.Empty,
                Wage = 0,
                SaleReferenceId = string.Empty,
                TraceNo = string.Empty,
                UserId = string.Empty,
                DiscountCode = string.Empty,
                Bank = string.Empty,
                CurrencyCode = string.Empty,
                CurrencyId = string.Empty
            });
        }
    }
}