namespace Food.V2.Application.DietPacks.V1.Commands.UpdateDietPack;

public class UpdateDietPackCommandHandler : IRequestHandler<UpdateDietPackCommand>
{
    private readonly IUnitOfWork _uow;

    public UpdateDietPackCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(UpdateDietPackCommand request, CancellationToken cancellationToken)
    {
        var dietPack = await _uow.GenericRepository<DietPack>().GetByIdAsync(request.Id, cancellationToken);
        if (dietPack == null)
            throw new NotFoundException(nameof(DietPack), request.Id);

        dietPack.DietPackFoods = request.DietPackFoods.ToEntity<DietPackFood>().ToList();
        dietPack.DietCategoryIds = request.DietCategoryIds.Select(ObjectId.Parse).ToList();
        dietPack.CalorieValue = new NonNegativeForIntegerTypes(request.CalorieValue);
        dietPack.FoodMeal = request.FoodMeal;
        dietPack.IngredientAllergy = request.IngredientAllergies.Select(ObjectId.Parse).ToList();
        dietPack.DailyCalorie = new NonNegativeForIntegerTypes(request.DailyCalorie);
        dietPack.IsActive = request.IsActive;
        dietPack.NationalityIds = request.NationalityIds.Select(ObjectId.Parse).ToList();
        dietPack.NutrientValues = request.NutrientValue.Select(s => new NotNegativeForDecimalTypes(s)).ToList();
        dietPack.Translation = request.Name.ToEntity<DietPackTranslation>();
        dietPack.ParentCategoryId = ObjectId.Parse(request.ParentCategory);
        dietPack.SpecialDiseases = request.SpecialDiseases;

        await _uow.GenericRepository<DietPack>().UpdateOneAsync(x => x.Id == request.Id, dietPack,
            new Expression<Func<DietPack, object>>[]
            {
                x => x.ParentCategoryId,
                x => x.CalorieValue,
                x => x.DailyCalorie,
                x => x.NationalityIds,
                x => x.NutrientValues,
                x => x.FoodMeal,
                x => x.IngredientAllergy,
                x => x.IsActive,
                x => x.Translation,
                x => x.SpecialDiseases,
                x => x.DietCategoryIds,
                x => x.DietPackFoods
            }, null, cancellationToken);

    }
}