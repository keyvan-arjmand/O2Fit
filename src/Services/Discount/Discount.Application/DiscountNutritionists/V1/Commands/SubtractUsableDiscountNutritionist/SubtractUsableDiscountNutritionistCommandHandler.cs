using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.DiscountO2Fits.V1.Commands.SubtractUsableDiscountO2Fit;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;
using Discount.Domain.ValueObjects;
using static System.DateTime;

namespace Discount.Application.DiscountNutritionists.V1.Commands.SubtractUsableDiscountNutritionist;

public class SubtractUsableDiscountNutritionistCommandHandler : IRequestHandler<SubtractUsableDiscountO2FitCommand>
{
    private readonly IUnitOfWork _uow;

    public SubtractUsableDiscountNutritionistCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(SubtractUsableDiscountO2FitCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<DiscountO2Fit>.Filter.Eq(x => x.Code, new DiscountCode(request.Code));
        var discount = await _uow.GenericRepository<DiscountO2Fit>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (discount == null) throw new NotFoundException($"Not Found Discount {request.Code}");
        if (discount.UsableCount <= 0) throw new NotFoundException($"discount has expire {request.Code}");
        discount.UsableCount--;
        discount.LastModified = UtcNow;
        discount.LastModifiedBy = request.Username;
        discount.LastModifiedById = ObjectId.Parse(request.UserId);
        await _uow.GenericRepository<DiscountO2Fit>()
            .UpdateOneAsync(x => x.Id == discount.Id, discount, new Expression<Func<DiscountO2Fit, object>>[]
            {
                x => x.UsableCount,
                x => x.LastModified,
                x => x.LastModifiedBy,
                x => x.LastModifiedById,
            }, null, cancellationToken).ConfigureAwait(false);
    }
}