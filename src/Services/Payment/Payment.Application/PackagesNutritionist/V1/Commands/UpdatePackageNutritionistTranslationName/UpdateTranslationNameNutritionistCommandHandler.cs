using Common.Enums.TypeEnums;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;

namespace Payment.Application.PackagesNutritionist.V1.Commands.UpdatePackageNutritionistTranslationName;

public class UpdateTranslationNameNutritionistCommandHandler : IRequestHandler<UpdateTranslationNameNutritionistCommand>
{
    private readonly IUnitOfWork _uow;


    public UpdateTranslationNameNutritionistCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateTranslationNameNutritionistCommand request, CancellationToken cancellationToken)
    {
        var package = await _uow.GenericRepository<Package>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (package == null) throw new NotFoundException("Not Found Package");
        if (!package.PackageType.Equals(PackageType.NutritionistPack.ToDescription()))
            throw new NotFoundException("package Not exist in DietNutritionist");

        package.TranslationName.Arabic = request.TranslationName.Arabic;
        package.TranslationName.Persian = request.TranslationName.Persian;
        package.TranslationName.English = request.TranslationName.English;

        await _uow.GenericRepository<Package>().UpdateOneAsync(x => x.Id == request.Id,
            package, new Expression<Func<Package, object>>[]
            {
                x => x.TranslationName.Arabic,
                x => x.TranslationName.Persian,
                x => x.TranslationName.English
            }, null, cancellationToken);
    }
}