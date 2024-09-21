using System.Xml.Linq;
using Food.V2.Application.Dtos.Food;
using Food.V2.Application.Dtos.Nationality;
using Food.V2.Domain.Aggregates.BrandAggregate;
using Food.V2.Domain.Aggregates.CategoryAggregate;
using Food.V2.Domain.Aggregates.DietCategoryAggregate;
using Food.V2.Domain.Aggregates.MeasureUnitAggregate;
using Food.V2.Domain.Aggregates.NationalityAggregate;
using Food.V2.Domain.Aggregates.RecipeAggregate;
using Food.V2.Domain.Aggregates.RecipeCategoryAggregate;
using Newtonsoft.Json;

namespace Food.V2.Infrastructure.Persistence.Repositories;

public class FoodAggregation : GenericRepository<Domain.Aggregates.FoodAggregate.Food>, IFoodAggregation
{
    private IMongoCollection<Recipe> RecipeCollection { get; }
    private IMongoCollection<Brand> BrandCollection { get; }
    private IMongoCollection<RecipeCategory> RecipeCategoryCollection { get; }
    private IMongoCollection<Ingredient> IngredientCollection { get; }
    private IMongoCollection<MeasureUnit> MeasureUnitCollection { get; }
    private IMongoCollection<Category> FoodCategoryUnitCollection { get; }
    private IMongoCollection<DietCategory> DietCategoryCollection { get; }
    private IMongoCollection<Nationality> NationalityCollection { get; }
    private readonly IMongoClient _mongoClient;

    public FoodAggregation(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService) :
        base(mediator, configuration, currentUserService)
    {
        var settings = MongoClientSettings.FromConnectionString(configuration["MongoSettings:ConnectionString"]);

        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        _mongoClient = new MongoClient(settings);
        RecipeCollection = _mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"])
            .GetCollection<Recipe>(ToLowerFirsWord(nameof(Recipe)) + "s");
        NationalityCollection = _mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"])
            .GetCollection<Nationality>(ToLowerFirsWord(nameof(Nationality)) + "s");
        BrandCollection = _mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"])
            .GetCollection<Brand>(ToLowerFirsWord(nameof(Brand)) + "s");
        RecipeCategoryCollection = _mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"])
            .GetCollection<RecipeCategory>(ToLowerFirsWord(nameof(RecipeCategory)) + "s");
        IngredientCollection = _mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"])
            .GetCollection<Ingredient>(ToLowerFirsWord(nameof(Ingredient)) + "s");
        FoodCategoryUnitCollection = _mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"])
            .GetCollection<Category>(ToLowerFirsWord(nameof(Category)) + "s");
        MeasureUnitCollection = _mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"])
            .GetCollection<MeasureUnit>(ToLowerFirsWord(nameof(MeasureUnit)) + "s");
        DietCategoryCollection = _mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"])
            .GetCollection<DietCategory>(ToLowerFirsWord(nameof(DietCategory)) + "s");
    }

