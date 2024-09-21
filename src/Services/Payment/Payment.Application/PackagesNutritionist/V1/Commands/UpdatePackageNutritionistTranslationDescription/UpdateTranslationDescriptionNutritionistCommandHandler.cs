using Common.Enums.TypeEnums;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;

namespace Payment.Application.PackagesNutritionist.V1.Commands.UpdatePackageNutritionistTranslationDescription;

public class UpdateTranslationDescriptionNutritionistCommandHandler : IRequestHandler<UpdateTranslationDescriptionNutritionistCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateTranslationDescriptionNutritionistCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateTranslationDescriptionNutritionistCommand request, CancellationToken cancellationToken)
    {
        var package = await _uow.GenericRepository<Package>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (package == null) throw new NotFoundException("Not Found Package");
        if (!package.PackageType.Equals(PackageType.NutritionistPack.ToDescription())) throw new NotFoundException("package Not exist in DietNutritionist");

        package.TranslationDescription.Arabic = request.TranslationDescription.Arabic;
        package.TranslationDescription.Persian = request.TranslationDescription.Persian;
        package.TranslationDescription.English = request.TranslationDescription.English;

        await _uow.GenericRepository<Package>().UpdateOneAsync(x => x.Id == request.Id,
             package, new Expression<Func<Package, object>>[]
             {
                x => x.TranslationDescription.Arabic ,
                x => x.TranslationDescription.Persian,
                x => x.TranslationDescription.English,
             }, null, cancellationToken);
    }
}