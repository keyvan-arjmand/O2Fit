using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Dtos;
using Payment.Domain.Enums;

namespace Payment.Application.PackageAdmin.V1.Query.PackageAdminById;

public class GetPackageAdminByIdQueryHandler : IRequestHandler<GetPackageAdminByIdQuery, PackageDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<CalculateAmountPackageById> _client;
    private readonly IMapper _mapper;

    public GetPackageAdminByIdQueryHandler(IRequestClient<CalculateAmountPackageById> client, IUnitOfWork uow, IMapper mapper)
    {
        _client = client;
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<PackageDto> Handle(GetPackageAdminByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _uow.GenericRepository<Package>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException("package Not Found");

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