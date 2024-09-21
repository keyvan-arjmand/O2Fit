using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Payments.Package;
using MassTransit;

namespace Discount.Application.DiscountPackageO2Fit.V1.Commands.CreateDiscountPackageO2Fit;

public class CreateDiscountPackageO2FitCommandHandler : IRequestHandler<CreateDiscountPackageO2FitCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetByIdPackage> _client;
    private readonly IRequestClient<GetCurrencyByCode> _clientCurrency;

    public CreateDiscountPackageO2FitCommandHandler(IUnitOfWork uow, IRequestClient<GetByIdPackage> client, IRequestClient<GetCurrencyByCode> clientCurrency)
    {
        _uow = uow;
        _client = client;
        _clientCurrency = clientCurrency;
    }

    public async Task<string> Handle(CreateDiscountPackageO2FitCommand request, CancellationToken cancellationToken)
    {
        var validDiscount = await _uow.GenericRepository<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>()
            .AnyAsync(x => x.Id == request.PackageId, cancellationToken);
        if (validDiscount) throw new AppException(ApiResultStatusCode.BadRequest, "Discount Package Already Exist");

        var package = await _client.GetResponse<GetByIdPackageResult>(new GetByIdPackage
        {
            Id = request.PackageId
        }, cancellationToken);
        if (string.IsNullOrEmpty(package.Message.Id)) throw new NotFoundException("package Not Found");

        var discountPackage = new Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit
        {
            PackageId = package.Message.Id.StringToObjectId(),
            Percent = request.Percent,
            PackageType = request.PackageType
        };
        await _uow.GenericRepository<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>()
            .InsertOneAsync(discountPackage, null, cancellationToken);
        return discountPackage.Id;
    }
}