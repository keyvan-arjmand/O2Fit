using Discount.Application.Dtos;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;
using Discount.Domain.Aggregates.DiscountPackageO2FitAggregate;

namespace Discount.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<Domain.Aggregates.DiscountAggregate.Discount, DiscountDto>()
        //     .ForMember(x => x.Id, y => y.MapFrom(x => x.Id.ToString()))
        //     .ForMember(x => x.UserId, y => y.MapFrom(x => x.UserId.ToString()))
        //     .ForMember(x => x.CurrencyId, y => y.MapFrom(x => x.CurrencyId.ToString()))
        //     .ForMember(x => x.Code, y => y.MapFrom(x => x.Code.Code))
        //     // .ForMember(x => x.Amount, y => y.MapFrom(x => x.Amount.Value))
        //     // .ForMember(x => x.Percent, y => y.MapFrom(x => x.Percent))
        //     .ReverseMap();
        CreateMap<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit, DiscountPackageDto>()
            .ForMember(x => x.Id, y => y.MapFrom(x => x.Id.ToString()))
            .ForMember(x => x.PackageId, y => y.MapFrom(x => x.PackageId.ToString()))
            // .ForMember(x=>x.Percent,y=>y.MapFrom(x=>x.Percent.Value))
            .ReverseMap();
        CreateMap<DiscountO2Fit, DiscountO2FitDto>().ReverseMap();
    }
}