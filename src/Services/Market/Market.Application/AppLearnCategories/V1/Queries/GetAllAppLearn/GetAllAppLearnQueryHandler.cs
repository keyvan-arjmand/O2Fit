using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.AppLearnCategoryAggregate;

namespace Market.Application.AppLearnCategories.V1.Queries.GetAllAppLearn;

public class GetAllAppLearnQueryHandler : IRequestHandler<GetAllAppLearnQuery, List<AppLearnCategoryDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllAppLearnQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<AppLearnCategoryDto>> Handle(GetAllAppLearnQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<AppLearnCategory>()
            .GetAllAsync(cancellationToken);
        return result.ToDto<AppLearnCategoryDto>().ToList();
    }
}