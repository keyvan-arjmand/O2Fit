using Common.Constants.Food;
using Food.V2.Application.Dtos.Brand;
using Food.V2.Domain.Aggregates.BrandAggregate;

namespace Food.V2.Application.Brands.V1.Queries.SearchByName;

public class SearchBrandByNameQueryHandler : IRequestHandler<SearchBrandByNameQuery, List<BrandDto>>
{
    private readonly IUnitOfWork _work;

    public SearchBrandByNameQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<BrandDto>> Handle(SearchBrandByNameQuery request, CancellationToken cancellationToken)
    {
        FilterDefinition<Brand> filter =
            Builders<Brand>.Filter.Regex(Keys.TransactionKey + request.Language, new BsonRegularExpression(request.Name));
        var brands = await _work.GenericRepository<Brand>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        if (brands.Count <= 0) throw new NotFoundException("brand Not Found");
        return brands.MapTo<List<BrandDto>, List<Brand>>();
    }
}