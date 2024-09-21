using Common.Enums.TypeEnums;
using EventBus.Messages.Events.Services.Discount.Command.DiscountInvitationCode;

namespace Identity.V2.Application.Users.V1.Commands.IncreaseReferralCountBuy;

public class IncreaseReferralCountBuyCommandHandler : IRequestHandler<IncreaseReferralCountBuyCommand>
{
    private readonly IUnitOfWork _uow;

    public IncreaseReferralCountBuyCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(IncreaseReferralCountBuyCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<User>.Filter.Eq(x => x.ReferralCode, request.ReferralInviter);
        var user = await _uow.UserGenericRepository<User>().GetSingleDocumentByFilterAsync(filter, cancellationToken);

        if (user == null)
            throw new NotFoundException(nameof(User), request.ReferralInviter);

        user.ReferralCountBuy += 1;

        AddDomainEvents(user);

        await _uow.UserGenericRepository<User>().UpdateOneAsync(x => x.ReferralCode == request.ReferralInviter, user,
            new Expression<Func<User, object>>[]
            {
                x => x.ReferralCountBuy
            }, null, cancellationToken);
    }
        
    private void AddDomainEvents(User user)
    {
        if (user.ReferralCountBuy % 3 == 0)
        {
            var invitedEvent = new CreatedDiscountInvitationCode
            {
                UserId = user.Id.ToString(),
                DiscountType = DiscountType.InvitedDiscountCode
            };
            user.AddDomainEvent(invitedEvent);
        }

        var inviterEvent = new CreatedDiscountInvitationCode
        {
            UserId = user.Id.ToString(),
            DiscountType = DiscountType.InviterDiscountCode
        };
        user.AddDomainEvent(inviterEvent);
    }
}