namespace Food.V2.Application.DietPacks.V1.Queries.GetAllDietPackPaginated;

public class
    GetAllDietPackPaginatedQueryHandler : IRequestHandler<GetAllDietPackPaginatedQuery,
        PaginationResult<GetAllDietPackDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllDietPackPaginatedQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<GetAllDietPackDto>> Handle(GetAllDietPackPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<DietPack>.Filter.AnyEq(x => x.DietCategoryIds, ObjectId.Parse(request.DietCategoryId));

        if (request.FoodMeal != null)
            filter &= Builders<DietPack>.Filter.Eq(x => x.FoodMeal, request.FoodMeal);

        if (request.DailyCalorie > 0)
            filter &= Builders<DietPack>.Filter.Eq(x => x.DailyCalorie, request.DailyCalorie);

        if (!string.IsNullOrEmpty(request.DietPackName))
            filter &= Builders<DietPack>.Filter.Eq(x => x.Translation.Persian, request.DietPackName);

        var result = await _uow.GenericRepository<DietPack>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);

        var dtoResult = result.ToDto<GetAllDietPackDto>().ToList();
        
        return
            PaginationResult<GetAllDietPackDto>.CreatePaginationResult(request.PageIndex, request.PageSize,
                result.Count, dtoResult);
    }
}