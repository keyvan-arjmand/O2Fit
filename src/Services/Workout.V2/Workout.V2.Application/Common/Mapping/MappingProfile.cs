namespace Workout.V2.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BodyMuscle, BodyMuscleDto>().ReverseMap();
        CreateMap<BodyMuscleTranslation, CreateBodyMuscleTranslationDto>().ReverseMap();
        CreateMap<BodyMuscleTranslation, BodyMuscleTranslationDto>().ReverseMap();

        CreateMap<HowToDoTranslation, HowToDoTranslationDto>().ReverseMap();
        CreateMap<RecommendationTranslation, RecommendationTranslationDto>().ReverseMap();
        CreateMap<WorkoutAttributeTranslation, WorkoutAttributeTranslationDto>().ReverseMap();
        CreateMap<WorkoutAttributeValueNameTranslation, WorkoutAttributeValueNameTranslationDto>().ReverseMap();
        CreateMap<WorkoutNameTranslation, WorkoutNameTranslationDto>().ReverseMap();
        CreateMap<WorkoutAttribute, WorkoutAttributeDto>().ReverseMap();
        
        CreateMap<HowToDoTranslation, CreateTranslationDto>().ReverseMap();
        CreateMap<RecommendationTranslation, CreateTranslationDto>().ReverseMap();
        CreateMap<WorkoutAttributeTranslation, CreateTranslationDto>().ReverseMap();
        CreateMap<WorkoutAttributeValueNameTranslation, CreateTranslationDto>().ReverseMap();
        CreateMap<WorkoutNameTranslation, CreateTranslationDto>().ReverseMap();
        CreateMap<HowToDoTranslation, CreateHowToDoTranslationDto>().ReverseMap();
        CreateMap<RecommendationTranslation, CreateRecommendationTranslationDto>().ReverseMap();
        
        CreateMap<Domain.Aggregates.WorkoutsAggregate.Workout, ExerciseDto>()
            .ForMember(x => x.ImageUrl, o => o.MapFrom((src, dest) =>
            {
                if (!string.IsNullOrEmpty(src.ImageName))
                {
                    return dest.ImageUrl = PathConstants.WorkoutImagePath + $"/{src.ImageName}";
                }

                return dest.ImageUrl;
            }))
            .ForMember(x => x.IconUrl, o => o.MapFrom((src, dest) =>
            {
                if (!string.IsNullOrEmpty(src.IconName))
                {
                    return dest.IconUrl = PathConstants.WorkoutIconPath + $"/{src.IconName}";
                }

                return dest.IconUrl;
            }))
            .ReverseMap();

        CreateMap<Domain.Aggregates.WorkoutsAggregate.Workout, CreateWorkoutAttributeValueDto>().ReverseMap();
        CreateMap<WorkoutAttributeValue, CreateWorkoutAttributeValueDto>().ReverseMap();

        
        
        CreateMap<Domain.Aggregates.WorkoutsAggregate.Workout, CardioDto>()
            .ForMember(x => x.ImageUrl, o => o.MapFrom((src, dest) =>
            {
                if (!string.IsNullOrEmpty(src.ImageName))
                {
                    return dest.ImageUrl = PathConstants.WorkoutImagePath + $"/{src.ImageName}";
                }

                return dest.ImageUrl;
            }))
            .ForMember(x => x.IconUrl, o => o.MapFrom((src, dest) =>
            {
                if (!string.IsNullOrEmpty(src.IconName))
                {
                    return dest.IconUrl = PathConstants.WorkoutIconPath + $"/{src.IconName}";
                }

                return dest.IconUrl;
            }))
            .ForMember(x => x.VideoUrl, o => o.MapFrom((src, dest) =>
            {
                if (!string.IsNullOrEmpty(src.VideoName))
                {
                    return dest.VideoUrl = PathConstants.WorkoutVideoPath + $"/{src.VideoName}";
                }

                return dest.VideoUrl;
            })).ReverseMap();

        
        CreateMap<Domain.Aggregates.WorkoutsAggregate.Workout, BodyBuildingDto>()
            .ForMember(x => x.ImageUrl, o => o.MapFrom((src, dest) =>
            {
                if (!string.IsNullOrEmpty(src.ImageName))
                {
                    return dest.ImageUrl = PathConstants.WorkoutImagePath + $"/{src.ImageName}";
                }

                return dest.ImageUrl;
            }))
            .ForMember(x => x.IconUrl, o => o.MapFrom((src, dest) =>
            {
                if (!string.IsNullOrEmpty(src.IconName))
                {
                    return dest.IconUrl = PathConstants.WorkoutIconPath + $"/{src.IconName}";
                }

                return dest.IconUrl;
            }))
            .ForMember(x => x.VideoUrl, o => o.MapFrom((src, dest) =>
            {
                if (!string.IsNullOrEmpty(src.VideoName))
                {
                    return dest.VideoUrl = PathConstants.WorkoutVideoPath + $"/{src.VideoName}";
                }

                return dest.VideoUrl;
            })).ReverseMap();

    }
}