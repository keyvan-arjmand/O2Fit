using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Domain.Aggregates.DietCategoryAggregate;

namespace Food.V2.Application.DietCategories.V1.Commands.UpdateDietCategory;

public class UpdateDietCategoryCommandHandler : IRequestHandler<UpdateDietCategoryCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public UpdateDietCategoryCommandHandler(IUnitOfWork work, IMapper mapper, IFileService fileService)
    {
        _work = work;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task Handle(UpdateDietCategoryCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.ParentId) && !await _work.GenericRepository<DietCategory>()
                .AnyAsync(x => x.Id == request.ParentId, cancellationToken))
            throw new AppException("Parent Not Found");
        var result = await _work.GenericRepository<DietCategory>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (request == null) throw new NotFoundException("Diet Category Not Found");

        if (!string.IsNullOrEmpty(request.ImageName))
        {
            result.ImageName = _fileService.AddImage(request.ImageName, "ImageDietCategory"
                , Guid.NewGuid().ToString().Substring(0, 7)); 
        }

        if (!string.IsNullOrEmpty(request.BannerImageName))
        {
            result.BannerImageName =  _fileService.AddImage(request.BannerImageName, "BannerImageDietCategory"
                , Guid.NewGuid().ToString().Substring(0, 7)); 
        }

        result.Description.Arabic = request.Description.Arabic;
        result.Description.English = request.Description.English;
        result.Description.Persian = request.Description.Persian;
        result.Name.Arabic = request.Name.Arabic;
        result.Name.English = request.Name.English;
        result.Name.Persian = request.Name.Persian;
        result.ParentId = request.ParentId.StringToObjectId();

        result.IsActive = request.IsActive;
        await _work.GenericRepository<DietCategory>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<DietCategory, object>>[]
                {
                    x => x.ImageName,
                    x => x.BannerImageName,
                    x => x.Description,
                    x => x.IsPromote,
                    x => x.Name,
                    x => x.ParentId,
                    x => x.IsActive,
                }, null, cancellationToken);
    }
}