    public async Task<FoodWithDetailDto> GetFoodWithDetailById(string id, CancellationToken cancellationToken)
    {
        #region OldCode

        // //var i = new FoodWithDetailDto();
        // //var foodBson = await Collection.Aggregate()
        // //    .Match(x => x.Id == id)
        // //    .Lookup(foreignCollection: RecipeCollection, localField: x => x.Id, foreignField: x => x.FoodId,
        // //        @as: (FoodWithDetailDto dto) => dto.RecipeResult)
        // //    .Lookup(foreignCollection: NationalityCollection, localField: x => x.NationalityIds,
        // //        foreignField: x => x.Id,
        // //        @as: (FoodWithDetailDto dto) => dto.NationalityResult)
        // //    .Lookup(foreignCollection: BrandCollection, localField: x => x.BrandId,
        // //        foreignField: x => x.Id,
        // //        @as: (FoodWithDetailDto dto) => dto.BrandResult)
        // //    .Lookup(foreignCollection: RecipeCategoryCollection, localField: x => x.RecipeCategoryId,
        // //        foreignField: x => x.Id,
        // //        @as: (FoodWithDetailDto dto) => dto.RecipeCategoryResult)
        // //    .Lookup(foreignCollection: IngredientCollection, localField: x => x.IngredientIds,
        // //        foreignField: x => x.Id,
        // //        @as: (FoodWithDetailDto dto) => dto.IngredientResult)
        // //    .Lookup(foreignCollection: FoodCategoryUnitCollection, localField: x => x.FoodCategoryIds,
        // //        foreignField: x => x.Id,
        // //        @as: (FoodWithDetailDto dto) => dto.FoodCategoryResult)
        // //    .Lookup(foreignCollection: MeasureUnitCollection, localField: x => x.MeasureUnitIds,
        // //        foreignField: x => x.Id,
        // //        @as: (FoodWithDetailDto dto) => dto.MeasureUnitResult)
        // //    .Lookup(foreignCollection: MeasureUnitCollection, localField: x => x.DefaultMeasureUnitId,
        // //        foreignField: x => x.Id,
        // //        @as: (FoodWithDetailDto dto) => dto.DefaultMeasureUnitResult)
        // //    .Lookup(foreignCollection: DietCategoryCollection, localField: x => x.DietCategoryIds,
        // //        foreignField: x => x.Id,
        // //        @as: (FoodWithDetailDto dto) => dto.DietCategoryResult)
        // //    .ToListAsync(cancellationToken);
        // var docs = await Collection.Aggregate()
        //     .Match(x => x.Id == id)
        //     // .Project(Builders<Domain.Aggregates.FoodAggregate.Food>.Projection.Exclude("created")
        //     //     .Exclude("createdBy").Exclude("createdById").Exclude("lastModified").Exclude("lastModifiedBy").Exclude("lastModifiedById"))
        //     .Lookup("recipes", "_id", "foodId", "Recipes")
        //     .Lookup("nationalitys", "nationalityIds", "_id", "Nationalities")
        //     .Lookup("recipeCategorys", "recipeCategoryId", "_id", "RecipeCategories")
        //     //.Lookup("brands","brandId","_id", "BrandResult")
        //     //.Lookup("categorys","foodCategoryIds","_id", "FoodCategoryResult")
        //     //.Lookup("dietCategorys","dietCategoryIds","_id", "DietCategoryResult")
        //     //.Lookup("measureUnits","measureUnitIds","_id", "MeasureUnitResult")
        //     //.Lookup("ingredients","ingredientIds","_id", "IngredientResult")
        //     //.Lookup("measureUnits","defaultMeasureUnitId","_id", "DefaultMeasureUnitResult")
        //     .FirstAsync(cancellationToken);
        //
        // var dto = BsonSerializer.Deserialize<FoodWithDetailDto>(docs);
        //
        // return new();
        //

        #endregion

        var docs = await Collection.Aggregate()
            .Match(x => x.Id == id)
            // .Lookup("recipes", "_id", "foodId", "Recipes")
            .Lookup("nationalitys", "nationalityIds", "_id", "Nationalities")
            .Lookup("recipeCategorys", "recipeCategoryId", "_id", "RecipeCategories")
            .Lookup("brands", "brandId", "_id", "Brand")
            .Lookup("categorys", "foodCategoryIds", "_id", "FoodCategories")
            .Lookup("dietCategorys", "dietCategoryIds", "_id", "DietCategories")
            .Lookup("measureUnits", "measureUnitIds", "_id", "MeasureUnits")
            // .Lookup("foodIngredients", "foodIngredientIds", "_id", "FoodIngredients")
            .Lookup("measureUnits", "defaultMeasureUnitId", "_id", "DefaultMeasureUnit")
            .FirstOrDefaultAsync(cancellationToken);
        return BsonSerializer.Deserialize<FoodWithDetailDto>(docs);
    }

    public async Task<FoodWithDetailDto> GetSingleDocumentByFilterAsync(
        FilterDefinition<Domain.Aggregates.FoodAggregate.Food> filter, CancellationToken cancellationToken = default)
    {
        var docs = await Collection.Aggregate()
            .Match(filter)
            .Lookup("recipes", "_id", "foodId", "Recipes")
            .Lookup("nationalitys", "nationalityIds", "_id", "Nationalities")
            .Lookup("recipeCategorys", "recipeCategoryId", "_id", "RecipeCategories")
            .Lookup("brands", "brandId", "_id", "Brand")
            .Lookup("categorys", "foodCategoryIds", "_id", "FoodCategories")
            .Lookup("dietCategorys", "dietCategoryIds", "_id", "DietCategories")
            .Lookup("measureUnits", "measureUnitIds", "_id", "MeasureUnits")
            .Lookup("foodIngredients", "foodIngredientIds", "_id", "FoodIngredients")

            .Lookup("measureUnits", "defaultMeasureUnitId", "_id", "DefaultMeasureUnit")
            .FirstOrDefaultAsync(cancellationToken);
        return BsonSerializer.Deserialize<FoodWithDetailDto>(docs);
    }

