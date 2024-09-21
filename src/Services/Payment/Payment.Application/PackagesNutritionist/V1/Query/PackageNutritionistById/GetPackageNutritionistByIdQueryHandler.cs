using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Dtos;

namespace Payment.Application.PackagesNutritionist.V1.Query.PackageNutritionistById;

public class GetPackageNutritionistByIdQueryHandler : IRequestHandler<GetPackageNutritionistByIdQuery, PackageDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<CalculateAmountPackageById> _client;
    private readonly IMapper _mapper;

    public GetPackageNutritionistByIdQueryHandler(IRequestClient<CalculateAmountPackageById> client, IUnitOfWork uow, IMapper mapper)
    {
        _client = client;
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<PackageDto> Handle(GetPackageNutritionistByIdQuery request, CancellationToken cancellationToken)
    {

        var result = await _uow.GenericRepository<Package>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (result == null) throw new NotFoundException("package Not Found");
        if (result.PackageType != PackageType.NutritionistPack.ToDescription()) throw new NotFoundException("package Not exist in DietNutritionist");

        var discountPackage = await _client.GetResponse<CalculateAmountPackageByIdResult>(new CalculateAmountPackageById()
        {
            Id = result.Id,
            Price = result.Price
        }, cancellationToken);

        var packDto = _mapper.Map<Package, PackageDto>(result);
        packDto.DiscountPrice = discountPackage.Message.DiscountPrice;
        packDto.Wage = discountPackage.Message.Wage;
        return packDto;
    }
}