namespace Nutritionist.Application.NutritionistOrders.V1.Queries.GetNutritionistOrdersPaginated;

public class
    GetNutritionistOrdersPaginatedQueryHandler : IRequestHandler<GetNutritionistOrdersPaginatedQuery, PaginationResult<NutritionistOrderDto>>
{
    private readonly IUnitOfWork _uow;

    public GetNutritionistOrdersPaginatedQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<NutritionistOrderDto>> Handle(GetNutritionistOrdersPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var data = await _uow.GenericRepository<NutritionistOrder>()
            .GetAllPaginationAsync(request.PageNumber, request.PageSize, cancellationToken);
        var result = data.Data.ToDto<NutritionistOrderDto>().ToList();
        foreach (var nutritionistOrderDto in result)
        {
            switch (request.Language)
            {
                case Language.Persian:
                    nutritionistOrderDto.PackageName = data.Data
                        .FirstOrDefault(x => x.OrderId == ObjectId.Parse(nutritionistOrderDto.OrderId))!.PackageName
                        .Persian;

                    nutritionistOrderDto.DietCategoryName = data.Data
                        .FirstOrDefault(x => x.OrderId == ObjectId.Parse(nutritionistOrderDto.OrderId))!
                        .DietCategoryName
                        .Persian;
                    continue;

                case Language.English:
                    nutritionistOrderDto.PackageName = data.Data
                        .FirstOrDefault(x => x.OrderId == ObjectId.Parse(nutritionistOrderDto.OrderId))!.PackageName
                        .English;

                    nutritionistOrderDto.DietCategoryName = data.Data
                        .FirstOrDefault(x => x.OrderId == ObjectId.Parse(nutritionistOrderDto.OrderId))!
                        .DietCategoryName
                        .English;
                    continue;

                case Language.Arabic:
                    
                    nutritionistOrderDto.PackageName = data.Data
                        .FirstOrDefault(x => x.OrderId == ObjectId.Parse(nutritionistOrderDto.OrderId))!.PackageName
                        .Arabic;
                    
                    nutritionistOrderDto.DietCategoryName = data.Data
                        .FirstOrDefault(x => x.OrderId == ObjectId.Parse(nutritionistOrderDto.OrderId))!
                        .DietCategoryName
                        .Arabic;
                    continue;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return PaginationResult<NutritionistOrderDto>.CreatePaginationResult(request.PageNumber, request.PageSize, result.Count, result);
    }
}