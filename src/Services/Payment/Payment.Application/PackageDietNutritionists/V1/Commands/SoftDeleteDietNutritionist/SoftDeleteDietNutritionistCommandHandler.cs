using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Interfaces.Services;
using Payment.Domain.Aggregates.PackageDietNutritionistAggregate;

namespace Payment.Application.PackageDietNutritionists.V1.Commands.SoftDeleteDietNutritionist;

public class SoftDeleteDietNutritionistCommandHandler : IRequestHandler<SoftDeleteDietNutritionistCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IResponseCacheService _responseCacheService;
    private readonly IMapper _mapper;

    public SoftDeleteDietNutritionistCommandHandler(IResponseCacheService responseCacheService, IUnitOfWork uow, IMapper mapper)
    {
        _responseCacheService = responseCacheService;
        _uow = uow;
        _mapper = mapper;
    }

    public async Task Handle(SoftDeleteDietNutritionistCommand request, CancellationToken cancellationToken)
    {
        var package = await _uow.GenericRepository<DietNutritionist>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (package == null) throw new NotFoundException("Package Not Found");
        package.IsDelete = true;
        await _uow.GenericRepository<DietNutritionist>().SoftDeleteByIdAsync(request.Id, package, null, cancellationToken);
    }
}