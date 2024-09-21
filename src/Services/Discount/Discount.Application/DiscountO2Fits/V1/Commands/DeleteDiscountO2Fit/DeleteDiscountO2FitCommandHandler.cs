using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;

namespace Discount.Application.DiscountO2Fits.V1.Commands.DeleteDiscountO2Fit;

public class DeleteDiscountO2FitCommandHandler:IRequestHandler<DeleteDiscountO2FitCommand>
{
    private readonly IUnitOfWork _uow;

    public DeleteDiscountO2FitCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task Handle(DeleteDiscountO2FitCommand request, CancellationToken cancellationToken)
    {
        var discount = await _uow.GenericRepository<DiscountO2Fit>().GetByIdAsync(request.Id, cancellationToken);
        if (discount == null) throw new NotFoundException("discount Not Found");
        discount.IsDelete = true;
        await _uow.GenericRepository<DiscountO2Fit>()
            .SoftDeleteByIdAsync(request.Id, discount, null, cancellationToken).ConfigureAwait(false);
    }
}