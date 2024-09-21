using Common.Constants.Package;
using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Foods.DietCategory;
using EventBus.Messages.Contracts.Services.Nutritionist.PackageRequest;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Interfaces.Services;
using Payment.Domain.ValueObjects;

namespace Payment.Application.PackagesNutritionist.V1.Commands.CreatePackageNutritionist;

public class CreatePackageNutritionistCommandHandler : IRequestHandler<CreatePackageNutritionistCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _client;
    private readonly IRequestClient<GetCurrencyByCode> _dietCategoryClient;

    public CreatePackageNutritionistCommandHandler(IUnitOfWork uow, IRequestClient<GetCurrencyByCode> client,
        IRequestClient<GetCurrencyByCode> dietCategoryClient)
    {
        _uow = uow;
        _client = client;
        _dietCategoryClient = dietCategoryClient;
    }

    public async Task<string> Handle(CreatePackageNutritionistCommand request, CancellationToken cancellationToken)
    {
        var currency = await _client.GetResponse<GetCurrencyByCodeResult>(new GetCurrencyByCode
        {
            CurrencyCode = request.CurrencyCode
        }, cancellationToken);
        if (currency.Message.Id == null) throw new NotFoundException("CurrencyName Not Valid");
        var diet = await _dietCategoryClient.GetResponse<GetDietCategoryByIdResult>(new GetDietCategoryById
        {
            Id = request.DietCategoryId
        }, cancellationToken);
        if (string.IsNullOrEmpty(diet.Message.Id)) throw new NotFoundException("dietId Not Valid");
        var package = new Package
        {
            CurrencyCode = currency.Message.CurrencyCode,
            IsActive = request.IsActive,
            IsPromote = request.IsPromote,
            PackageType = PackageType.NutritionistPack.ToDescription(),
            Price = request.Price,
            Sort = request.Sort,
            TranslationDescription = new Translation
            {
                Arabic = request.Description.Arabic,
                English = request.Description.English,
                Persian = request.Description.Persian
            },
            TranslationName = new Translation
            {
                Arabic = request.Name.Arabic,
                English = request.Name.English,
                Persian = request.Name.Persian
            },
            Duration = DurationConstants.DurationNutritionist,
            CurrencyId = currency.Message.Id.StringToObjectId(),
            DietCategoryId = diet.Message.Id.StringToObjectId(),
            DietCategoryName = new Translation
            {
                Arabic = diet.Message.DietCategoryNameArabic,
                English = diet.Message.DietCategoryNameEnglish,
                Persian = diet.Message.DietCategoryNamePersian
            },
            CountryIds = new List<int>()
        };
        package.CountryIds = request.CountryIds;
        await _uow.GenericRepository<Package>().InsertOneAsync(package, null, cancellationToken);

        return package.Id;
    }
}