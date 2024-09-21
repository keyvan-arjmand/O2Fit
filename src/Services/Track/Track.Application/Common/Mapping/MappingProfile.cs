using Common.Constants.Track;
using EventBus.Messages.Contracts.Services.Foods.Food;
using EventBus.Messages.Contracts.Services.Track.Specification;
using MongoDB.Bson;
using Track.Application.Dtos;
using Track.Domain.Aggregates.FavoriteFoodAggregate;
using Track.Domain.Aggregates.FavoriteMealAggregate;
using Track.Domain.Aggregates.FavoriteRecipeAggregate;
using Track.Domain.Aggregates.TrackFoodAggregate;
using Track.Domain.Aggregates.TrackSpecificationAggregate;
using Track.Domain.Aggregates.TrackWaterAggregate;

namespace Track.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TrackSpecification, UserTrackSpecificationDto>()
            .ForMember(x => x.UserId, y => y.MapFrom(x => x.CreatedById))
            .ReverseMap();
        CreateMap<FavoriteFood, FavoriteFoodDto>()
            .ForMember(x => x.UserId, y => y.MapFrom(x => x.CreatedById))
            .ReverseMap();
        CreateMap<FavoriteFoodGetDto, FavoriteFoodDto>()
            // .AfterMap((d, s) =>
            // {
            //     if (!string.IsNullOrEmpty(s.Lang))
            //     {
            //         switch (s.Lang)
            //         {
            //             case "Persian":
            //                 d.Name = s.Name.Persian;
            //                 break;
            //             case "English":
            //                 d.Name = s.Name.English;
            //                 break;
            //             case "Arabic":
            //                 d.Name = s.Name.Arabic;
            //                 break;
            //             default:
            //                 d.Name = s.Name.Persian;
            //                 break;
            //         }
            //     }
            // })
            .ReverseMap();
        CreateMap<Domain.Aggregates.TrackStepAggregate.TrackStep, UserStepsDto>()
            // .ForMember(x => x.BurnedCalories, y => y.MapFrom(x => x.BurnedCalories.GetNotNegativeForDecimalType()))
            .ReverseMap();
        CreateMap<UserTrackSpecificationDto, UserTrackSpecificationResult>()
            .ReverseMap();
        CreateMap<UserTrackFoodDto, TrackFood>()
            // .ForMember(x=>x.Value.GetNotNegativeForDecimalType(),y=>y.MapFrom(x=>x.Value))
            .ForMember(x => x.CreatedById, y => y.MapFrom(x => x.UserId))
            .ReverseMap();
        CreateMap<FoodMealEntity, FoodMealDto>()
            // .ForMember(x=>x.NutrientValue,y=>y.MapFrom(x=>x.NutrientValue.GetValueNotNegativeForDecimalType()))
            .ForMember(x => x.Value, y => y.MapFrom(x => x.Value.GetNotNegativeForDecimalType()))
            .AfterMap((s, d) =>
            {
                s.MeasureUnitId = !string.IsNullOrWhiteSpace(d.MeasureUnitId)
                    ? ObjectId.Parse(d.MeasureUnitId)
                    : ObjectId.Empty;
                s.PersonalFoodId = !string.IsNullOrWhiteSpace(d.PersonalFoodId)
                    ? ObjectId.Parse(d.PersonalFoodId)
                    : ObjectId.Empty;
                s.FoodId = !string.IsNullOrWhiteSpace(d.FoodId)
                    ? ObjectId.Parse(d.FoodId)
                    : ObjectId.Empty;
            })
            .ReverseMap();
        CreateMap<FoodMealEntity, GetFoodResult>().ReverseMap();
        // CreateMap<FoodMealEntity, FoodMessage>()
        //     // .ForMember(x=>x.Value,y=>y.MapFrom(x=>x.Value.GetNotNegativeForDecimalType()))
        //     // .ForMember(x=>x.NutrientValue,y=>y.MapFrom(x=>x.NutrientValue.GetValueNotNegativeForDecimalType()))
        //     .AfterMap((s, d) => s.Id = ObjectId.GenerateNewId().ToString()!)
        //     .ReverseMap();
        CreateMap<FavoriteMeal, TrackMealDto>()
            .ForMember(x => x.ImageUrl, y => y.MapFrom(x => "FavoriteMealImage/" + x.ImageName))
            // .ForMember(x=>x.CalorieValue,y=>y.MapFrom(x=>x.CalorieValue.GetNotNegativeForDecimalType()))
            // .ForMember(x=>x.MeasureUnitValue,y=>y.MapFrom(x=>x.MeasureUnitValue.GetNotNegativeForDecimalType()))
            .AfterMap((s, d) =>
            {
                if (s.Foods.Count > 0)
                {
                    int n = 0;
                    s.Foods.ForEach(x =>
                    {
                        n++;
                        if (n != s.Foods.Count)
                        {
                            d.FoodMealName += x.Name + " _ ";
                        }
                        else
                        {
                            d.FoodMealName += x.Name;
                        }
                    });
                }
            })
            .ReverseMap();
        CreateMap<FavoriteRecipe, TrackRecipeDto>().ReverseMap();
        CreateMap<TrackWater, UserTrackWaterDto>()
            .ForMember(x => x.UserId, y => y.MapFrom(x => x.CreatedById))
            // .ForMember(x=>x.Value,y=>y.MapFrom(x=>x.Value.GetNotNegativeForDecimalType()))
            .ReverseMap();
    }
}