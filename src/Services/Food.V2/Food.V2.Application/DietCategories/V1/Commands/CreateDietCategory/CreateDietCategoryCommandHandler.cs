using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.DietCategoryAggregate;
using Google.Protobuf.Collections;

namespace Food.V2.Application.DietCategories.V1.Commands.CreateDietCategory;

public class CreateDietCategoryCommandHandler : IRequestHandler<CreateDietCategoryCommand, string>
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public CreateDietCategoryCommandHandler(IUnitOfWork work, IMapper mapper, IFileService fileService)
    {
        _work = work;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<string> Handle(CreateDietCategoryCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.ParentId) && !await _work.GenericRepository<DietCategory>()
                .AnyAsync(x => x.Id == request.ParentId, cancellationToken))
            throw new NotFoundException($"Parent Not Found");
        if (!string.IsNullOrWhiteSpace(request.ImageName))
        {
            request.ImageName = _fileService.AddImage(request.ImageName, "ImageDietCategory"
                , Guid.NewGuid().ToString().Substring(0, 7));
        }

        if (!string.IsNullOrWhiteSpace(request.BannerImageName))
        {
            request.BannerImageName = _fileService.AddImage(request.BannerImageName, "BannerImageDietCategory"
                , Guid.NewGuid().ToString().Substring(0, 7));
        }
        var desc = request.Description.MapTo<DietCategoryTranslation,TranslationDto>();
        var name = request.Name.MapTo<DietCategoryTranslation,TranslationDto>();
        desc.Id = ObjectId.GenerateNewId().ToString();
        name.Id = ObjectId.GenerateNewId().ToString();

        var dietCat = new DietCategory
        {
            IsPromote = request.IsPromote,
            BannerImageName = request.BannerImageName,
            ParentId = request.ParentId.StringToObjectId(),
            Description = desc,
            ImageName = request.ImageName,
            IsActive = request.IsActive,
            Name = name,
        };
        await _work.GenericRepository<DietCategory>()
            .InsertOneAsync(dietCat, null, cancellationToken);
        return dietCat.Id;
    }
}