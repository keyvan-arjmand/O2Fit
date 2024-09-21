using Order.V2.Application.Dtos;

namespace Order.V2.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Aggregates.OrderAggregate.Order, OrderDto>()
            .ReverseMap();
    }
}