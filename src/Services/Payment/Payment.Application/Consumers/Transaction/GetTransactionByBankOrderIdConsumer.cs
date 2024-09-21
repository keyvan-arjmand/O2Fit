using EventBus.Messages.Contracts.Services.Payments.Transaction;
using MassTransit;
using Payment.Application.Dtos;
using Payment.Application.Transactions.V1.Query.GetByBankOrderId;

namespace Payment.Application.Consumers.Transaction;

public class GetTransactionByBankOrderIdConsumer : IConsumer<GetTransactionByBankOrderId>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetTransactionByBankOrderIdConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<GetTransactionByBankOrderId> context)
    {
        var transAction = await _mediator.Send(new GetByBankOrderIdQuery() { BankOrderId = context.Message.BankOrderId });

        if (transAction != null)
        {
            await context.RespondAsync<GetTransactionByBankOrderIdResult>(_mapper.Map<TransactionDto, GetTransactionByBankOrderIdResult>(transAction));
        }
        else
        {
            await context.RespondAsync<GetTransactionByBankOrderIdResult>(new GetTransactionByBankOrderIdResult
            {
                Amount = 0,
                Discount = 0,
                FinalAmount = 0,
                Id = string.Empty,
                Wage = 0,
                SaleReferenceId = string.Empty,
                TraceNo = string.Empty,
                UserId = string.Empty,
                DiscountCode = string.Empty,
                Bank = string.Empty,
                BankOrderId =0,

            });
        }
    }
}


