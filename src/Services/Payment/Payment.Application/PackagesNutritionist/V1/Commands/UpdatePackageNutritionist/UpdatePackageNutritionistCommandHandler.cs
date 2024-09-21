using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Foods.DietCategory;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Interfaces.Services;
using Payment.Domain.ValueObjects;

namespace Payment.Application.PackagesNutritionist.V1.Commands.UpdatePackageNutritionist;

public class UpdatePackageNutritionistCommandHandler : IRequestHandler<UpdatePackageNutritionistCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _client;
    private readonly IRequestClient<GetCurrencyByCode> _dietCategoryClient;

    public UpdatePackageNutritionistCommandHandler(IUnitOfWork uow, IRequestClient<GetCurrencyByCode> client,
        IRequestClient<GetCurrencyByCode> dietCategoryClient)
    {
        _uow = uow;
        _client = client;
        _dietCategoryClient = dietCategoryClient;
    }

    public async Task Handle(UpdatePackageNutritionistCommand request, CancellationToken cancellationToken)
    {
        var currency = await _client.GetResponse<
            GetCurrencyByCodeResult>(new GetCurrencyByCode
        {
            CurrencyCode = request.CurrencyCode
        }, cancellationToken);
        if (string.IsNullOrEmpty(currency.Message.Id)) throw new NotFoundException("CurrencyName Not Valid");
        var diet = await _dietCategoryClient.GetResponse<GetDietCategoryByIdResult>(new GetDietCategoryById
        {
            Id = request.DietCategoryId
        }, cancellationToken);
        if (string.IsNullOrEmpty(diet.Message.Id)) throw new NotFoundException("dietId Not Valid");

        var package = await _uow.GenericRepository<Package>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (package == null) throw new NotFoundException("Package Not Found");
        if (!package.PackageType.Equals(PackageType.NutritionistPack.ToDescription()))
            throw new NotFoundException("package Not exist in DietNutritionist");

        package.IsActive = request.IsActive;
        package.Sort = request.Sort;
        package.CurrencyCode = currency.Message.CurrencyCode;
        package.CurrencyId = currency.Message.Id.StringToObjectId();
        package.Price = request.Price;
        package.DietCategoryId = diet.Message.Id.StringToObjectId();
        package.IsPromote = request.IsPromote;
        package.CountryIds = request.CountryIds;
        package.TranslationDescription = new Translation
        {
            Arabic = request.Description.Arabic,
            English = request.Description.English,
            Persian = request.Description.Persian,
        };
        package.TranslationName = new Translation
        {
            Arabic = request.Name.Arabic,
            English = request.Name.English,
            Persian = request.Name.Persian,
        };
        package.DietCategoryName = new Translation
        {
            Arabic = diet.Message.DietCategoryNameArabic,
            English = diet.Message.DietCategoryNameEnglish,
            Persian = diet.Message.DietCategoryNamePersian
        };

        await _uow.GenericRepository<Package>()
            .UpdateOneAsync(x => x.Id == request.Id, package, new Expression<Func<Package, object>>[]
            {
                x => x.IsActive,
                x => x.Sort,
                x => x.CurrencyCode,
                x => x.CurrencyId,
                x => x.Price,
                x => x.IsPromote,
                x => x.TranslationName.Persian,
                x => x.TranslationName.English,
                x => x.TranslationName.Arabic,
                x => x.TranslationDescription.Persian,
                x => x.TranslationDescription.English,
                x => x.TranslationDescription.Arabic,
                x => x.DietCategoryName.Persian,
                x => x.DietCategoryName.English,
                x => x.DietCategoryName.Arabic,
                x => x.DietCategoryId,
                x => x.CountryIds
            }, null, cancellationToken).ConfigureAwait(false);
    }
}