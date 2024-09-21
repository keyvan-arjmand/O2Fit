using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Foods.DietCategory;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.PackageDietNutritionists.V1.Commands.InsertDietNutritionist;
using Payment.Domain.Aggregates.PackageDietNutritionistAggregate;
using Payment.Domain.Aggregates.PackageDietO2FitAggregate;

namespace Payment.Application.PackageDietO2Fits.V1.Commands.InsertDietO2Fit;

public class InsertDietO2FitCommandHandler : IRequestHandler<InsertDietO2FitCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _client;
    private readonly IRequestClient<GetDietCategoryById> _dietCategoryClient;

    public InsertDietO2FitCommandHandler(IUnitOfWork uow, IRequestClient<GetCurrencyByCode> client,
        IRequestClient<GetDietCategoryById> dietCategoryClient)
    {
        _uow = uow;
        _client = client;
        _dietCategoryClient = dietCategoryClient;
    }

    public async Task<string> Handle(InsertDietO2FitCommand request, CancellationToken cancellationToken)
    {
        var currency = await _client.GetResponse<GetCurrencyByCodeResult>(new GetCurrencyByCode
        {
            CurrencyCode = request.CurrencyCode
        }, cancellationToken);
        if (currency.Message.Id == null)
            throw new NotFoundException($"CurrencyName Not Valid Code:{request.CurrencyCode}");

        var package = new DietO2Fit
        {
            CurrencyCode = currency.Message.CurrencyCode,
            IsActive = request.IsActive,
            PackageType = PackageType.NutritionistDietPack,
            Price = request.Price,
            TranslationDescription = new TranslationDietO2Fit
            {
                Arabic = request.TranslationDescription.Arabic,
                English = request.TranslationDescription.English,
                Persian = request.TranslationDescription.Persian
            },
            TranslationName = new TranslationDietO2Fit
            {
                Arabic = request.TranslationName.Arabic,
                English = request.TranslationName.English,
                Persian = request.TranslationName.Persian
            },
            Duration = request.DurationPackage.GetDuration(),
            Sort = request.Sort,
            IsPromote = request.IsPromote
        };
        await _uow.GenericRepository<DietO2Fit>().InsertOneAsync(package, null, cancellationToken);
        return package.Id;
    }
}