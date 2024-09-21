using System.Linq;
using System.Runtime.CompilerServices;
using AutoMapper;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Service.Models;
using FoodStuff.Service.Services;
using FoodStuff.Service.v2.Command.Foods;

namespace WebFramework.CustomMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Translation, TranslationResultDto>().ReverseMap();
            CreateMap<Translation, CreateTranslationDto>().ReverseMap();
            CreateMap<DietPack, DietPackDto>().ForMember(d => d.Name,
                o => o.MapFrom(x => x.Name.Persian)).ReverseMap();
            CreateMap<int, DietPack>();


            CreateMap<DietPackFood, DietPackFoodResponseDto>()
                .ForMember(x => x.NutrientValue,
                    o => o.MapFrom(s => s.NutrientValue))
                .ForMember(x => x.MeasureUnitValue,
                    o => o.MapFrom(s => s.MeasureUnit.Value))
                .ForMember(x => x.MeasureUnitName,
                    o => o.MapFrom(m => m.MeasureUnit.Translation.Persian))
                .ForMember(x => x.FoodName, o => o.MapFrom(f => f.Food.TranslationName.Persian))
                .ReverseMap();

            CreateMap<MeasureUnit, MeasureUnitResultDto>()
                .ForMember(x => x.Name, o => o.MapFrom(t => t.Translation))
                .ForMember(x => x.meassureUnitCategory, o => o.MapFrom(c => c.MeasureUnitCategory));

            CreateMap<RecipeStep, RecipeStepDto>().ReverseMap();

            CreateMap<DietCategory, DietCategoryResultDto>()
                .ForMember(x => x.Image,
                    y => y.MapFrom((src, dest) =>
                    {

                        if (!string.IsNullOrEmpty(src.Image))
                        {
                            dest.Image = "https://foodtest.o2fitt.com/DietCategoryImg/" + src.Image;
                            return dest.Image;
                        }
                        else
                        {
                            return dest.Image;
                        }
                    }))
                .ForMember(x => x.BannerImage,
                    y => y.MapFrom((src, dest) =>
                    {

                        if (!string.IsNullOrEmpty(src.BannerImage))
                        {
                            dest.BannerImage = "https://foodtest.o2fitt.com/DietCategoryImg/" + src.BannerImage;
                            return dest.BannerImage;
                        }
                        else
                        {
                            return dest.BannerImage;
                        }
                    }))
                .ForMember(x => x.Name, o => o.MapFrom(t => t.NameTranslation))
                .ForMember(x => x.Description, o => o.MapFrom(t => t.DescriptionTranslation));


            CreateMap<RecipeCategore, RecipeCategoryDto>()
                .ForMember(x => x.Translation, o => o.MapFrom(t => t.NameTranslation))
                .ReverseMap();

            CreateMap<Food, CreateFoodDto>()
                .ForMember(x => x.Recipe, o => o.MapFrom(r => r.Recipe))
                .ForMember(x => x.Name, o => o.MapFrom(t => t.TranslationName))
                .ReverseMap();

            CreateMap<Recipe, CreateRecipeDto>()
                .ForMember(x => x.RecipeSteps, o => o.MapFrom(r => r.RecipeSteps))
                .ForMember(x => x.Tips, o => o.MapFrom(t => t.Tips))
                .ReverseMap();
            // CreateMap<CreateRecipeCategoryDto, RecipeCategoryDto>();

            CreateMap<RecipeStep, CreateRecipeStepDto>()
                .ForMember(x => x.Translation, o => o.MapFrom(t => t.Translation))
                .ReverseMap();

            CreateMap<Tip, CreateTipDto>()
                .ForMember(x => x.Translation, o => o.MapFrom(t => t.Translation))
                .ReverseMap();

            CreateMap<Ingredient, CreateIngredientDto>()
                .ForMember(x => x.Translation, o => o.MapFrom(t => t.Translation))
                .ReverseMap();

            CreateMap<FoodIngredient, CreateFoodIngredientDto>().ReverseMap();

            CreateMap<Food, FoodDto>()
                .ForMember(x => x.TranslationName, o => o.MapFrom(t => t.TranslationName))
                .ForMember(x => x.TranslationRecipe, o => o.MapFrom(t => t.TranslationRecipe))
                .ReverseMap();

            CreateMap<UpdateFoodCommand, Food>();

            CreateMap<Ingredient, IngredientDto>()
                .ForMember(x => x.Name, o => o.MapFrom(t => t.Translation));

            CreateMap<Category, CategoryDto>()
                .ForMember(x => x.NameTranslation, o => o.MapFrom(t => t.NameTranslation));

            CreateMap<Nationality, NationalityDto>()
                .ForMember(x => x.NameTranslation, o => o.MapFrom(t => t.NameTranslation));

            CreateMap<Food, FoodWithDetailsDto>()
                .ForMember(x => x.Name, o => o.MapFrom(n => n.TranslationName))
                .ForMember(x => x.OldRecipe, o => o.MapFrom(r => r.TranslationRecipe))
                .ForMember(x => x.Brand, o => o.MapFrom(b => b.Brand.Translation))
                .ForMember(x => x.Recipe, o => o.MapFrom(r => r.Recipe))
                //.ForMember(x => x.Ingredients,
                //  o => o.MapFrom(i => i.FoodIngredients.Select(s => s.Ingredient).ToList()))
                .ForMember(x => x.DietCategories,
                    o => o.MapFrom(d => d.FoodDietCategories.Select(s => s.DietCategory).ToList()))
                .ForMember(x => x.Categories, o => o.MapFrom(f => f.FoodCategories.Select(s => s.Category).ToList()))
                .ForMember(x => x.Nationalities,
                    o => o.MapFrom(n => n.FoodNationalities.Select(s => s.Nationality).ToList()));
            //.ForMember(x=>x.Ingredients.Select(s=>s.Value), o=>o.MapFrom(i=>i.FoodIngredients.Select(s=>s.MeasureUnitId)))
            //.ForMember(x=>x.Ingredients.Select(s=>s.Value), o=>o.MapFrom(m=>m.FoodMeasureUnits.Select(s=>s.MeasureUnit.Value)));


            CreateMap<Recipe, RecipeDto>();

            CreateMap<RecipeStep, RecipeStepDto>()
                .ForMember(x => x.Translation, o => o.MapFrom(t => t.Translation));

            CreateMap<Tip, TipDto>()
                .ForMember(x => x.Translation, o => o.MapFrom(t => t.Translation));

            CreateMap<Brand, BrandDto>()
                .ForMember(x => x.Translation, o => o.MapFrom(t => t.Translation));

            CreateMap<Ingredient, IngredientAdminDto>()
                .ForMember(x => x.Name, o => o.MapFrom(t => t.Translation));

            CreateMap<Category, CategoryAdminDto>()
                .ForMember(x => x.NameTranslation, o => o.MapFrom(t => t.NameTranslation));

            CreateMap<DietCategory, DietCategoryAdminDto>()
                .ForMember(x => x.NameTranslation, o => o.MapFrom(x => x.NameTranslation))
                .ForMember(x => x.DescriptionTranslation, o => o.MapFrom(x => x.DescriptionTranslation));

            CreateMap<Nationality, NationalityAdminDto>()
                .ForMember(x => x.NameTranslation, o => o.MapFrom(t => t.NameTranslation));

            CreateMap<Food, FoodWithDetailForAdminDto>()
                .ForMember(x => x.Name, o => o.MapFrom(t => t.TranslationName))
                .ForMember(x => x.OldRecipe, o => o.MapFrom(t => t.TranslationRecipe))
                .ForMember(x => x.Recipe, o => o.MapFrom(r => r.Recipe))
                //.ForMember(x => x.Ingredients,
                //  o => o.MapFrom(i => i.FoodIngredients.Select(s => s.Ingredient).ToList()))
                .ForMember(x => x.DietCategories,
                    o => o.MapFrom(d => d.FoodDietCategories.Select(s => s.DietCategory).ToList()))
                .ForMember(x => x.Categories, o => o.MapFrom(f => f.FoodCategories.Select(s => s.Category).ToList()))
                .ForMember(x => x.Nationalities,
                    o => o.MapFrom(n => n.FoodNationalities.Select(s => s.Nationality).ToList()))
                .ForMember(x => x.FoodMealIds, o => o.MapFrom(x => x.FoodMeals));


            CreateMap<IngredientAllergy, IngredientAllergyDto>()
                .ForMember(x => x.Translation, o => o.MapFrom(t => t.Ingredient.Translation))
                .ForMember(x => x.IngredientId, o => o.MapFrom(i => i.IngredientId))
                .ForMember(x => x.Code, o => o.MapFrom(c => c.Ingredient.Code));
            CreateMap<Recipe, RecipeGetAllDto>().ForMember(x => x.FoodName, y => y.MapFrom(x => x.Food.TranslationName))
                .ForMember(x => x.RecipeCategoryId, y => y.MapFrom(x => x.Food.RecipeCategoryId)).ReverseMap();
            CreateMap<Recipe, GetFullRecipeById>().ReverseMap();
            CreateMap<Food, FoodRecipeDto>().ReverseMap();
            CreateMap<Food, GetFoodNotRecipeDto>().ReverseMap();
            CreateMap<Ingredient, IngredientSearchAdminResultDto>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Translation.Persian))
                .ReverseMap();
            
            CreateMap<Food, GetFullRecipeById>()
                  .ForMember(x => x.Food, r => r.MapFrom(x => x))
                  .ForMember(x => x.Id, y => y.MapFrom(x => x.Recipe.Id))
                  .ForMember(x => x.RecipeSteps, y => y.MapFrom(x => x.Recipe.RecipeSteps))
                  .ForMember(x => x.Tips, y => y.MapFrom(x => x.Recipe.Tips))
                  .ForMember(x => x.Status, y => y.MapFrom(x => x.Recipe.Status))
                  .ForPath(x => x.Food.Id, o => o.MapFrom(f => f.Id))
                  .ForPath(x => x.Food.TranslationName, o => o.MapFrom(t => t.TranslationName))
                  .ForPath(x => x.Food.RecipeCategoryId, o => o.MapFrom(r => r.RecipeCategoryId))
                  .ReverseMap();

            CreateMap<Note, NoteDto>()
                .ForMember(x=>x._id,o=>o.MapFrom(s=>s.AppId))
                .ReverseMap();
        }
    }
}




