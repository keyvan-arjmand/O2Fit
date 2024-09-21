namespace Food.V2.Application.Nationalities.V1.Queries.IsNationalityExits;

public record IsNationalityExitsQuery(string Id) : IRequest<bool>;