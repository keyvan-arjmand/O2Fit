using Common.Enums.TypeEnums;
using EventBus.Messages.Events.Services.Wallet;
using MongoDB.Bson;
using Order.V2.Application.Common.Exceptions;
using Order.V2.Application.Common.Interfaces.Persistence.UoW;
using Order.V2.Application.Orders.V1.Command.InsertOrder;
using Order.V2.Domain.Enums;

namespace Order.V2.Application.Orders.V1.Command.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateOrderCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _uow.GenericRepository<Domain.Aggregates.OrderAggregate.Order>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (order == null) throw new NotFoundException("order Not Found");
        if (!order.FinalState.Equals(OrderState.Pending.ToDescription()))
            throw new AppException($"The status of the order has already been determined:{order.Id}");
        order.FinalState = request.OrderState.ToDescription();

        // if (request.OrderState == OrderState.Accepted)
        // {
        //     order.AddDomainEvent(new WalletChargedNutritionistEvent
        //     {
        //         NutritionistId = order.NutritionistId.ToString(),
        //         TransactionId = order.WalletTransactionId.ToString(),
        //         OrderId = order.Id,
        //         Username = request.Username,
        //     });
        // }
        // else if (request.OrderState == OrderState.Rejected)
        // {
        //     order.AddDomainEvent(new WalletChargedUserEvent()
        //     {
        //         TransactionId = order.WalletTransactionId.ToString(),
        //         OrderId = order.Id,
        //         Username = request.Username
        //     });
        // }
        order.LastModifiedBy = request.Username;
        order.LastModifiedById = ObjectId.Parse(request.UserId);
        order.LastModified = DateTime.UtcNow;
        await _uow.GenericRepository<Domain.Aggregates.OrderAggregate.Order>()
            .UpdateOneAsync(x => x.Id == request.Id, order,
                new Expression<Func<Domain.Aggregates.OrderAggregate.Order, object>>[]
                {
                    x => x.FinalState
                }, null, cancellationToken).ConfigureAwait(false);
    }
}