using AutoMapper;
using Food.Domain.Entities;
using Food.Domain.Enum;

namespace Food.Grpc.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Translation, GrpcTranslaltion>().ReverseMap();
        
        CreateMap<DietPack, GrpcDietPack>()
            .ForMember(x=>x.FoodMeal,
                y=>y.MapFrom(x=>(int)x.FoodMeal)).ReverseMap();
        CreateMap<DietPackFood, GrpcDietPackFood>()
            .ForMember(x => x.FoodId,
                y => y.MapFrom(x => x.FoodId))
            .ForMember(x => x.Value,
                y => y.MapFrom(x => x.Value))
            .ForMember(x => x.NutrientValue,
                y => y.MapFrom(x => x.NutrientValue))
            .ForMember(x => x.Calorie,
                y => y.MapFrom(x => x.Calorie))
            .ForMember(x => x.MeasureUnitValue,
                y => y.MapFrom(x => x.MeasureUnit.Value))
            .ForMember(x => x.MeasureUnitId,
                y => y.MapFrom(x => x.MeasureUnitId))
            .ForMember(x => x.CategoryChildId,
                y => y.MapFrom(x => x.CategoryChildId))
            .ReverseMap();



    }
}