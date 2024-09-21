using Food.V2.Application.Dtos.Brand;
using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Application.Dtos.FoodCategory;
using Food.V2.Application.Dtos.MissingFood;
using Food.V2.Application.Dtos.Nationality;
using Food.V2.Application.Dtos.PersonalFood;
using Food.V2.Application.Dtos.ProblemFood;
using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.BrandAggregate;
using Food.V2.Domain.Aggregates.CategoryAggregate;
using Food.V2.Domain.Aggregates.DietCategoryAggregate;
using Food.V2.Domain.Aggregates.FaultFoodAggregate;
using Food.V2.Domain.Aggregates.MissingFoodAggregate;
using Food.V2.Domain.Aggregates.NationalityAggregate;
using Food.V2.Domain.Aggregates.PersonalFoodAggregate;
using Food.V2.Domain.Aggregates.RecipeAggregate;
using IngredientDto = Food.V2.Application.Dtos.Ingredients.IngredientDto;

namespace Food.V2.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Translation
        CreateMap<NationalityTranslation, TranslationDto>()
            .ReverseMap();
        CreateMap<FoodTranslation, IngredientTranslation>()
            .ReverseMap();
        CreateMap<FoodTranslation, MeasureUnitTranslation>()
            .ReverseMap();
        CreateMap<BrandTranslation, TranslationDto>()
            .ReverseMap();
        CreateMap<FoodTranslation, TranslationDto>()
            .ReverseMap();
        CreateMap<FaultFoodTranslation, TranslationDto>()
            .ReverseMap();
        CreateMap<CategoryTranslation, TranslationDto>()
            .ReverseMap();
        CreateMap<DietCategoryTranslation, TranslationDto>()
            .ReverseMap();
        CreateMap<RecipeTranslation, TranslationDto>()
            .ReverseMap();
        CreateMap<PersonalFoodTranslation, IngredientTranslation>()
            .ReverseMap();
        CreateMap<PersonalFoodTranslation, TranslationDto>()
            .ReverseMap();
        CreateMap<PersonalFoodTranslation, MeasureUnitTranslation>()
            .ReverseMap();

        CreateMap<NationalityTranslation, TranslationDto>().ReverseMap();
        CreateMap<CategoryTranslation, TranslationDto>().ReverseMap();
        CreateMap<DietCategoryTranslation, TranslationDto>().ReverseMap();
        CreateMap<FoodTranslation, CreateFoodTranslationDto>().ReverseMap();
        CreateMap<FoodTranslation, FoodTranslationDto>().ReverseMap();


        CreateMap<MeasureUnit, MeasureUnitDto>().ForMember(x=>x.Name, o=>o.MapFrom(x=>x.Translation)).ReverseMap();
        CreateMap<PersonalFoodIngredient, PersonalIngredientDto>()
            .ForMember(x => x.Value, y => y.MapFrom(x => x.IngredientValue.Value))
            .ForMember(x => x.Name, y => y.MapFrom(x => x.IngredientTranslation))
            .ForMember(x => x.Id, y => y.MapFrom(x => x.IngredientId))
            .ForMember(x => x.MeasureUnitList, y => y.MapFrom(x => x.IngredientMeasureUnits.Select(x => x.Id).ToList()))
            .ReverseMap();
        CreateMap<MeasureUnitTranslation, MeasureUnitTranslationDto>().ReverseMap();
        CreateMap<MeasureUnitTranslation, CreateUpdateMeasureUnitTranslationDto>().ReverseMap();
        CreateMap<IngredientTranslation, CreateUpdateIngredientTranslationDto>().ReverseMap();
        CreateMap<Ingredient, GetByIdAdminIngredientDto>().ReverseMap();
        CreateMap<IngredientTranslation, IngredientTranslationDto>().ReverseMap();
        CreateMap<Ingredient, GetIngredientByIdDto>().ForMember(x=>x.Name, o=>o.MapFrom(x=>x.Translation)).ReverseMap();
        CreateMap<Ingredient, SearchIngredientByNameDto>().ReverseMap();
        CreateMap<Ingredient, IngredientDto>().ReverseMap();
        CreateMap<RecipeCategoryTranslation, CreateUpdateRecipeCategoryTranslationDto>().ReverseMap();
        CreateMap<RecipeCategoryTranslation, RecipeCategoryTranslationDto>().ReverseMap();
        CreateMap<RecipeCategory, RecipeCategoryDto>()
            .ForMember(x => x.ImageUri, o => o.MapFrom(u => u.ImageName)).ReverseMap();

        CreateMap<DietCategoryChildren, DietCategoryChildrenDto>().ReverseMap();
        CreateMap<BrandDto, Brand>().ReverseMap();
        CreateMap<MissingFoodDto, MissingFood>()
            .ForMember(x => x.Created, y => y.MapFrom(x => x.Created))
            .ForMember(x => x.CreatedById, y => y.MapFrom(x => x.UserId))
            .ReverseMap();
        CreateMap<FaultFoodDto, FaultFood>()
            .ForMember(x => x.Created, y => y.MapFrom(x => x.Created))
            .ForMember(x => x.CreatedById, y => y.MapFrom(x => x.UserId))
            .ReverseMap();
        CreateMap<FullRecipeDto, Domain.Aggregates.FoodAggregate.Food>()
            .ForMember(x => x.Id, y => y.MapFrom(x => x.FoodId))
            .ForMember(x => x.Name, y => y.MapFrom(x => x.FoodName))
            .ReverseMap();
        CreateMap<RecipeDto, Domain.Aggregates.FoodAggregate.Food>()
            .ForMember(x => x.Id, y => y.MapFrom(x => x.FoodId))
            .ReverseMap();

        CreateMap<RecipeDto, Domain.Aggregates.FoodAggregate.Food>()
            .ForMember(x => x.Id, y => y.MapFrom(x => x.FoodId))
            .ReverseMap();
        CreateMap<ListRecipeStepDto, Domain.Aggregates.FoodAggregate.Food>()
            .ForMember(x => x.Id, y => y.MapFrom(x => x.FoodId))
            .ReverseMap();
        CreateMap<ListRecipeTipDto, Domain.Aggregates.FoodAggregate.Food>()
            .ForMember(x => x.Id, y => y.MapFrom(x => x.FoodId))
            .ReverseMap();
        CreateMap<Domain.Aggregates.FoodAggregate.Food, RecipeTipDto>()
            .ForMember(x => x.FoodId, y => y.MapFrom(x => x.Id))
            .ReverseMap();
        CreateMap<DietCategory, DietCategoryDto>()
            .ForMember(x => x.ParentId, y => y.MapFrom(x => x.ParentId.ObjectIdToString()))
            .ReverseMap();
        CreateMap<Category, FoodCategoryDto>()
            .ForMember(x => x.ParentId, y => y.MapFrom(x => x.ParentId.ObjectIdToString()))
            .ReverseMap();
        CreateMap<Nationality, NationalityDto>()
            .ForMember(x => x.ParentId, y => y.MapFrom(x => x.ParentId.ObjectIdToString()))
            .ReverseMap();
        CreateMap<DietPackTranslation, CreateDietPackTranslationDto>().ReverseMap();
        CreateMap<DietPackFood, CreateDietPackFoodDto>().ReverseMap();
        CreateMap<FilterFoodDto, Domain.Aggregates.FoodAggregate.Food>().ReverseMap();
        CreateMap<FoodIngredient, FoodIngredientDto>()
            .ForMember(x => x.MeasureUnits, y => y.MapFrom(x => x.IngredientMeasureUnits))
            .ForMember(x => x.Name, y => y.MapFrom(x => x.IngredientTranslation))
            .ForMember(x => x.MeasureUnitName, y => y.MapFrom(x => x.MeasureUnitTranslation))
            .ForMember(x => x.Value, y => y.MapFrom(x => x.IngredientValue.Value))
            .ForMember(x => x.MeasureUnitValue, y => y.MapFrom(x => x.MeasureUnitValue.Value))
            .ReverseMap();
        CreateMap<FoodWithDetailDto, SearchFoodNameDto>()
            .ForMember(x => x.BrandName, y => y.MapFrom(x => x.Brand.FirstOrDefault()!.Translation))
            .ForMember(x => x.FoodId, y => y.MapFrom(x => x.Id))
            .AfterMap((s, d) =>
            {
                if (!string.IsNullOrWhiteSpace(s.Lang))
                {
                    switch (s.Lang)
                    {
                        case "Persian":
                            d.Name = s.Name.Persian;
                            break;
                        case "English":
                            d.Name = s.Name.English;
                            break;
                        case "Arabic":
                            d.Name = s.Name.Arabic;
                            break;
                        default:
                            d.Name = s.Name.Persian;
                            break;
                    }
                }
            })
            .ReverseMap();

        CreateMap<DietPack, DietPackDto>()
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Translation.Persian)).ReverseMap();
        CreateMap<DietPackFood, DietPackFoodDto>().ReverseMap();
        CreateMap<FoodWithDetailDto, GetUserFoodDto>()
            .ForMember(x => x.Brand, y => y.MapFrom(x => x.Brand.FirstOrDefault()!.Translation))
            .ForMember(x => x.FoodId, y => y.MapFrom(x => x.Id))
            .ForMember(x => x.BrandId, y => y.MapFrom(x => x.BrandId))
            .ForMember(x => x.NationalityIds, y => y.MapFrom(x => x.Nationalities.Select(x => x.Id).ToList()))
            .ForMember(x => x.DefaultMeasureUnitId, y => y.MapFrom(x => x.DefaultMeasureUnit.FirstOrDefault()!.Id))
            .ForMember(x => x.Ingredients, y => y.MapFrom(x => x.FoodIngredients))
            .ReverseMap();
        CreateMap<DietPack, GetAllDietPackDto>()
            .ForMember(x => x.PackageName, o => o.MapFrom(x => x.Translation.Persian)).ReverseMap();
        CreateMap<DietPack, GetUserPackageDto>()
            .ForMember(x => x.Name, o => o.MapFrom(des => des.Translation
                .Persian)).ReverseMap();
        CreateMap<DietPackFood, GetUserPackageDietPackFoodDto>().ReverseMap();
        CreateMap<PersonalFood, PersonalFoodDto>()
            .ForMember(x => x.PersonalFoodId, y => y.MapFrom(x => x.Id))
            .ForMember(x => x.FoodName, y => y.MapFrom(x => x.Name))
            .ForMember(x => x.ImageUri,
                y => y.MapFrom(x =>
                    string.IsNullOrWhiteSpace(x.ImageName) ? string.Empty : "PersonalFoodImage/" + x.ImageName))
            .ForMember(x => x.Ingredients, y => y.MapFrom(x => x.PersonalFoodIngredients))
            .ForMember(x => x.NutrientValue, y => y.MapFrom(x => x.NutrientValue))
            .ForMember(x => x.Ingredients, y => y.MapFrom(x => x.PersonalFoodIngredients))
            .AfterMap((s, d) =>
            {
                foreach (var i in s.NutrientValue)
                {
                    d.NutrientValue.Add(i.Value);
                }
            })
            .ReverseMap();
        //CreateMap<GetUserPackageDto, GetUserPackageAggregationResultDto>()
        //    .ForMember(x => x.Translation.Persian, o => o.MapFrom(s => s.Name));
        //.ForMember(x=>x.DietPackFoods.)
        //     .ForMember(dest => dest.MeasureUnits, 
        //         opt => opt.MapFrom((src, dest, destMember, context) =>
        //         {
        //             (decimal)context.Items["MeasureUnitValue"] == src.DietPackFoods.
        //         }
        // opt => opt.Condition(src=> (src.DietPackFoods.Where(x=>x.MeasureUnitId == opt.))))
        // .ForMember(x => x.PackageName, o => o.MapFrom(x => x.Translation.Persian)).ReverseMap();
    }
}