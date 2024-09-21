using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;

namespace Discount.Application.DiscountO2Fits.V1.Commands.ActivateDiscountO2Fit;

public class ActivateDiscountO2FitCommandHandler : IRequestHandler<ActivateDiscountO2FitCommand>
{
    private readonly IUnitOfWork _uow;

    public ActivateDiscountO2FitCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(ActivateDiscountO2FitCommand request, CancellationToken cancellationToken)
    {
        var discount = await _uow.GenericRepository<DiscountO2Fit>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (discount == null) throw new NotFoundException("discount Not Found");
        discount.IsActive = request.IsActive;
        await _uow.GenericRepository<DiscountO2Fit>()
            .UpdateOneAsync(x => x.Id == request.Id, discount, new Expression<Func<DiscountO2Fit, object>>[]
            {
                x => x.IsActive,
            }, null, cancellationToken).ConfigureAwait(false);
    }
}