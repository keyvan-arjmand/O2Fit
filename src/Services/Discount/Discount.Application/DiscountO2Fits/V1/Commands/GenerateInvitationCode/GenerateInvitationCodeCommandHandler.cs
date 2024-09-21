using Common.Constants.Currency;
using Common.Constants.Discount;
using Common.Enums.TypeEnums;
using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;
using Discount.Domain.ValueObjects;
using EventBus.Messages.Contracts.Services.Foods;
using EventBus.Messages.Contracts.Services.Nutritionist.PackageRequest;
using MassTransit;
using static System.DateTime;

namespace Discount.Application.DiscountO2Fits.V1.Commands.GenerateInvitationCode;

public class GenerateInvitationCodeCommandHandler : IRequestHandler<GenerateInvitationCodeCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetUserById> _clientUser;

    public GenerateInvitationCodeCommandHandler(IUnitOfWork uow, IRequestClient<GetUserById> clientUser)
    {
        _uow = uow;
        _clientUser = clientUser;
    }

    public async Task Handle(GenerateInvitationCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await _clientUser.GetResponse<GetUserByIdResult>(new GetUserById
        {
            UserId = request.UserId
        }, cancellationToken);
        if (string.IsNullOrEmpty(user.Message.Id)) throw new NotFoundException("User Not Found");
        var percentDiscount = request.DiscountType == DiscountType.InvitedDiscountCode
            ? GenerateCodeConstants.DiscountInvitedPercent
            : GenerateCodeConstants.DiscountInviterPercent;

        var discountCode = new DiscountO2Fit
        {
            Code = new(await _uow.DiscountRepository().GenerationInvitationCode()),
            Percent = percentDiscount,
            StartDate = UtcNow,
            EndDateTime = UtcNow.AddDays(7),
            UserId = request.UserId.StringToObjectId(),
            UsableCount = 1,
            PackageType = PackageType.O2Package,
            DiscountType = request.DiscountType,
            CountryId = new(request.CountryId),
            Description = new DiscountO2FitTranslation
            {
                Persian = "کد تخفیف دعوت دوستان",
                English = "Discount code for inviting friends",
                Arabic = "كود الخصم لدعوة الأصدقاء"
            },
            IsActive = true,
            CreatedBy = request.Username,
            Created = UtcNow,
            CreatedById = request.UserId.StringToObjectId()
        };
        await _uow.GenericRepository<DiscountO2Fit>()
            .InsertOneAsync(discountCode, null, cancellationToken);
    }
}