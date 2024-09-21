using Food.V2.Application.Dtos.PersonalFood;
using Food.V2.Domain.Aggregates.PersonalFoodAggregate;

namespace Food.V2.Application.PersonalFoods.V1.Queries.GetPersonalFoodByIds;

public class GetPersonalFoodByIdsQueryHandler : IRequestHandler<GetPersonalFoodByIdsQuery, List<PersonalFoodDto>>
{
    private readonly IUnitOfWork _work;

    public GetPersonalFoodByIdsQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<PersonalFoodDto>> Handle(GetPersonalFoodByIdsQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<PersonalFood>.Filter.Where(x => request.PersonalFoodIds.Contains(x.Id));
        var result = await _work.GenericRepository<PersonalFood>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        
        return result.ToDto<PersonalFoodDto>().ToList();
    }
}