using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Interfaces.Services;
using Payment.Application.PackageDietNutritionists.V1.Commands.SoftDeleteDietNutritionist;
using Payment.Domain.Aggregates.PackageDietNutritionistAggregate;
using Payment.Domain.Aggregates.PackageDietO2FitAggregate;

namespace Payment.Application.PackageDietO2Fits.V1.Commands.SoftDeleteDietO2Fit;

public class SoftDeleteDietO2FitCommandHandler : IRequestHandler<SoftDeleteDietO2FitCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IResponseCacheService _responseCacheService;
    private readonly IMapper _mapper;

    public SoftDeleteDietO2FitCommandHandler(IResponseCacheService responseCacheService, IUnitOfWork uow, IMapper mapper)
    {
        _responseCacheService = responseCacheService;
        _uow = uow;
        _mapper = mapper;
    }

    public async Task Handle(SoftDeleteDietO2FitCommand request, CancellationToken cancellationToken)
    {
        var package = await _uow.GenericRepository<DietO2Fit>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (package == null) throw new NotFoundException("Package Not Found");
        package.IsDelete = true;
        await _uow.GenericRepository<DietO2Fit>().SoftDeleteByIdAsync(request.Id, package, null, cancellationToken);
    }
}