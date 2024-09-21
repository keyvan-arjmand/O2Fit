namespace Food.V2.Application.DietPacks.V1.Queries.IsDietPackExits;

public class IsDietPackExitsQueryHandler : IRequestHandler<IsDietPackExitsQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public IsDietPackExitsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public  Task<bool> Handle(IsDietPackExitsQuery request, CancellationToken cancellationToken)
    {
        return _uow.GenericRepository<DietPack>().AnyAsync(x => x.CalorieValue == request.CalorieValue
                                                                             && x.Translation.Persian ==
                                                                             request.Persian
                                                                             && x.DailyCalorie == request.DailyCalorie
                                                                             && x.FoodMeal == request.FoodMeal
                                                                             && x.DietCategoryIds.Contains(ObjectId.Parse(request.DietCategoryId))
                                                                            && x.NutrientValues.SequenceEqual(request.NutritionValue.Select(s=>new NotNegativeForDecimalTypes(s)))
                                                                                , cancellationToken);
    }
}