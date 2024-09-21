using EventBus.Messages.Contracts.Services.Payments.Transaction;
using Payment.Application.Dtos;

namespace Payment.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<Package, PackageDto>()
        //     .ForMember(x => x.UserId, y => y.MapFrom(x => x.CreatedById))
        //     .ReverseMap();
        // CreateMap<TransactionDietPackage, TransactionDto>()
        //     .ReverseMap();
        // CreateMap<TransactionDietPackage, GetTransactionByIdResult>()
        //     .ReverseMap();
        CreateMap<TransactionDto, GetTransactionByIdResult>()
            .ReverseMap();
    }
}