namespace Food.V2.Application.DietPacks.V1.Commands.CreateDietPack;

public class CreateDietPackCommandHandler : IRequestHandler<CreateDietPackCommand, string>
{
    private readonly IUnitOfWork _uow;

    public CreateDietPackCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<string> Handle(CreateDietPackCommand request, CancellationToken cancellationToken)
    {
        var dietPack = new DietPack
        {
            FoodMeal = request.FoodMeal,
            CalorieValue = new NonNegativeForIntegerTypes(request.CalorieValue),
            DailyCalorie = new NonNegativeForIntegerTypes(request.DailyCalorie),
            IsActive = request.IsActive,
            NationalityIds = request.NationalityIds.Select(ObjectId.Parse).ToList(),
            NutrientValues =
                new List<NotNegativeForDecimalTypes>(
                    request.NutrientValues.Select(s => new NotNegativeForDecimalTypes(s))),
            Translation = request.Name.ToEntity<DietPackTranslation>(),
            SpecialDiseases = request.SpecialDiseases,
            IngredientAllergy = request.IngredientAllergies.Select(ObjectId.Parse).ToList(),
            DietCategoryIds = request.DietCategoryIds.Select(ObjectId.Parse).ToList(),
            DietPackFoods = request.DietPackFoods.ToEntity<DietPackFood>().ToList(),
            ParentCategoryId = ObjectId.Parse(request.ParentCategory),
            
        };

        await _uow.GenericRepository<DietPack>().InsertOneAsync(dietPack, null, cancellationToken);
        return dietPack.Id;
    }
}