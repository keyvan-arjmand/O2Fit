using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Track.Specification;
using Identity.V2.Application.Dtos.UserTrackSpecification;
using Permission = Identity.V2.Domain.Aggregates.PermissionCategoryAggregate.Permission;

namespace Identity.V2.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Role, RoleDto>();
        CreateMap<CategoryPermission, CategoryPermissionPaginatedDto>();
        CreateMap<CategoryPermission, CategoryPermissionWithPermissionsDto>();
        CreateMap<Permission, PermissionsDto>();
        CreateMap<CategoryPermission, CategoryPermissionWithPermissionsForTreeViewDto>();
        CreateMap<Permission, PermissionsForTreeViewDto>();
        CreateMap<Country, CountryDto>();
        CreateMap<City, CityDto>();
        CreateMap<Translation, CountryTranslationDto>();
        CreateMap<UserProfile, ProfileDto>();
        CreateMap<User, UserInfoDto>();
        CreateMap<Target, TargetDto>();
        CreateMap<UserTrackSpecificationResult, UserTrackSpecificationDto>().ReverseMap();
        CreateMap<UserForUserInfoDto, UserInfoDto>()
.ForMember(x=>x.CountryId, opt=>opt.Ignore()).ReverseMap();
        CreateMap<ProfileForUserInfoDto, ProfileDto>().ReverseMap();
        CreateMap<CountryDto, GetCountryByCurrencyCodeResult>();
        CreateMap<Translation, CreateTranslationDto>().ReverseMap();
        CreateMap<SpecialDiseaseTranslation, CreateSpecialDiseasesTranslationDto>().ReverseMap();
        CreateMap<SpecialDiseaseTranslation, SpecialDiseasesTranslationDto>().ReverseMap();
        CreateMap<SpecialDisease, SpecialDiseaseDto>().ReverseMap();
        CreateMap<Coordinates, CoordinatesDto>().ReverseMap();
        CreateMap<NutritionistProfile, NutritionistProfileDto>().ReverseMap();
        
    }
}