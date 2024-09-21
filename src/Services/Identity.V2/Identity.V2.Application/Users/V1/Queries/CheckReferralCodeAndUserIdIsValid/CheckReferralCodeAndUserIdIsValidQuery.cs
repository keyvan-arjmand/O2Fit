namespace Identity.V2.Application.Users.V1.Queries.CheckReferralCodeAndUserIdIsValid;

public record CheckReferralCodeAndUserIdIsValidQuery(string Code, string UserId): IRequest<bool>;