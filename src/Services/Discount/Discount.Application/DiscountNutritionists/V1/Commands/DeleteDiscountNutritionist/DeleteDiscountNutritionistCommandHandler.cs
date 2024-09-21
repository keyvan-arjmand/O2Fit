using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountNutritionistAggregate;

namespace Discount.Application.DiscountNutritionists.V1.Commands.DeleteDiscountNutritionist;

public class DeleteDiscountNutritionistCommandHandler:IRequestHandler<DeleteDiscountNutritionistCommand>
{
    private readonly IUnitOfWork _uow;

    public DeleteDiscountNutritionistCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task Handle(DeleteDiscountNutritionistCommand request, CancellationToken cancellationToken)
    {
        var discount = await _uow.GenericRepository<DiscountNutritionist>().GetByIdAsync(request.Id, cancellationToken);
        if (discount == null) throw new NotFoundException("discount Not Found");
        discount.IsDelete = true;
        await _uow.GenericRepository<DiscountNutritionist>()
            .SoftDeleteByIdAsync(request.Id, discount, null, cancellationToken).ConfigureAwait(false);
    }
}