namespace Nutritionist.Application.NutritionistOrders.V1.Queries.GetPendingNutritionistOrdersPaginated;

public class GetPendingNutritionistOrdersPaginatedQueryHandler : IRequestHandler<GetPendingNutritionistOrdersPaginatedQuery, PaginationResult<NutritionistOrderDto>>
{
    private readonly IUnitOfWork _uow;

    public GetPendingNutritionistOrdersPaginatedQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PaginationResult<NutritionistOrderDto>> Handle(GetPendingNutritionistOrdersPaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<NutritionistOrder>.Filter.Eq(x => x.Status, OrderStatus.Pending);
        filter &= Builders<NutritionistOrder>.Filter.Eq(x => x.IsDelete, false);
        var nutritionistOrders = await _uow.GenericRepository<NutritionistOrder>()
            .GetListPaginationOfDocumentsByFilterAsync(request.PageNumber, request.PageSize, filter, cancellationToken);
        
        var result = nutritionistOrders.Data.ToDto<NutritionistOrderDto>().ToList();

        foreach (var nutritionistOrderDto in result)
        {
            switch (request.Language)
            {
                case Language.Persian:
                    nutritionistOrderDto.PackageName = nutritionistOrders.Data
                        .FirstOrDefault(x => x.OrderId == ObjectId.Parse(nutritionistOrderDto.OrderId))!.PackageName
                        .Persian;

                    nutritionistOrderDto.DietCategoryName = nutritionistOrders.Data
                        .FirstOrDefault(x => x.OrderId == ObjectId.Parse(nutritionistOrderDto.OrderId))!
                        .DietCategoryName
                        .Persian;
                    continue;

                case Language.English:
                    nutritionistOrderDto.PackageName = nutritionistOrders.Data
                        .FirstOrDefault(x => x.OrderId == ObjectId.Parse(nutritionistOrderDto.OrderId))!.PackageName
                        .English;

                    nutritionistOrderDto.DietCategoryName = nutritionistOrders.Data
                        .FirstOrDefault(x => x.OrderId == ObjectId.Parse(nutritionistOrderDto.OrderId))!
                        .DietCategoryName
                        .English;
                    continue;

                case Language.Arabic:
                    
                    nutritionistOrderDto.PackageName = nutritionistOrders.Data
                        .FirstOrDefault(x => x.OrderId == ObjectId.Parse(nutritionistOrderDto.OrderId))!.PackageName
                        .Arabic;
                    
                    nutritionistOrderDto.DietCategoryName = nutritionistOrders.Data
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