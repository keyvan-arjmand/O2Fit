namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.CreateNutritionistBannerAdvertise;

public class CreateNutritionistBannerAdvertiseCommandHandler : IRequestHandler<CreateNutritionistBannerAdvertiseCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;
    public CreateNutritionistBannerAdvertiseCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task Handle(CreateNutritionistBannerAdvertiseCommand request, CancellationToken cancellationToken)
    {
        var persianImageName = string.Empty;
        var englishImageName = string.Empty;
        var arabicImageName = string.Empty;
        if (!string.IsNullOrEmpty(request.Image.PersianImageUrl))
        {
            persianImageName = _fileService.AddImage(request.Image.PersianImageUrl,
                PathConstants.PersianNutritionistAdvertiseBannerPath, Guid.NewGuid().ToString());
        }
        if (!string.IsNullOrEmpty(request.Image.EnglishImageUrl))
        {
            englishImageName = _fileService.AddImage(request.Image.EnglishImageUrl,
                PathConstants.EnglishNutritionistAdvertiseBannerPath, Guid.NewGuid().ToString());
        }
        if (!string.IsNullOrEmpty(request.Image.ArabicImageUrl))
        {
            arabicImageName = _fileService.AddImage(request.Image.ArabicImageUrl,
                PathConstants.ArabicNutritionistAdvertiseBannerPath, Guid.NewGuid().ToString());
        }
        var nutritionistBanner = new NutritionistBannerAdvertise(request.Link, request.Cost, new NotNegativeForDoubleTypes(request.Budget), new NutritionistBannerAdvertiseImagePerLanguage(arabicImageName, englishImageName, persianImageName),
            new NutritionistBannerAdvertiseTitleTranslation(request.Title.Arabic, request.Title.English, request.Title.Persian), new NutritionistBannerAdvertiseDescriptionTranslation(request.Description.Arabic, request.Description.English, request.Description.Persian),
            AdvertiseStatus.Pending, request.Languages, request.ClickCount, 0, new NotNegativeForIntegerTypes(0),
            request.StateIds.Select(ObjectId.Parse).ToList());

        await _uow.GenericRepository<NutritionistBannerAdvertise>()
            .InsertOneAsync(nutritionistBanner, null, cancellationToken);
    }
}