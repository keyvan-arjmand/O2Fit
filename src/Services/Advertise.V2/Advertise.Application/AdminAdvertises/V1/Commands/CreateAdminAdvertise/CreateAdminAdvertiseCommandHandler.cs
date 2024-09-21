namespace Advertise.Application.AdminAdvertises.V1.Commands.CreateAdminAdvertise;

public class CreateAdminAdvertiseCommandHandler : IRequestHandler<CreateAdminAdvertiseCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;
    public CreateAdminAdvertiseCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task Handle(CreateAdminAdvertiseCommand request, CancellationToken cancellationToken)
    {
        if (request.Budget % request.Cost != 0)
            throw new BadRequestException("Mod of budget and cost should be zero");

        if (request.Budget % request.ClickCount != 0)
            throw new BadRequestException("Mod of budget and click count should be zero");
        
        var persianImageName = string.Empty;
        var englishImageName = string.Empty;
        var arabicImageName = string.Empty;
        if (!string.IsNullOrEmpty(request.Images.PersianImageUrl))
        {
            persianImageName = _fileService.AddImage(request.Images.PersianImageUrl,
                PathConstants.PersianAdminAdvertiseBannerPath, Guid.NewGuid().ToString());
        }
        if (!string.IsNullOrEmpty(request.Images.EnglishImageUrl))
        {
            englishImageName = _fileService.AddImage(request.Images.EnglishImageUrl,
                PathConstants.EnglishAdminAdvertiseBannerPath, Guid.NewGuid().ToString());
        }
        if (!string.IsNullOrEmpty(request.Images.ArabicImageUrl))
        {
            arabicImageName = _fileService.AddImage(request.Images.ArabicImageUrl,
                PathConstants.ArabicAdminAdvertiseBannerPath, Guid.NewGuid().ToString());
        }
        var adminAdvertise = new AdminAdvertise(request.Link, request.ClickCount, request.Cost, new NotNegativeForDoubleTypes(request.Budget),
            new AdminAdvertiseImagePerLanguage(arabicImageName, englishImageName, persianImageName),
            new AdminAdvertiseTitleTranslation(request.Title.Persian, request.Title.English, request.Title.Arabic), new AdminAdvertiseDescriptionTranslation(request.Description.Persian, request.Description.English, request.Description.Arabic),
            AdvertiseStatus.Active, request.Languages, new NotNegativeForIntegerTypes(0), new NotNegativeForIntegerTypes(0), request.StateIds.Select(ObjectId.Parse).ToList());

        await _uow.GenericRepository<AdminAdvertise>().InsertOneAsync(adminAdvertise, null, cancellationToken);
    }
}