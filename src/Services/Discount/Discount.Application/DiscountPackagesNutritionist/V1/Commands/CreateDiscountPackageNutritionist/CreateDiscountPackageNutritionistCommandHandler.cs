using Common.Enums.TypeEnums;
using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountNutritionistAggregate;
using Discount.Domain.Aggregates.DiscountPackageNutritionistAggregate;
using Discount.Domain.Aggregates.DiscountPackageO2FitAggregate;
using Discount.Domain.ValueObjects;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Payments.Package;
using MassTransit;

namespace Discount.Application.DiscountPackagesNutritionist.V1.Commands.CreateDiscountPackageNutritionist;

public class CreateDiscountPackageNutritionistCommandHandler : IRequestHandler<CreateDiscountPackageNutritionistCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetByIdPackage> _client;
    private readonly IRequestClient<GetCurrencyByCode> _clientCurrency;
    public CreateDiscountPackageNutritionistCommandHandler(IUnitOfWork uow, IRequestClient<GetByIdPackage> client, IRequestClient<GetCurrencyByCode> clientCurrency)
    {
        _uow = uow;
        _client = client;
        _clientCurrency = clientCurrency;
    }

    public async Task<string> Handle(CreateDiscountPackageNutritionistCommand request, CancellationToken cancellationToken)
    {
        var validDiscount = await _uow.GenericRepository<DiscountPackageNutritionist>()
            .AnyAsync(x => x.Id == request.PackageId, cancellationToken);
        if (validDiscount) throw new AppException(ApiResultStatusCode.BadRequest, "Discount Package Nutritionist Already Exist");

        var package = await _client.GetResponse<GetByIdPackageResult>(new GetByIdPackage
        {
            Id = request.PackageId
        }, cancellationToken);
        if (string.IsNullOrEmpty(package.Message.Id)) throw new NotFoundException("package Not Found");
     
        var discountPackage = new DiscountPackageNutritionist
        {
            PackageId =package.Message.Id.StringToObjectId(),
            Percent = request.Percent,
        };
        await _uow.GenericRepository<DiscountPackageNutritionist>()
            .InsertOneAsync(discountPackage, null, cancellationToken);
        return discountPackage.Id;
    }
}