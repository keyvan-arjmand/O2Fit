using Currency.Application.Dtos;
using Currency.Domain.ValueObjects;

namespace Currency.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Aggregates.CurrencyAggregate.Currency, CurrencyDto>()
            .ForMember(x=>x.CurrencyCode,y=>y.MapFrom(x=>x.CurrencyCode.Name))
            .ReverseMap();
    }
}