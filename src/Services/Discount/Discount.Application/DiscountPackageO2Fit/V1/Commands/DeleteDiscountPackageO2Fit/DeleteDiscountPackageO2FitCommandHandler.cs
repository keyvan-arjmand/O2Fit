using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;

namespace Discount.Application.DiscountPackageO2Fit.V1.Commands.DeleteDiscountPackageO2Fit;

public class DeleteDiscountPackageO2FitCommandHandler : IRequestHandler<DeleteDiscountPackageO2FitCommand>
{
    private readonly IUnitOfWork _uow;


    public DeleteDiscountPackageO2FitCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DeleteDiscountPackageO2FitCommand request, CancellationToken cancellationToken)
    {
        var discountPackage = await _uow.GenericRepository<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>().GetByIdAsync(request.Id, cancellationToken);
        if (discountPackage == null) throw new NotFoundException("discount Not Found");

        discountPackage.IsDelete = true;
        await _uow.GenericRepository<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>()
            .SoftDeleteByIdAsync(request.Id, discountPackage, null, cancellationToken).ConfigureAwait(false);
    }
}