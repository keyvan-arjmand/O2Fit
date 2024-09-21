using Common.Constants.Food;

namespace Food.V2.Application.Foods.V1.Queries.GetFoodByBarcode;

public class GetFoodByBarcodeQueryHandler : IRequestHandler<GetFoodByBarcodeQuery, SearchFoodNameDto>
{
    private readonly IUnitOfWork _work;

    public GetFoodByBarcodeQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<SearchFoodNameDto> Handle(GetFoodByBarcodeQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.FoodAggregate.Food>.Filter.Where(x =>
            x.BarcodeGs1 == request.Barcode || x.BarcodeNational == request.Barcode);
        var foods = await _work.FoodAggregation()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        foods.Lang = request.Lang;
        return foods.MapTo<SearchFoodNameDto, FoodWithDetailDto>();
    }
}