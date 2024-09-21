namespace Identity.V2.Application.Users.V1.Queries.GetUserInfoById;

public record GetUserInfoByIdQuery(string UserId): IRequest<UserInfoResponseDto>;