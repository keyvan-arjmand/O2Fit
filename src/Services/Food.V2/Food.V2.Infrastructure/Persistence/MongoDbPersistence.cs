using System.Xml.Linq;
using Food.V2.Application.Common.Mapping;
using Food.V2.Application.Dtos.Brand;
using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Application.Dtos.DietPack;
using Food.V2.Application.Dtos.Food;
using Food.V2.Application.Dtos.FoodCategory;
using Food.V2.Application.Dtos.Ingredients;
using Food.V2.Application.Dtos.MeasureUnits;
using Food.V2.Application.Dtos.MeasureUnits;
using Food.V2.Application.Dtos.Nationality;
using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.RecipeCategory;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.RecipeAggregate;
using Food.V2.Domain.Serializers;
using Google.Protobuf.Collections;

namespace Food.V2.Infrastructure.Persistence;

public static class MongoDbPersistence
{
    public static void Configure()
    {
        //ProductMap.Configure();
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
        // BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));

        //BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

        //BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

        //var stringBsonSerializer = BsonSerializer.SerializerRegistry.GetSerializer<string>();
        //BsonSerializer.RegisterSerializer(new TodoListName(stringBsonSerializer));
        // Conventions

        BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            //  BsonSerializer.RegisterSerializer(typeof(decimal), new NotNegativeForDecimalTypesSerializer());
        });
        BsonClassMap.RegisterClassMap<BaseDto>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
        });
        //BsonClassMap.RegisterClassMap<BaseAuditableDto>(cm =>
        //{
        //    cm.AutoMap();
        //    cm.MapProperty(x => x.LastModifiedById)
        //        .SetSerializer(new StringSerializer(BsonType.ObjectId));
        //    cm.MapProperty(x => x.CreatedById)
        //        .SetSerializer(new StringSerializer(BsonType.ObjectId));
        //});

        BsonClassMap.RegisterClassMap<TranslationDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(c => c.Arabic).SetElementName("arabic");
            cm.MapMember(c => c.English).SetElementName("english");
            cm.MapMember(c => c.Persian).SetElementName("persian");

            //cm.MapProperty(c => c.English);
            //cm.MapProperty(c => c.Persian);
        });
        BsonClassMap.RegisterClassMap<RecipeCategoryTranslationDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(c => c.Arabic).SetElementName("arabic");
            cm.MapMember(c => c.English).SetElementName("english");
            cm.MapMember(c => c.Persian).SetElementName("persian");

            //cm.MapProperty(c => c.English);
            //cm.MapProperty(c => c.Persian);
        });
        BsonClassMap.RegisterClassMap<IngredientTranslationDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(c => c.Arabic).SetElementName("arabic");
            cm.MapMember(c => c.English).SetElementName("english");
            cm.MapMember(c => c.Persian).SetElementName("persian");

            //cm.MapProperty(c => c.English);
            //cm.MapProperty(c => c.Persian);
        });
        BsonClassMap.RegisterClassMap<MeasureUnitTranslationDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(c => c.Arabic).SetElementName("arabic");
            cm.MapMember(c => c.English).SetElementName("english");
            cm.MapMember(c => c.Persian).SetElementName("persian");

            //cm.MapProperty(c => c.English);
            //cm.MapProperty(c => c.Persian);
        });
        BsonClassMap.RegisterClassMap<RecipeDto>(cm =>
        {
            
            cm.MapIdProperty(c => c.FoodId)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(c => c.RecipeSteps).SetElementName("recipeSteps");
            cm.MapMember(c => c.RecipeTips).SetElementName("recipeTips");
        });
        BsonClassMap.RegisterClassMap<NationalityDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(c => c.Translation).SetElementName("translation");
            cm.MapMember(c => c.ParentTranslation).SetElementName("parentTranslation");
        });
        BsonClassMap.RegisterClassMap<RecipeCategoryDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapProperty(c => c.Translation).SetElementName("translation");
            cm.MapProperty(c => c.ImageUri).SetElementName("imageUri");
        });
        BsonClassMap.RegisterClassMap<IngredientDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapProperty(c => c.Translation).SetElementName("translation");
            cm.MapProperty(c => c.Code).SetElementName("code");
            cm.MapProperty(c => c.TagEn).SetElementName("tagEn");
            cm.MapProperty(c => c.TagAr).SetElementName("tagAr");
            cm.MapProperty(c => c.Tag).SetElementName("tag");
            cm.MapProperty(c => c.ThumbName).SetElementName("thumbName");
        });
        BsonClassMap.RegisterClassMap<MeasureUnitDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapProperty(c => c.Name).SetElementName("translation");
            cm.MapProperty(c => c.Value).SetElementName("value");
        });
        BsonClassMap.RegisterClassMap<MeasureUnitsDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapProperty(c => c.Name).SetElementName("translation");
            cm.MapProperty(c => c.Value).SetElementName("value");
        });
        BsonClassMap.RegisterClassMap<FoodCategoryDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapProperty(c => c.Translation)
                .SetElementName("translation");
            cm.MapProperty(c => c.ParentId)
                .SetElementName("parentId")
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapProperty(c => c.Percent).SetElementName("percent");
            cm.MapProperty(c => c.ParentTranslation).SetElementName("parentTranslation");
        });
        BsonClassMap.RegisterClassMap<FoodIngredientDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapIdProperty(c => c.IngredientId)
                .SetElementName("ingredientId")
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapIdProperty(c => c.MeasureUnits)
                .SetElementName("ingredientMeasureUnits");
                // .SetIdGenerator(StringObjectIdGenerator.Instance)
                // .SetSerializer(new StringSerializer(BsonType.ObjectId));
            // .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            // .SetIdGenerator(StringObjectIdGenerator.Instance);
            cm.MapIdProperty(c => c.MeasureUnitId)
                .SetElementName("measureUnitId")
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapProperty(c => c.Name).SetElementName("ingredientTranslation");
            cm.MapProperty(c => c.Value).SetElementName("ingredientValue");
            cm.MapProperty(c => c.MeasureUnitName).SetElementName("measureUnitTranslation");
            cm.MapProperty(c => c.MeasureUnitValue).SetElementName("measureUnitValue");
        });
        BsonClassMap.RegisterClassMap<DietCategoryDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapProperty(c => c.Name).SetElementName("name");
            cm.MapProperty(c => c.ParentName).SetElementName("prentName");
            cm.MapProperty(c => c.Description).SetElementName("description");
            cm.MapProperty(c => c.ParentDescription).SetElementName("parentDescription");
            cm.MapProperty(c => c.ImageName).SetElementName("imageName");
            cm.MapProperty(c => c.BannerImageName).SetElementName("bannerImageName");
            cm.MapProperty(c => c.IsActive).SetElementName("isActive");
            cm.MapProperty(c => c.IsPromote).SetElementName("isPromote");
            cm.MapProperty(c => c.ParentId).SetElementName("parentId")
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
        });
        BsonClassMap.RegisterClassMap<BrandDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapProperty(c => c.Translation).SetElementName("translation");
            cm.MapProperty(c => c.LogoUri).SetElementName("logoUri");
            cm.MapProperty(c => c.Address).SetElementName("address");
        });
        BsonClassMap.RegisterClassMap<FoodWithDetailDto>(cm =>
        {
            // cm.MapProperty(x => x.RecipeDto)
            //     .SetSerializer(new ArraySerializer<List<RecipeDto>>());
            //  cm.MapProperty(x => x.RecipeCategories)
            //      .SetSerializer(new StringSerializer(BsonType.ObjectId));
            //      .SetSerializer(new ArraySerializer<RecipeCategoryDto>());
            //  cm.MapProperty(c => c.RecipeCategories).SetElementName("RecipeCategories");
            cm.AutoMap();
            cm.MapProperty(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapProperty(x => x.BrandId)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            // cm.MapProperty(x => x.DefaultMeasureUnitId)
            //     .SetSerializer(new StringSerializer(BsonType.ObjectId));
            // cm.MapProperty(x => x.RecipeCategoryId)
            //     .SetSerializer(new StringSerializer(BsonType.ObjectId));
            // cm.MapProperty(x => x.IngredientIds)
            //     .SetSerializer(new StringSerializer(BsonType.ObjectId));
            // cm.MapProperty(x => x.MeasureUnitIds)
            //     .SetSerializer(new StringSerializer(BsonType.ObjectId));
            // cm.MapProperty(x => x.NationalityIds)
            //     .SetSerializer(new StringSerializer(BsonType.ObjectId));
            // cm.MapProperty(x => x.FoodCategoryIds)
            //     .SetSerializer(new StringSerializer(BsonType.ObjectId));
            // cm.MapProperty(x => x.DietCategoryIds)
            //     .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapProperty(c => c.BrandId).SetElementName("brandId");
            cm.MapProperty(c => c.Name).SetElementName("name");
            cm.MapProperty(c => c.FoodCode).SetElementName("foodCode");
            cm.MapProperty(c => c.BakingType).SetElementName("bakingType");
            cm.MapProperty(c => c.BakingTime).SetElementName("bakingTime");
            cm.MapProperty(c => c.BarcodeGs1).SetElementName("barcodeGs1");
            cm.MapProperty(c => c.BarcodeNational).SetElementName("barcodeNational");
            cm.MapProperty(c => c.ImageUri).SetElementName("imageUri");
            cm.MapProperty(c => c.ImageThumb).SetElementName("imageThumb");
            cm.MapProperty(c => c.WeightBeforeBaking).SetElementName("weightBeforeBaking");
            cm.MapProperty(c => c.WeightAfterBaking).SetElementName("weightAfterBaking");
            cm.MapProperty(c => c.EvaporatedWater).SetElementName("evaporatedWater");
            cm.MapProperty(c => c.DryIngredient).SetElementName("dryIngredient");
            cm.MapProperty(c => c.FoodType).SetElementName("foodType");
            cm.MapProperty(c => c.IsActive).SetElementName("isActive");
            cm.MapProperty(c => c.Tag).SetElementName("tag");
            cm.MapProperty(c => c.TagArEn).SetElementName("tagArEn");
            cm.MapProperty(c => c.NutrientValue).SetElementName("nutrientValue");
            cm.MapProperty(c => c.PersonCount).SetElementName("personCount");
            cm.MapProperty(c => c.FoodMeals).SetElementName("foodMeals");
            cm.MapProperty(c => c.FoodHabits).SetElementName("foodHabits");
            cm.MapProperty(c => c.SpecialDiseases).SetElementName("specialDiseases");
            cm.MapProperty(c => c.Gl).SetElementName("gl");
            cm.MapProperty(c => c.Gi).SetElementName("gi");
            cm.MapProperty(c => c.UseInDiet).SetElementName("useInDiet");
            cm.MapProperty(c => c.RecipeStatus).SetElementName("recipeStatus");
            cm.MapMember(c => c.RecipeSteps).SetElementName("recipeSteps");
            cm.MapMember(c => c.FoodIngredients).SetElementName("foodIngredients");
        });

        #region DietPack

        BsonClassMap.RegisterClassMap<DietPackTranslationDto>(cm =>
        {
            //cm.MapIdProperty(c => c.Id)
            //    .SetIdGenerator(StringObjectIdGenerator.Instance)
            //    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(c => c.Arabic).SetElementName("arabic");
            cm.MapMember(c => c.English).SetElementName("english");
            cm.MapMember(c => c.Persian).SetElementName("persian");
        });


        BsonClassMap.RegisterClassMap<GetUserPackageAggregationResultDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(c => c.CalorieValue).SetElementName("calorieValue");
            cm.MapMember(c => c.DailyCalorie).SetElementName("dailyCalorie");
            cm.MapMember(c => c.NutrientValues).SetElementName("nutrientValues");
            cm.MapMember(c => c.FoodMeal).SetElementName("foodMeal");
            cm.MapMember(c => c.Translation).SetElementName("translation");
            cm.MapMember(c => c.MeasureUnits).SetElementName("measureUnits");
            cm.MapMember(c => c.Foods).SetElementName("foods");
            cm.MapMember(c => c.DietPackFoods).SetElementName("dietPackFoods");
        });

        BsonClassMap.RegisterClassMap<FoodTranslationDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(c => c.Arabic).SetElementName("arabic");
            cm.MapMember(c => c.English).SetElementName("english");
            cm.MapMember(c => c.Persian).SetElementName("persian");
        });
        BsonClassMap.RegisterClassMap<GetFoodForDietPackAggregationResultDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(c => c.Name).SetElementName("name");
            // cm.MapMember(c => c.ChildCategoryId)
            //     .SetSerializer(new StringSerializer(BsonType.ObjectId)).SetElementName("childCategoryId");
            // cm.MapMember(c => c.MeasureUnitId)
            //     .SetSerializer(new StringSerializer(BsonType.ObjectId)).SetElementName("measureUnitId");
        });


        BsonClassMap.RegisterClassMap<DietPackFoodForAggregationDto>(cm =>
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(c => c.Value).SetElementName("value");
            cm.MapMember(c => c.ChildCategoryId).SetElementName("childCategoryId")
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
        });

        #endregion


        var pack = new ConventionPack
        {
            //new IgnoreExtraElementsConvention(true),
            //new IgnoreIfDefaultConvention(true),
            new CamelCaseElementNameConvention(),
            //new ImmutableTypeClassMapConvention()
        };
        ConventionRegistry.Register("My Solution Conventions", pack, t => true);
    }
}