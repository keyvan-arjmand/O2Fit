namespace Food.V2.Application.DietPacks.V1.Queries.GetUserPackage;

public class GetUserPackageQueryHandler : IRequestHandler<GetUserPackageQuery,List<GetUserPackageDto>>
{
    private readonly IUnitOfWork _uow;

    public GetUserPackageQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<GetUserPackageDto>> Handle(GetUserPackageQuery request, CancellationToken cancellationToken)
    {
        var calorieResult = 0;
        switch (string.Compare(request.DailyCalorie.ToString().Substring(2, 2), "50"))
        {
            case 0:
                calorieResult = request.DailyCalorie;
                break;
            case -1:
                calorieResult = int.Parse(request.DailyCalorie.ToString()
                    .Replace(request.DailyCalorie.ToString().Substring(2, 2), "00"));
                break;
            case 1:
                calorieResult = int.Parse(request.DailyCalorie.ToString()
                    .Replace(request.DailyCalorie.ToString().Substring(2, 2), "50"));
                break;
            default:
                break;
        }

        var allergyIds = new List<ObjectId>();

        if (!string.IsNullOrEmpty(request.AllergyIds))
        {
            var allergies = request.AllergyIds.Split(',');
            allergyIds = allergies.Select(ObjectId.Parse).ToList();
        }

        var builder = Builders<DietPack>.Filter;
        var filter = builder.Where(x => x.DietCategoryIds.Contains(ObjectId.Parse(request.DietCategoryId)));
        filter &= builder.Eq(x => x.DailyCalorie, new NonNegativeForIntegerTypes(calorieResult));

        if (allergyIds.Count > 0)
            filter &= builder.Where(x => !allergyIds.Any(c => x.IngredientAllergy.Contains(c)));

        var dietPacks = await _uow.GenericRepository<DietPack>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);

        return dietPacks.ToDto<GetUserPackageDto>().ToList();
    }
}