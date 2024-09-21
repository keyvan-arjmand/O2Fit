using Common.Enums.TypeEnums;
using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountPackageNutritionistAggregate;
using Discount.Domain.Aggregates.DiscountPackageO2FitAggregate;

namespace Discount.Application.DiscountPackagesNutritionist.V1.Commands.DeleteDiscountPackageNutritionist;

public class DeleteDiscountPackageNutritionistCommandHandler : IRequestHandler<DeleteDiscountPackageNutritionistCommand>
{
    private readonly IUnitOfWork _uow;


    public DeleteDiscountPackageNutritionistCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DeleteDiscountPackageNutritionistCommand request, CancellationToken cancellationToken)
    {
        var discountPackage = await _uow.GenericRepository<DiscountPackageNutritionist>().GetByIdAsync(request.Id, cancellationToken);
        if (discountPackage == null) throw new NotFoundException("discount Not Found");
        discountPackage.IsDelete = true;
        await _uow.GenericRepository<DiscountPackageNutritionist>()
            .SoftDeleteByIdAsync(request.Id, discountPackage, null, cancellationToken).ConfigureAwait(false);
    }
}