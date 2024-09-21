namespace Food.V2.Application.Foods.V1.Queries.GetFoodNameById;

public class GetFoodNameByIdQueryHandler : IRequestHandler<GetFoodNameByIdQuery, FoodTranslationDto>
{
    private readonly IUnitOfWork _uow;

    public GetFoodNameByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<FoodTranslationDto> Handle(GetFoodNameByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _uow.FoodRepository().GetFoodTranslationById(request.Id, cancellationToken);
        if (data == null)
            throw new NotFoundException(nameof(Domain.Aggregates.FoodAggregate.Food), request.Id);

        return data.ToDto<FoodTranslationDto>();
    }
}