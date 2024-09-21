using System.Net.Http.Json;
using EventBus.Messages.Contracts.Services.Track.Specification;
using Identity.V2.Application.Dtos.UserTrackSpecification;

namespace Identity.V2.Application.Users.V1.Queries.GetUserInfoById;

public class GetUserInfoByIdQueryHandler: IRequestHandler<GetUserInfoByIdQuery, UserInfoResponseDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;
    public GetUserInfoByIdQueryHandler(IUnitOfWork uow, IMapper mapper, IHttpClientFactory httpClientFactory)
    {
        _uow = uow;
        _mapper = mapper;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<UserInfoResponseDto> Handle(GetUserInfoByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserGenericRepository<User>()
            .GetByIdAsync(ObjectId.Parse(request.UserId), cancellationToken);
        
        if (user == null)
            throw new NotFoundException(nameof(User), request.UserId);

        var userInfo = user.ToDto<UserInfoDto>();
        
        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.GetAsync($"https://workouttest.o2fitt.com/api/v1/TrackSpecification/get-last-track?id={request.UserId}",cancellationToken);
       // var httpResponseMessage2 = await httpClient.GetAsync("/api/v1/TrackSpecification/get-last-track?userid=64fc06c1ad71e88df5e4aeeb");
        
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var data = await httpResponseMessage.Content.ReadFromJsonAsync<List<UserTrackSpecificationDto>>();

            userInfo.UserTrackSpecification = new List<UserTrackSpecificationDto>();
            userInfo.UserTrackSpecification.AddRange(data);

        }

        var result = new UserInfoResponseDto();
        result.TrackSpecification = new List<UserTrackSpecificationDto>(userInfo.UserTrackSpecification);
        result.Profile =  _mapper.Map<ProfileForUserInfoDto>(userInfo.UserProfile);
        result.Profile.IsComplete = user.IsComplete;
        result.Target = userInfo.UserProfile.Target;
        
        var userForResult = _mapper.Map<UserForUserInfoDto>(userInfo);
        userForResult.Country = user.CountryId.ToString();
        result.User = userForResult;
        
        return result;
    }
}