using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Mapping;
using Payment.Application.Dtos;
using Payment.Domain.Aggregates.PackageAdvertiseAggregate;
using Payment.Domain.Aggregates.TransactionDietNutritionistPackageAggregate;

namespace Payment.Application.PackageAdvertises.V1.Commands.InsertPackageAdvertise;

public class InsertPackageAdvertiseCommandHandler : IRequestHandler<InsertPackageAdvertiseCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IRequestClient<GetCurrencyByCode> _client;

    public InsertPackageAdvertiseCommandHandler(IUnitOfWork work, IRequestClient<GetCurrencyByCode> client)
    {
        _work = work;
        _client = client;
    }

    public async Task Handle(InsertPackageAdvertiseCommand request, CancellationToken cancellationToken)
    {
        if (request.Price.ModNumber(request.Cost) != 0)
            throw new NotFoundException("The value of Price is not divisible by Cost");
        var currency = await _client.GetResponse<GetCurrencyByCodeResult>(new GetCurrencyByCode
        {
            CurrencyCode = request.CurrencyCode
        }, cancellationToken);
        if (currency.Message.Id == null)
            throw new NotFoundException($"CurrencyName Not Valid Code:{request.CurrencyCode}");
        var package = new Advertise
        {
            CurrencyCode = currency.Message.CurrencyCode,
            Price = request.Price,
            TranslationDescription = request.TranslationDescription!.MapTo<TransactionDietNutritionist, TranslationDto>(),
            TranslationTitle = request.TranslationTitle.MapTo<TransactionDietNutritionist, TranslationDto>(),
            PackageType = PackageType.Advertise,
            IsActive = request.IsActive,
            Cost = request.Cost,
            AdvertisePackageType = request.AdvertisePackageType
        };
        package.ClickCount = request.Price.CountModNumber(request.Cost);
        await _work.GenericRepository<Advertise>().InsertOneAsync(package, null, cancellationToken);
    }
}