using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Foods.DietCategory;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Domain.Aggregates.PackageDietNutritionistAggregate;

namespace Payment.Application.PackageDietNutritionists.V1.Commands.UpdateDietNutritionist;

public class UpdateDietNutritionistCommandHandler : IRequestHandler<UpdateDietNutritionistCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _client;
    private readonly IRequestClient<GetCurrencyByCode> _dietCategoryClient;

    public UpdateDietNutritionistCommandHandler(IUnitOfWork uow, IRequestClient<GetCurrencyByCode> client,
        IRequestClient<GetCurrencyByCode> dietCategoryClient)
    {
        _uow = uow;
        _client = client;
        _dietCategoryClient = dietCategoryClient;
    }

    public async Task Handle(UpdateDietNutritionistCommand request, CancellationToken cancellationToken)
    {
        var currency = await _client.GetResponse<GetCurrencyByCodeResult>(new GetCurrencyByCode
        {
            CurrencyCode = request.CurrencyCode
        }, cancellationToken);
        if (string.IsNullOrEmpty(currency.Message.Id)) throw new NotFoundException("CurrencyName Not Valid");
        var diet = await _dietCategoryClient.GetResponse<GetDietCategoryByIdResult>(new GetDietCategoryById
        {
            Id = request.DietCategoryId.ToString()!
        }, cancellationToken);
        if (string.IsNullOrEmpty(diet.Message.Id)) throw new NotFoundException("dietId Not Valid");

        var package = await _uow.GenericRepository<DietNutritionist>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (package == null) throw new NotFoundException("Package Not Found");

        package.IsActive = request.IsActive;
        package.CurrencyCode = currency.Message.CurrencyCode;
        package.Price = request.Price;
        package.DietCategoryId = diet.Message.Id.StringToObjectId();
        package.TranslationDescription.English = request.TranslationDescription.Persian;
        package.TranslationDescription.Arabic = request.TranslationDescription.Arabic;
        package.TranslationDescription.Persian = request.TranslationDescription.English;
        package.TranslationName.Persian = request.TranslationName.Persian;
        package.TranslationName.Arabic = request.TranslationName.Arabic;
        package.TranslationName.English = request.TranslationName.English;
        package.DietCategoryName.Arabic = diet.Message.DietCategoryNameArabic;
        package.DietCategoryName.Persian = diet.Message.DietCategoryNamePersian;
        package.DietCategoryName.English = diet.Message.DietCategoryNameEnglish;

        await _uow.GenericRepository<DietNutritionist>()
            .UpdateOneAsync(x => x.Id == request.Id, package, new Expression<Func<DietNutritionist, object>>[]
            {
                x => x.IsActive,
                x => x.CurrencyCode,
                x => x.Price,
                x => x.TranslationName.English,
                x => x.TranslationName.Persian,
                x => x.TranslationName.Arabic,
                x => x.TranslationDescription.English,
                x => x.TranslationDescription.Persian,
                x => x.TranslationDescription.Arabic,
                x => x.DietCategoryName.Arabic,
                x => x.DietCategoryName.Persian,
                x => x.DietCategoryName.English,
                x => x.DietCategoryId,
            }, null, cancellationToken).ConfigureAwait(false);
    }
}