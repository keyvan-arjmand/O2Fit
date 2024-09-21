namespace Advertise.Application.AdminAdvertises.V1.Queries.GetAllActiveAdminAdvertise;

public class GetAllActiveAdminAdvertiseQueryHandler : IRequestHandler<GetAllActiveAdminAdvertiseQuery,List<AdminAdvertiseDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly ICurrentUserService _currentUserService;
    public GetAllActiveAdminAdvertiseQueryHandler(IUnitOfWork uow, ICurrentUserService currentUserService)
    {
        _uow = uow;
        _currentUserService = currentUserService;
    }

    public async Task<List<AdminAdvertiseDto>> Handle(GetAllActiveAdminAdvertiseQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<AdminAdvertise>.Filter.Eq(x => x.Status, AdvertiseStatus.Active);
        filter &= Builders<AdminAdvertise>.Filter.Eq(x => x.IsDelete, false);
        filter &= Builders<AdminAdvertise>.Filter.AnyEq(x => x.StatesIds, ObjectId.Parse(_currentUserService.StateId));
        filter &= Builders<AdminAdvertise>.Filter.AnyEq(x => x.Languages, _currentUserService.Language.ToEnum<Language>());
        var adminAdvertises = await _uow.GenericRepository<AdminAdvertise>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);

        var result = adminAdvertises.ToDto<AdminAdvertiseDto>().ToList();
        foreach (var advertiseDto in result)
        {
            switch (request.Language)
            {
                case Language.Persian:
                    advertiseDto.Title = adminAdvertises.First(x => x.Id == advertiseDto.Id).Title.Persian;
                    advertiseDto.Description = adminAdvertises.First(x => x.Id == advertiseDto.Id).Description.Persian;
                    advertiseDto.Image = string.IsNullOrEmpty(adminAdvertises.First(x => x.Id == advertiseDto.Id).Image.PersianImageName) ? string.Empty : 
                        PathConstants.PersianAdminAdvertiseBannerPath + adminAdvertises.First(x => x.Id == advertiseDto.Id).Image.PersianImageName;
                    continue;
                case Language.English:
                    advertiseDto.Title = adminAdvertises.First(x => x.Id == advertiseDto.Id).Title.English;
                    advertiseDto.Description = adminAdvertises.First(x => x.Id == advertiseDto.Id).Description.English;
                    advertiseDto.Image = string.IsNullOrEmpty(adminAdvertises.First(x => x.Id == advertiseDto.Id).Image.EnglishImageName) ? string.Empty :
                    PathConstants.EnglishAdminAdvertiseBannerPath + adminAdvertises.First(x => x.Id == advertiseDto.Id).Image.EnglishImageName;
                    continue;
                case Language.Arabic:
                    advertiseDto.Title = adminAdvertises.First(x => x.Id == advertiseDto.Id).Title.Arabic;
                    advertiseDto.Description = adminAdvertises.First(x => x.Id == advertiseDto.Id).Description.Arabic;
                    advertiseDto.Image = string.IsNullOrEmpty(adminAdvertises.First(x => x.Id == advertiseDto.Id).Image.ArabicImageName) ? string.Empty : 
                    PathConstants.ArabicAdminAdvertiseBannerPath + adminAdvertises.First(x => x.Id == advertiseDto.Id).Image.ArabicImageName;
                    continue;
                default:
                    advertiseDto.Title = adminAdvertises.First(x => x.Id == advertiseDto.Id).Title.Persian;
                    advertiseDto.Description = adminAdvertises.First(x => x.Id == advertiseDto.Id).Description.Persian;
                    advertiseDto.Image = string.IsNullOrEmpty(adminAdvertises.First(x => x.Id == advertiseDto.Id).Image.PersianImageName) ? string.Empty :
                        PathConstants.PersianAdminAdvertiseBannerPath + adminAdvertises.First(x => x.Id == advertiseDto.Id).Image.PersianImageName;
                    continue;
            }
        }

        return result;
    }
}