    public async Task<List<FoodWithDetailDto>> GetListOfDocumentsByFilterAsync(
        FilterDefinition<Domain.Aggregates.FoodAggregate.Food> filter, CancellationToken cancellationToken = default)
    {
        var docs = await Collection.Aggregate()
            .Match(filter)
            .Lookup("recipes", "_id", "foodId", "Recipes")
            .Lookup("nationalitys", "nationalityIds", "_id", "Nationalities")
            .Lookup("recipeCategorys", "recipeCategoryId", "_id", "RecipeCategories")
            .Lookup("brands", "brandId", "_id", "Brand")
            .Lookup("categorys", "foodCategoryIds", "_id", "FoodCategories")
            .Lookup("dietCategorys", "dietCategoryIds", "_id", "DietCategories")
            .Lookup("measureUnits", "measureUnitIds", "_id", "MeasureUnits")
            .Lookup("foodIngredients", "foodIngredientIds", "_id", "FoodIngredients")
            .Lookup("measureUnits", "defaultMeasureUnitId", "_id", "DefaultMeasureUnit")
            .ToListAsync(cancellationToken);
        return BsonSerializer.Deserialize<List<FoodWithDetailDto>>(docs.ToJson());
    }

    public async Task<List<FoodWithDetailDto>> GetListPaginationOfDocumentsByFilterAsync(int pageIndex, int pageSize,
        FilterDefinition<Domain.Aggregates.FoodAggregate.Food> filter,
        CancellationToken cancellationToken = default)
    {
        var docs = await Collection.Aggregate()
            .Match(filter)
            .Lookup("recipes", "_id", "foodId", "Recipes")
            .Lookup("nationalitys", "nationalityIds", "_id", "Nationalities")
            .Lookup("recipeCategorys", "recipeCategoryId", "_id", "RecipeCategories")
            .Lookup("brands", "brandId", "_id", "Brand")
            .Lookup("categorys", "foodCategoryIds", "_id", "FoodCategories")
            .Lookup("dietCategorys", "dietCategoryIds", "_id", "DietCategories")
            .Lookup("measureUnits", "measureUnitIds", "_id", "MeasureUnits")
            .Lookup("ingredients", "ingredientIds", "_id", "Ingredients")
            .Lookup("measureUnits", "defaultMeasureUnitId", "_id", "DefaultMeasureUnit")
            .Skip((pageIndex - 1) * pageSize).Limit(pageSize)
            .ToListAsync(cancellationToken);
        return BsonSerializer.Deserialize<List<FoodWithDetailDto>>(docs.ToJson());
    }

    public async Task<List<FoodWithDetailDto>> GetListPaginationOfDocumentsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var docs = await Collection.Aggregate()
            .Lookup("recipes", "_id", "foodId", "Recipes")
            .Lookup("nationalitys", "nationalityIds", "_id", "Nationalities")
            .Lookup("recipeCategorys", "recipeCategoryId", "_id", "RecipeCategories")
            .Lookup("brands", "brandId", "_id", "Brand")
            .Lookup("categorys", "foodCategoryIds", "_id", "FoodCategories")
            .Lookup("dietCategorys", "dietCategoryIds", "_id", "DietCategories")
            .Lookup("measureUnits", "measureUnitIds", "_id", "MeasureUnits")
            .Lookup("ingredients", "ingredientIds", "_id", "Ingredients")
            .Lookup("measureUnits", "defaultMeasureUnitId", "_id", "DefaultMeasureUnit")
            .Skip((pageIndex - 1) * pageSize).Limit(pageSize)
            .ToListAsync(cancellationToken);
        return BsonSerializer.Deserialize<List<FoodWithDetailDto>>(docs.ToJson());
    }


    private string ToLowerFirsWord(string word)
    {
        word = word.Replace(word.Substring(0, 1), word.Substring(0, 1).ToLower());
        return word;
    }
}