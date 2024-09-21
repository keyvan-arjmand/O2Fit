using Common.Enums.TypeEnums;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Interfaces.Services;

namespace Payment.Application.PackagesNutritionist.V1.Commands.SoftDeletePackageNutritionist;

public class DeletePackageNutritionistCommandHandler : IRequestHandler<DeletePackageNutritionistCommand>
{
    private readonly IUnitOfWork _uow;

    public DeletePackageNutritionistCommandHandler( IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DeletePackageNutritionistCommand request, CancellationToken cancellationToken)
    {
        var package = await _uow.GenericRepository<Package>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (package == null) throw new NotFoundException("Package Not Found");
        if (!package.PackageType.Equals(PackageType.NutritionistPack.ToDescription())) throw new NotFoundException("package Not exist in DietNutritionist");

        await _uow.GenericRepository<Package>().SoftDeleteByIdAsync(request.Id, package, null, cancellationToken);
    }
}