namespace Identity.V2.Application.Users.V1.Queries.IsUserReferralCodeValid;

public record IsUserReferralCodeValidQuery(string ReferralCode) : IRequest<bool>;