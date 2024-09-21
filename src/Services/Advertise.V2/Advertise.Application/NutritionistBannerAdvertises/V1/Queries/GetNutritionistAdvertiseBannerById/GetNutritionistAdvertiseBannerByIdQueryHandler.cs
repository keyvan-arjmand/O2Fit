namespace Advertise.Application.NutritionistBannerAdvertises.V1.Queries.GetNutritionistAdvertiseBannerById;

public class GetNutritionistAdvertiseBannerByIdQueryHandler : IRequestHandler<GetNutritionistAdvertiseBannerByIdQuery, NutritionistBannerAdvertiseDto>
{
    private readonly IUnitOfWork _uow;
    private readonly ICurrentUserService _currentUserService;

    public GetNutritionistAdvertiseBannerByIdQueryHandler(IUnitOfWork uow, ICurrentUserService currentUserService)
    {
        _uow = uow;
        _currentUserService = currentUserService;
    }

    public async Task<NutritionistBannerAdvertiseDto> Handle(GetNutritionistAdvertiseBannerByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<NutritionistBannerAdvertise>.Filter.Eq(x => x.Status, AdvertiseStatus.Active);
        filter &= Builders<NutritionistBannerAdvertise>.Filter.Eq(x => x.IsDelete, false);
        filter &= Builders<NutritionistBannerAdvertise>.Filter.AnyEq(x => x.StatesIds, ObjectId.Parse(_currentUserService.StateId));
        filter &= Builders<NutritionistBannerAdvertise>.Filter.Eq(x => x.Id, request.Id);
        filter &= Builders<NutritionistBannerAdvertise>.Filter.AnyEq(x => x.Languages,
            _currentUserService.Language.ToEnum<Language>());
        var nutritionistBannerAdvertise = await _uow.GenericRepository<NutritionistBannerAdvertise>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);

        if (nutritionistBannerAdvertise == null)
            throw new NotFoundException(nameof(NutritionistBannerAdvertise), request.Id);

        var result = nutritionistBannerAdvertise.ToDto<NutritionistBannerAdvertiseDto>();

        var language = _currentUserService.Language!.ToEnum<Language>();
        switch (language)
        {
            case Language.Persian:
                result.Title = nutritionistBannerAdvertise.Title.Persian;
                result.Description = nutritionistBannerAdvertise.Description.Persian;
                result.Image = string.IsNullOrEmpty(nutritionistBannerAdvertise.Image.PersianImageName) ? string.Empty:
                        PathConstants.PersianNutritionistAdvertiseBannerPath + nutritionistBannerAdvertise.Image.PersianImageName;
                break;
            case Language.English:
                result.Title = nutritionistBannerAdvertise.Title.English;
                result.Description = nutritionistBannerAdvertise.Description.English;
                result.Image = string.IsNullOrEmpty(nutritionistBannerAdvertise.Image.EnglishImageName) ? string.Empty :
                        PathConstants.EnglishNutritionistAdvertiseBannerPath + nutritionistBannerAdvertise.Image.EnglishImageName;
                break;
            case Language.Arabic:
                result.Title = nutritionistBannerAdvertise.Title.Arabic;
                result.Description = nutritionistBannerAdvertise.Description.Arabic;
                result.Image = string.IsNullOrEmpty(nutritionistBannerAdvertise.Image.ArabicImageName) ? string.Empty : 
                        PathConstants.ArabicNutritionistAdvertiseBannerPath + nutritionistBannerAdvertise.Image.ArabicImageName;
                break;
            default:
                result.Title = nutritionistBannerAdvertise.Title.Persian;
                result.Description = nutritionistBannerAdvertise.Description.Persian;
                result.Image = string.IsNullOrEmpty(nutritionistBannerAdvertise.Image.PersianImageName) ? string.Empty:
                    PathConstants.PersianNutritionistAdvertiseBannerPath + nutritionistBannerAdvertise.Image.PersianImageName;
                break;
        }

        return result;
    }
}