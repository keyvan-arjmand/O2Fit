using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.BrandAggregate;

namespace Food.V2.Application.Brands.V1.Commands.InsertBrand;

public class InsertBrandCommandHandler : IRequestHandler<InsertBrandCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IFileService _fileService;

    public InsertBrandCommandHandler(IUnitOfWork work, IFileService fileService)
    {
        _work = work;
        _fileService = fileService;
    }

    public async Task Handle(InsertBrandCommand request, CancellationToken cancellationToken)
    {
        Brand brand = new Brand
        {
            Address = request.Address,
            Translation = request.Translation.MapTo<BrandTranslation, TranslationDto>()
        };
        brand.Translation.Id = ObjectId.GenerateNewId().ToString();
        if (!string.IsNullOrWhiteSpace(request.LogoUri))
        {
            brand.LogoUri = _fileService.AddImage(request.LogoUri, "LogoBrand",
                Guid.NewGuid().ToString().Substring(0, 6));
        }

        await _work.GenericRepository<Brand>().InsertOneAsync(brand, null, cancellationToken);
    }
}