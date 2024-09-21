using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountNutritionistAggregate;

namespace Discount.Application.DiscountNutritionists.V1.Commands.ActivateDiscountNutritionist;

public class ActivateDiscountNutritionistCommandHandler : IRequestHandler<ActivateDiscountNutritionistCommand>
{
    private readonly IUnitOfWork _uow;

    public ActivateDiscountNutritionistCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(ActivateDiscountNutritionistCommand request, CancellationToken cancellationToken)
    {
        var discount = await _uow.GenericRepository<DiscountNutritionist>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (discount == null) throw new NotFoundException("discount Not Found");
        discount.IsActive = request.IsActive;
        await _uow.GenericRepository<DiscountNutritionist>()
            .UpdateOneAsync(x => x.Id == request.Id, discount, new Expression<Func<DiscountNutritionist, object>>[]
            {
                x => x.IsActive,
            }, null, cancellationToken).ConfigureAwait(false);
    }
}