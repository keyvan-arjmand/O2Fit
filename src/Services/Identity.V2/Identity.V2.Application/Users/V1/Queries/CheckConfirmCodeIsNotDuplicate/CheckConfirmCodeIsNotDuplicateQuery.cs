namespace Identity.V2.Application.Users.V1.Queries.CheckConfirmCodeIsNotDuplicate;

public record CheckConfirmCodeIsNotDuplicateQuery(string Code) : IRequest<bool>;