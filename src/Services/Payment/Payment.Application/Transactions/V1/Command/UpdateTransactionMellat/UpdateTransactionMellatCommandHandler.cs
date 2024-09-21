using System.Transactions;
using Common.Enums.TypeEnums;
using EventBus.Messages.Events.Services.Discount.Command.SubTrackUsableCountDiscount;
using EventBus.Messages.Events.Services.Order;
using EventBus.Messages.Events.Services.Wallet;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Domain.Enums;

namespace Payment.Application.Transactions.V1.Command.UpdateTransactionMellat;

public class UpdateTransactionMellatCommandHandler : IRequestHandler<UpdateTransactionMellatCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateTransactionMellatCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateTransactionMellatCommand request, CancellationToken cancellationToken)
    {
    //     var transaction = await _uow.GenericRepository<TransactionDietPackage>()
    //         .GetByIdAsync(request.TransactionId, cancellationToken);
    //     if (transaction == null) throw new NotFoundException("Not Found transaction");
    //
    //     transaction.SaleReferenceId = request.MellatResult.SaleReferenceId;
    //     transaction.RefNum = request.MellatResult.RefId;
    //     transaction.ResNum = request.MellatResult.ResCode;
    //     transaction.Bank = Bank.Mellat.ToDescription();
    //     transaction.FinalState = request.Status.ToDescription();
    //     transaction.PaymentTime = DateTime.UtcNow;
    //
    //     if (request.Status == PaymentResult.Success)
    //     {
    //         transaction.AddDomainEvent(new WalletTransactionCreatedEvent()
    //         {
    //             PaymentType = transaction.PaymentFor.ToEnum<PaymentType>(),
    //             UserType = transaction.UserType.ToEnum<UserType>(),
    //             PackageId = transaction.PackageId.ToString(),
    //             TransactionId = transaction.Id,
    //             Username = request.UserName
    //         });
    //         if (!string.IsNullOrEmpty(transaction.DiscountCode))
    //         {
    //             transaction.AddDomainEvent(new SubTrackedUsableCountDiscountEvent()
    //             {
    //                 DiscountCode = transaction.DiscountCode,
    //                 UserId = transaction.CreatedById.ToString(),
    //                 Username = request.UserName,
    //             });
    //         }
    //     }
    //
    //     await _uow.GenericRepository<TransactionDietPackage>().UpdateOneAsync(x => x.Id == request.TransactionId,
    //         transaction, new Expression<Func<TransactionDietPackage, object>>[]
    //         {
    //             x => x.SaleReferenceId,
    //             x => x.RefNum,
    //             x => x.ResNum,
    //             x => x.Bank,
    //             x => x.FinalState,
    //             x => x.PaymentTime,
    //         }, null, cancellationToken);
    }
}