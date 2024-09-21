namespace Identity.V2.Application.NutritionistProfiles.V1.Queries.GetNutritionistProfileById;

public class GetNutritionistProfileByIdQueryHandler : IRequestHandler<GetNutritionistProfileByIdQuery , NutritionistDataDto>
{
    private readonly IUnitOfWork _uow;

    public GetNutritionistProfileByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<NutritionistDataDto> Handle(GetNutritionistProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>()
            .GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);

        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId);
        
        var nutritionistProfile = user.NutritionistProfile.MapTo<NutritionistProfileDto,NutritionistProfile>();
        var userData = user.ToDto<UserForUserInfoDto>();

        var nutritionistData = new NutritionistDataDto
        {
            User = userData,
            NutritionistProfile = nutritionistProfile
        };
        return nutritionistData;
    }
}