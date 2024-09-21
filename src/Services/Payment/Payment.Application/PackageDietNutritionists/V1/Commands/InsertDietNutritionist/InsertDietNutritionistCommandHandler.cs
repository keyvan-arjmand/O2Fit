using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Foods.DietCategory;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Domain.Aggregates.PackageDietNutritionistAggregate;

namespace Payment.Application.PackageDietNutritionists.V1.Commands.InsertDietNutritionist;

public class InsertDietNutritionistCommandHandler : IRequestHandler<InsertDietNutritionistCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _client;
    private readonly IRequestClient<GetDietCategoryById> _dietCategoryClient;

    public InsertDietNutritionistCommandHandler(IUnitOfWork uow, IRequestClient<GetCurrencyByCode> client,
        IRequestClient<GetDietCategoryById> dietCategoryClient)
    {
        _uow = uow;
        _client = client;
        _dietCategoryClient = dietCategoryClient;
    }

    public async Task<string> Handle(InsertDietNutritionistCommand request, CancellationToken cancellationToken)
    {
        var currency = await _client.GetResponse<GetCurrencyByCodeResult>(new GetCurrencyByCode
        {
            CurrencyCode = request.CurrencyCode
        }, cancellationToken);
        if (currency.Message.Id == null)
            throw new NotFoundException($"CurrencyName Not Valid Code:{request.CurrencyCode}");
        var diet = await _dietCategoryClient.GetResponse<GetDietCategoryByIdResult>(new GetDietCategoryById
        {
            Id = request.DietCategoryId.ToString()!
        }, cancellationToken);
        if (string.IsNullOrEmpty(diet.Message.Id)) throw new NotFoundException("dietId Not Valid");

        var package = new DietNutritionist
        {
            CurrencyCode = currency.Message.CurrencyCode,
            IsActive = request.IsActive,
            PackageType = PackageType.NutritionistDietPack,
            Price = request.Price,
            TranslationDescription = new TranslationDietNutritionist
            {
                Arabic = request.TranslationDescription.Arabic,
                English = request.TranslationDescription.English,
                Persian = request.TranslationDescription.Persian
            },
            TranslationName = new TranslationDietNutritionist
            {
                Arabic = request.TranslationName.Arabic,
                English = request.TranslationName.English,
                Persian = request.TranslationName.Persian
            },
            Duration = DurationPackage.DurationNutritionist.GetDuration(),
            DietCategoryId = diet.Message.Id.StringToObjectId(),
            DietCategoryName = new TranslationDietNutritionist
            {
                Arabic = diet.Message.DietCategoryNameArabic,
                English = diet.Message.DietCategoryNameEnglish,
                Persian = diet.Message.DietCategoryNamePersian
            }
        };
        await _uow.GenericRepository<DietNutritionist>().InsertOneAsync(package, null, cancellationToken);

        return package.Id;
    }
}