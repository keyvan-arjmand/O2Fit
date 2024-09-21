using EventBus.Messages.Contracts.Services.Wallet;
using Wallet.Application.Dtos;
using Wallet.Domain.Aggregates.TransactionCompanyAggregate;

namespace Wallet.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TransactionCompany, TransactionCompDto>().ReverseMap();
        CreateMap<GetTransactionCompanyByIdResult, TransactionCompDto>().ReverseMap();
        CreateMap<Domain.Aggregates.WalletAggregate.Wallet, WalletDto>().ReverseMap();
    }
}