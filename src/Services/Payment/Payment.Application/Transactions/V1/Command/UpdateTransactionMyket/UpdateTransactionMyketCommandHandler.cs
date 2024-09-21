using System.Transactions;
using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Payments.Package;
using EventBus.Messages.Events.Services.Identity.Package;
using EventBus.Messages.Events.Services.Order;
using EventBus.Messages.Events.Services.Wallet;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Transactions.V1.Command.UpdateTransactionCafeBazar;
using Payment.Domain.Enums;

namespace Payment.Application.Transactions.V1.Command.UpdateTransactionMyket;

public class UpdateTransactionMyketCommandHandler : IRequestHandler<UpdateTransactionMyketCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetByIdPackage> _clientPackage;

    public UpdateTransactionMyketCommandHandler(IUnitOfWork uow, IRequestClient<GetByIdPackage> clientPackage)
    {
        _uow = uow;
        _clientPackage = clientPackage;
    }

    public async Task Handle(UpdateTransactionMyketCommand request, CancellationToken cancellationToken)
    {
        // var transaction = await _uow.GenericRepository<TransactionDietPackage>()
        //     .GetByIdAsync(request.TransactionId, cancellationToken);
        // if (transaction == null) throw new NotFoundException("Not Found transaction");
        //
        // if (request.IsSuccess)
        // {
        //     transaction.State = "Success";
        //     transaction.FinalState = PaymentResult.Success.ToDescription();
        //     transaction.SaleReferenceId = request.SaleReferenceId;
        //     transaction.PaymentTime = DateTime.UtcNow;
        //
        //     // transaction.AddDomainEvent(new UserPackageRegistered
        //     // {
        //     //     PackageType = transaction.PackageType.ToEnum<PackageType>(),
        //     //     UserId = transaction.UserId.ToString(),
        //     //     ExpireDate = DateTime.Now.AddDays((int)transaction.DurationPackage)
        //     // });
        //
        //     await _uow.GenericRepository<TransactionDietPackage>().UpdateOneAsync(x => x.Id == transaction.Id,
        //         transaction, new Expression<Func<TransactionDietPackage, object>>[]
        //         {
        //             x => x.State,
        //             x => x.FinalState,
        //             x => x.SaleReferenceId
        //         }, null, cancellationToken);
        // }
        // else
        // {
        //     transaction.State = "Failed";
        //     transaction.FinalState = PaymentResult.Failed.ToDescription();
        //     transaction.SaleReferenceId = request.SaleReferenceId;
        //     transaction.PaymentTime = DateTime.UtcNow;
        //
        //     await _uow.GenericRepository<TransactionDietPackage>().UpdateOneAsync(x => x.Id == transaction.Id,
        //         transaction, new Expression<Func<TransactionDietPackage, object>>[]
        //         {
        //             x => x.State,
        //             x => x.FinalState,
        //             x => x.SaleReferenceId,
        //             x => x.PaymentTime
        //         }, null, cancellationToken);
        // }
    }
}