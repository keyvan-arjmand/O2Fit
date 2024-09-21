namespace Identity.V2.Application.Users.V1.Queries.CheckReferralCodeDuplicate;

public record CheckReferralCodeDuplicateQuery(string Code): IRequest<bool>;