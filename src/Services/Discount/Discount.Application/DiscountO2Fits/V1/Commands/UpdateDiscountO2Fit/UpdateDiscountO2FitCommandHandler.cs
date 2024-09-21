using Common.Enums.TypeEnums;
using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;

namespace Discount.Application.DiscountO2Fits.V1.Commands.UpdateDiscountO2Fit;

public class UpdateDiscountO2FitCommandHandler:IRequestHandler<UpdateDiscountO2FitCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateDiscountO2FitCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateDiscountO2FitCommand request, CancellationToken cancellationToken)
    {
           var discount = await _uow.GenericRepository<DiscountO2Fit>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (discount == null) throw new NotFoundException("discount Not Found");

        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            discount.DiscountType = DiscountType.DiscountCodeGeneral;
            discount.UserId =ObjectId.Empty;
            discount.CountryId = request.CountryIds;
        }
        else
        {
            discount.DiscountType = DiscountType.DiscountCodePerUser;
            discount.UserId = request.UserId.StringToObjectId();
            discount.CountryId = new List<int>(-1);
        }
        discount.EndDateTime = request.EndDateTime;
        discount.StartDate = request.StartDate;
        discount.Percent = request.Percent;
        discount.IsActive = request.IsActive;
        discount.PackageType = request.PackageType;
        discount.Description.Arabic = request.Description.Arabic;
        discount.Description.Persian = request.Description.Persian;
        discount.Description.English = request.Description.English;
        discount.UsableCount = request.UsableCount;
        await _uow.GenericRepository<DiscountO2Fit>()
            .UpdateOneAsync(x => x.Id == request.Id, discount, new Expression<Func<DiscountO2Fit, object>>[]
            {
                x=>x.EndDateTime,
                x=>x.StartDate,
                x=>x.Percent,
                x=>x.IsActive,
                x=>x.Description,
                x=>x.UsableCount,
                x=>x.UserId,
                x=>x.PackageType,
                x=>x.DiscountType,
                x=>x.CountryId
            }, null, cancellationToken).ConfigureAwait(false);
    }
}