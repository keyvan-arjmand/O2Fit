using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Foods.DietCategory;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Domain.Aggregates.PackageDietNutritionistAggregate;
using Payment.Domain.Aggregates.PackageDietO2FitAggregate;

namespace Payment.Application.PackageDietO2Fits.V1.Commands.UpdateDietO2Fit;

public class UpdatePackageO2FitCommandHandler : IRequestHandler<UpdatePackageO2FitCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _client;
    private readonly IRequestClient<GetCurrencyByCode> _dietCategoryClient;

    public UpdatePackageO2FitCommandHandler(IUnitOfWork uow, IRequestClient<GetCurrencyByCode> client,
        IRequestClient<GetCurrencyByCode> dietCategoryClient)
    {
        _uow = uow;
        _client = client;
        _dietCategoryClient = dietCategoryClient;
    }

    public async Task Handle(UpdatePackageO2FitCommand request, CancellationToken cancellationToken)
    {
        var currency = await _client.GetResponse<GetCurrencyByCodeResult>(new GetCurrencyByCode
        {
            CurrencyCode = request.CurrencyCode
        }, cancellationToken);
        if (string.IsNullOrEmpty(currency.Message.Id)) throw new NotFoundException("CurrencyName Not Valid");

        var package = await _uow.GenericRepository<DietO2Fit>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (package == null) throw new NotFoundException("Package Not Found");

        package.IsActive = request.IsActive;
        package.CurrencyCode = currency.Message.CurrencyCode;
        package.Price = request.Price;
        package.TranslationDescription.English = request.TranslationDescription.Persian;
        package.TranslationDescription.Arabic = request.TranslationDescription.Arabic;
        package.TranslationDescription.Persian = request.TranslationDescription.English;
        package.TranslationName.Persian = request.TranslationName.Persian;
        package.TranslationName.Arabic = request.TranslationName.Arabic;
        package.TranslationName.English = request.TranslationName.English;
        package.Duration = request.DurationPackage.GetDuration();

        await _uow.GenericRepository<DietO2Fit>()
            .UpdateOneAsync(x => x.Id == request.Id, package, new Expression<Func<DietO2Fit, object>>[]
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
                x => x.Duration,
            }, null, cancellationToken).ConfigureAwait(false);
    }
}