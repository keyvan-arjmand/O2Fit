using Common.Enums.TypeEnums;
using EventBus.Messages.Events.Services.Discount.Command.SubTrackUsableCountDiscount;
using EventBus.Messages.Events.Services.Order;
using EventBus.Messages.Events.Services.Wallet;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Domain.Enums;

namespace Payment.Application.Transactions.V1.Command.UpdateTransactionSaman;

public class UpdateTransactionSamanCommandHandler : IRequestHandler<UpdateTransactionSamanCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateTransactionSamanCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateTransactionSamanCommand request, CancellationToken cancellationToken)
    {
        // var transaction = await _uow.GenericRepository<TransactionDietPackage>()
        //     .GetByIdAsync(request.TransactionId, cancellationToken);
        // if (transaction == null) throw new NotFoundException("Not Found transaction");
        // transaction.ResNum = request.SamanResult.ResNum;
        // transaction.TraceNo = request.SamanResult.TraceNo;
        // transaction.RefNum = request.SamanResult.RefNum;
        // transaction.SecurePan = request.SamanResult.SecurePan;
        // transaction.State = request.SamanResult.State;
        // transaction.StateCode = request.SamanResult.StateCode;
        // transaction.CId = request.SamanResult.CId;
        // transaction.SaleReferenceId = request.SamanResult.RefNum;
        // transaction.FinalState = request.Status.ToDescription();
        // transaction.Bank = Bank.Saman.ToDescription();
        // transaction.PaymentTime = DateTime.UtcNow;
        //
        // if (request.Status == PaymentResult.Success)
        // {
        //     transaction.AddDomainEvent(new WalletTransactionCreatedEvent()
        //     {
        //         PaymentType = transaction.PaymentFor.ToEnum<PaymentType>(),
        //         UserType = transaction.UserType.ToEnum<UserType>(),
        //         PackageId = transaction.PackageId.ToString(),
        //         TransactionId = transaction.Id,
        //         Username = request.UserName
        //     });
        //     if (!string.IsNullOrEmpty(transaction.DiscountCode))
        //     {
        //         // transaction.AddDomainEvent(new SubTrackedUsableCountDiscountEvent()
        //         // {
        //         //     DiscountCode = transaction.DiscountCode,
        //         //     UserId = transaction.UserId.ToString(),
        //         //     Username = request.UserName,
        //         // });
        //     }
        // }
        //
        // await _uow.GenericRepository<TransactionDietPackage>().UpdateOneAsync(x => x.Id == request.TransactionId,
        //     transaction, new Expression<Func<TransactionDietPackage, object>>[]
        //     {
        //         x => x.ResNum,
        //         x => x.TraceNo,
        //         x => x.RefNum,
        //         x => x.SecurePan,
        //         x => x.State,
        //         x => x.StateCode,
        //         x => x.CId,
        //         x => x.SaleReferenceId,
        //         x => x.Bank,
        //         x => x.FinalState,
        //     }, null, cancellationToken);
    }
}